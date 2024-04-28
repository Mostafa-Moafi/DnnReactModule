// MIT License

using AutoMapper;
using DnnReactDemo.Data.IRepositories;
using DnnReactDemo.Utilities.CustomMapping;

namespace DnnReactDemo.Services
{
    public class ServiceManager : IServiceManager
    {
        private IMapper _mapper;
        private IMissionService _missionService;
        private IRepositoryManager _repositoryManager;
        public ServiceManager(IRepositoryManager repositoryManager)
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddCustomMappingProfile();
            });
            _mapper = mapperConfig.CreateMapper();
            _repositoryManager = repositoryManager;

        }

        public IMissionService Mission
        {
            get
            {
                if (this._missionService == null)
                    this._missionService = new MissionService(_repositoryManager, _mapper);

                return this._missionService;
            }
        }
    }
}
