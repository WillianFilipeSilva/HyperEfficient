using AutoMapper;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Registro;
using HyperEfficient.Entities;
using HyperEfficient.Services.Base;

namespace HyperEfficient.Services
{
    public class RegistroService : ServiceBase<Registro>, IRegistroService
    {
        private readonly IEquipamentoRepository _equipamentoRepository;

        public RegistroService(IRegistroRepository repository, IMapper map, IEquipamentoRepository equipamentoRepository
        ) : base(repository, map)
        {
            _equipamentoRepository = equipamentoRepository;
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
            try
            {
                if (await ((IRegistroRepository)_repository)
                        .GetRegistroByEquipamentoId(equipamentoId) is var registro && registro is not null &&
                    registro.DataFinal is null)
                {
                    registro.DataFinal = DateTime.UtcNow;
                    if (await _repository.Update(registro) <= 0)
                    {
                        throw new KeyNotFoundException(
                            $"Não foi possível finalizar o registro para o equipamento {equipamentoId}!");
                    }

                    return new MessageResponse { Message = "Registro finalizado!" };
                }

                registro = new Registro
                {
                    EquipamentoId = equipamentoId, DataInicial = DateTime.UtcNow, DataFinal = null
                };
                if (await _repository.Insert(registro) <= 0)
                {
                    throw new KeyNotFoundException(
                        $"Não foi possível iniciar um registro para o equipamento {equipamentoId}!");
                }

                return new MessageResponse { Message = "Registro iniciado!" };
            }
            finally
            {
                _equipamentoRepository.ToggleEquipamentoStatus(equipamentoId);
            }
        }
    }
}