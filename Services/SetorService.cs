using AutoMapper;
using HyperEfficient.Contracts.Repository;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Services
{
    public class SetorService : ISetorService
    {
        private readonly ISetorRepository _setorRepository;
        private readonly IMapper _map;

        public SetorService(ISetorRepository setorRepository, IMapper map)
        {
            _setorRepository = setorRepository;
            _map = map;
        }

        public async Task<MessageResponse> Insert(SetorInsertDTO dto)
        {
            await _setorRepository.Insert(_map.Map<SetorEntity>(dto));
            return new MessageResponse { Message = "Setor cadastrado com sucesso!" };
        }

        public async Task<MessageResponse> Update(SetorEntity setor)
        {
            await _setorRepository.Update(setor);
            return new MessageResponse { Message = "Setor editado com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            await _setorRepository.Delete(id);
            return new MessageResponse { Message = "Setor deletado com sucesso!" };
        }

        public async Task<SetorGetAllResponse> GetAll()
        {
            var setores = await _setorRepository.GetAll();
            return new SetorGetAllResponse { Data = setores };
        }

        public async Task<SetorEntity> GetById(int id)
        {
            return await _setorRepository.GetById(id);
        }
    }
}
