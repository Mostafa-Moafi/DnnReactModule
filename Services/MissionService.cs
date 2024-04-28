// MIT License

using AutoMapper;
using DnnReactDemo.Data.IRepositories;

namespace DnnReactDemo.Services
{
    public class MissionService : IMissionService
    {
        private IMapper _mapper;
        private IRepositoryManager _repositoryManager;

        public MissionService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            this._repositoryManager = repositoryManager;
            this._mapper = mapper;
        }

    }
}
