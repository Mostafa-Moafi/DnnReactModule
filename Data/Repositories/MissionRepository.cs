// MIT License

using DnnReactDemo.Data.IRepositories;
using DnnReactDemo.Entities;

namespace DnnReactDemo.Data.Repositories
{
    public class MissionRepository : Repository<Mission>, IMissionRepository
    {
        private ModuleDbContext _context;
        public MissionRepository(ModuleDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
