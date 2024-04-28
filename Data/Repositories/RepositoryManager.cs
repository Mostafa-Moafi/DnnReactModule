// MIT License

using DnnReactDemo.Data.IRepositories;
using System.Threading.Tasks;

namespace DnnReactDemo.Data.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private ModuleDbContext _context;
        private IMissionRepository _missionRepository;
        public RepositoryManager(ModuleDbContext context)
        {
            _context = context;
        }

        public IMissionRepository Mission
        {
            get
            {
                if (this._missionRepository is null)
                    this._missionRepository = new MissionRepository(_context);

                return this._missionRepository;
            }
        }

        public void SaveChanges() => _context.SaveChanges();
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
