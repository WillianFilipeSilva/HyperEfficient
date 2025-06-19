using AutoMapper;
using HyperEfficient.Contracts.Repository;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Services
{
    public class RegistroService : IRegistroService
    {
        private readonly IRegistroRepository _registroRepository;
        private readonly IMapper _map;

        public RegistroService(IRegistroRepository registroRepository, IMapper map)
        {
            _registroRepository = registroRepository;
            _map = map;
        }

        public async Task<MessageResponse> Insert(RegistroInsertDTO dto)
        {
            await _registroRepository.Insert(_map.Map<RegistroEntity>(dto));
            return new MessageResponse { Message = "Registro cadastrado com sucesso!" };
        }

        public async Task<MessageResponse> Update(RegistroEntity registro)
        {
            await _registroRepository.Update(registro);
            return new MessageResponse { Message = "Registro editado com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            await _registroRepository.Delete(id);
            return new MessageResponse { Message = "Registro deletado com sucesso!" };
        }

        public async Task<RegistroGetAllResponse> GetAll()
        {
            var registros = await _registroRepository.GetAll();
            return new RegistroGetAllResponse { Data = registros };
        }

        public async Task<RegistroEntity> GetById(int id)
        {
            return await _registroRepository.GetById(id);
        }
    }
}
