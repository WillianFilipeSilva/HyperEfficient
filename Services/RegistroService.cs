using AutoMapper;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.Equipamento;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Registro;
using HyperEfficient.Entities;
using HyperEfficient.Services.Base;

namespace HyperEfficient.Services
{
    public class RegistroService : ServiceBase<Registro>, IRegistroService
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IEquipamentoService _equipamentoService;
        private readonly ITuyaApiClientService _tuyaApiClientService;

        public RegistroService(IRegistroRepository repository,
            IMapper map,
            IEquipamentoRepository equipamentoRepository,
            ITuyaApiClientService tuyaApiClientService,
            IEquipamentoService equipamentoService
        ) : base(repository, map)
        {
            _equipamentoRepository = equipamentoRepository;
            _tuyaApiClientService = tuyaApiClientService;
            _equipamentoService = equipamentoService;
        }

        public async Task<MessageResponse> Insert(RegistroInsertDto dto)
        {
            if (await _repository.Insert(_map.Map<Registro>(dto)) <= 0)
                throw new KeyNotFoundException("Não foi possível cadastrar o registro!");

            return new MessageResponse { Message = "Registro cadastrado com sucesso!" };
        }

        public async Task<MessageResponse> Update(Registro registro)
        {
            if (await _repository.Update(registro) <= 0)
                throw new KeyNotFoundException("Não foi possível editar o registro!");

            return new MessageResponse { Message = "Registro editado com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            if (await _repository.Delete(id) <= 0)
                throw new KeyNotFoundException("Não foi possível deletar o registro!");

            return new MessageResponse { Message = "Registro deletado com sucesso!" };
        }

        public async Task<RegistroGetAllResponse> GetAll()
        {
            return new RegistroGetAllResponse { Data = await _repository.GetAll() ?? new List<Registro>() };
        }

        public async Task<GetPagedResponseBase<Registro>> GetPaged(int page, int pageSize)
        {
            var data = await ((IRegistroRepository)_repository).GetPaged(page, pageSize) ?? new List<Registro>();
            var allData = await _repository.GetAll() ?? new List<Registro>();
            var totalItems = allData.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return new GetPagedResponseBase<Registro>
            {
                Data = data,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems
            };
        }

        public async Task<Registro> GetById(int id)
        {
            return await _repository.GetById(id) ?? throw new KeyNotFoundException("Registro não encontrado!");
        }

        public async Task<MessageResponse> StartStopRegistro(int equipamentoId)
        {
            if (await _equipamentoRepository.GetById(equipamentoId) is var equipamento && equipamento is not null &&
                equipamento.DeviceIdIntegration is not null)
            {
                var equipamentoStatus = await _tuyaApiClientService.GetStatusAsync(equipamento.DeviceIdIntegration);

                if (!equipamento.Ativo && !equipamentoStatus.Ligado)
                    await LigarAsync(equipamento);

                else if (equipamento.Ativo && equipamentoStatus.Ligado)
                    await DesligarAsync(equipamento);

                else if (!equipamento.Ativo && equipamentoStatus.Ligado)
                    return await StartRegistro(equipamentoId);
            }

            if (await ((IRegistroRepository)_repository)
                    .GetRegistroByEquipamentoId(equipamentoId) is var registro &&
                registro is not null && registro.DataFinal is null)
                return await StopRegistro(registro);

            return await StartRegistro(equipamentoId);
        }

        public async Task<EquipamentoStatusDto> ObterConsumoAsync(int equipamentoId)
        {
            if (await _equipamentoRepository.GetById(equipamentoId) is var equipamento && equipamento is null)
                throw new KeyNotFoundException("Equipamento não encontrado!");

            if (equipamento.DeviceIdIntegration is null)
            {
                double totalKwhCalculado = 0;

                if (await ((IRegistroRepository)_repository)
                        .GetRegistroByEquipamentoId(equipamentoId) is var registro && registro is not null)
                {
                    var tempoDecorrido = registro.TotalTempo is null
                        ? (DateTime.Now - registro.DataInicial).TotalHours
                        : registro.TotalTempo.Value;
                    totalKwhCalculado = tempoDecorrido * equipamento.PotenciaKwh;
                }

                return new EquipamentoStatusDto
                {
                    PotenciaKwh = equipamento.PotenciaKwh, TotalKwh = totalKwhCalculado, Ligado = equipamento.Ativo
                };
            }

            var equipamentoStatus = await _tuyaApiClientService.GetStatusAsync(equipamento.DeviceIdIntegration);
            if (equipamentoStatus.Ligado && equipamentoStatus.PotenciaKwh > 0)
            {
                equipamento.PotenciaKwh = equipamentoStatus.PotenciaKwh;
                equipamento.Ativo = equipamentoStatus.Ligado;
                await _equipamentoRepository.Update(equipamento);
            }
    
            return equipamentoStatus;
        }

        public async Task LigarAsync(int equipamentoId)
        {
            if (await _equipamentoRepository.GetById(equipamentoId) is var equipamento && equipamento is null ||
                equipamento.DeviceIdIntegration is null)
                throw new KeyNotFoundException($"Não foi possível ligar o equipamento {equipamentoId}!");
            try
            {
                await _tuyaApiClientService.LigarAsync(equipamento.DeviceIdIntegration);
                await _equipamentoService.AtualizarConsumoEquipamento(equipamento);
            }
            finally
            {
                Thread.Sleep(10000);
                await _equipamentoService.AtualizarConsumoEquipamento(equipamento);
            }
        }

        public async Task DesligarAsync(int equipamentoId)
        {
            if (await _equipamentoRepository.GetById(equipamentoId) is var equipamento && equipamento is null ||
                equipamento.DeviceIdIntegration is null)
                throw new KeyNotFoundException($"Não foi possível desligar o equipamento {equipamentoId}!");

            try
            {
                await _tuyaApiClientService.DesligarAsync(equipamento.DeviceIdIntegration);
            }
            finally
            {
                await _equipamentoService.AtualizarConsumoEquipamento(equipamento);
            }
        }

        private async Task LigarAsync(Equipamento equipamento)
        {
            try
            {
                if (equipamento.DeviceIdIntegration is null)
                {
                    throw new KeyNotFoundException(
                        $"Não foi possível ligar o equipamento {equipamento.Nome} id: {equipamento.Id}!");
                }

                await _tuyaApiClientService.LigarAsync(equipamento.DeviceIdIntegration);
                await _equipamentoService.AtualizarConsumoEquipamento(equipamento);
            }
            finally
            {
                Thread.Sleep(10000);
                await _equipamentoService.AtualizarConsumoEquipamento(equipamento);
            }
        }

        private async Task DesligarAsync(Equipamento equipamento)
        {
            if (equipamento.DeviceIdIntegration is null)
            {
                throw new KeyNotFoundException(
                    $"Não foi possível desligar o equipamento {equipamento.Nome} id: {equipamento.Id}!");
            }

            await _tuyaApiClientService.DesligarAsync(equipamento.DeviceIdIntegration);
            await _equipamentoService.AtualizarConsumoEquipamento(equipamento);
        }

        private async Task<MessageResponse> StopRegistro(Registro registro)
        {
            var equipamento = await _equipamentoRepository.GetById(registro.EquipamentoId);

            var consumoAtual = await ObterConsumoAsync(registro.EquipamentoId);

            registro.DataFinal = DateTime.UtcNow;

            var tempoTotal = (registro.DataFinal.Value - registro.DataInicial).TotalHours;

            if (!string.IsNullOrEmpty(equipamento.DeviceIdIntegration))
                registro.TotalKwh = consumoAtual.TotalKwh;

            else
                registro.TotalKwh = tempoTotal * equipamento.PotenciaKwh;

            if (await _repository.Update(registro) <= 0)
            {
                throw new KeyNotFoundException(
                    $"Não foi possível finalizar o registro para o equipamento {registro.EquipamentoId}!");
            }

            await _equipamentoRepository.InactiveEquipamento(registro.EquipamentoId);
            return new MessageResponse { Message = "Registro finalizado!" };
        }

        private async Task<MessageResponse> StartRegistro(int equipamentoId)
        {
            var registro = new Registro
            {
                EquipamentoId = equipamentoId, DataInicial = DateTime.UtcNow, DataFinal = null
            };
            if (await _repository.Insert(registro) <= 0)
            {
                throw new KeyNotFoundException(
                    $"Não foi possível iniciar um registro para o equipamento {equipamentoId}!");
            }

            await _equipamentoRepository.ActiveEquipamento(equipamentoId);

            return new MessageResponse { Message = "Registro iniciado!" };
        }
    }
}