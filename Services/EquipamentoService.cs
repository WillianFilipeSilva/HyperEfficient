using AutoMapper;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.Equipamento;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Entities;
using System.Diagnostics;

namespace HyperEfficient.Services
{
    public class EquipamentoService : IEquipamentoService
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IMapper _map;
        private readonly ITuyaApiClientService _tuyaApiClientService;

        public EquipamentoService(IEquipamentoRepository equipamentoRepository,
            ITuyaApiClientService tuyaApiClientService,
            IMapper map
        )
        {
            _equipamentoRepository = equipamentoRepository;
            _tuyaApiClientService = tuyaApiClientService;
            _map = map;
        }

        public async Task<MessageResponse> Insert(EquipamentoInsertDto dto)
        {
            if (await _equipamentoRepository.Insert(_map.Map<Equipamento>(dto)) <= 0)
                throw new KeyNotFoundException($"Não foi possível cadastrar o equipamento {dto.Nome}!");

            return new MessageResponse { Message = "Equipamento cadastrado com sucesso!" };
        }

        public async Task<MessageResponse> Update(Equipamento equipamento)
        {
            if (await _equipamentoRepository.Update(equipamento) <= 0)
                throw new KeyNotFoundException($"Não foi possível editar o equipamento {equipamento.Nome}!");

            return new MessageResponse { Message = "Equipamento editado com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            if (await _equipamentoRepository.Delete(id) <= 0)
                throw new KeyNotFoundException("Não foi possível deletar o equipamento!");

            return new MessageResponse { Message = "Equipamento deletado com sucesso!" };
        }

        public async Task<EquipamentoGetAllResponse> GetAll()
        {
            return new EquipamentoGetAllResponse
            {
                Data = await _equipamentoRepository.GetAll() ?? new List<Equipamento>()
            };
        }

        public async Task<Equipamento> GetById(int id)
        {
            return await _equipamentoRepository.GetById(id) ??
                   throw new KeyNotFoundException("Equipamento não encontrado!");
        }

        public async Task<GetPagedResponseBase<EquipamentoDto>> GetPaged(int page, int pageSize)
        {
            var data = await _equipamentoRepository.GetEquipamentoProjection(page, pageSize) ??
                       new List<EquipamentoDto>();

            var allData =
                await _equipamentoRepository.GetAll() ?? new List<Equipamento>();

            var totalItems = allData.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return new GetPagedResponseBase<EquipamentoDto>
            {
                Data = data,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems
            };
        }

        public async Task AtualizarConsumoEquipamentos()
        {
            try
            {
                var equipamentos = await _equipamentoRepository.GetAll();
                var equipamentosComIntegracao = equipamentos
                    .Where(e => !string.IsNullOrEmpty(e.DeviceIdIntegration))
                    .ToList();

                foreach (var equipamento in equipamentosComIntegracao)
                {
                    await AtualizarConsumoEquipamento(equipamento);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao atualizar consumo dos equipamentos: {ex.Message}");
            }
        }

        public async Task AtualizarConsumoEquipamento(int equipamentoId)
        {
            if (await _equipamentoRepository.GetById(equipamentoId) is var equipamento &&
                (equipamento == null || string.IsNullOrEmpty(equipamento.DeviceIdIntegration)))
                return;

            var status = new EquipamentoStatusDto();
            try
            {
                status = await _tuyaApiClientService.GetStatusAsync(equipamento.DeviceIdIntegration);

                equipamento.PotenciaKwh = status.PotenciaKwh;
                equipamento.Ativo = status.Ligado;

                await _equipamentoRepository.Update(equipamento);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao atualizar consumo do equipamento {equipamentoId}: {ex.Message}");
            }
            finally
            {
                while (status.Ligado && status.PotenciaKwh <= 0)
                {
                    Thread.Sleep(10000);
                    status = await _tuyaApiClientService.GetStatusAsync(equipamento.DeviceIdIntegration);
                }

                equipamento.PotenciaKwh = status.PotenciaKwh;
                equipamento.Ativo = status.Ligado;

                await _equipamentoRepository.Update(equipamento);
            }
        }

        public async Task AtualizarConsumoEquipamento(Equipamento equipamento)
        {
            try
            {
                if (string.IsNullOrEmpty(equipamento.DeviceIdIntegration))
                    return;

                var status = await _tuyaApiClientService.GetStatusAsync(equipamento.DeviceIdIntegration);
                if (status.Ligado && status.PotenciaKwh > 0)
                    equipamento.PotenciaKwh = status.PotenciaKwh;

                equipamento.Ativo = status.Ligado;

                await _equipamentoRepository.Update(equipamento);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(
                    $"Erro ao atualizar consumo do equipamento {equipamento.Nome} id: {equipamento.Id}: {ex.Message}");
            }
        }
    }
}