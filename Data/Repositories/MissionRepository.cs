// MIT License

using DnnReactModule.Data.IRepositories;
using DnnReactModule.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DnnReactModule.Data.Repositories
{
    public class MissionRepository : Repository<Mission>, IMissionRepository
    {
        private ModuleDbContext _context;
        public MissionRepository(ModuleDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Mission> GetMissionByUserIdAsync(int missionId, int userId)
        {
            return await _context.Missions.FirstOrDefaultAsync(x => x.Id == missionId && x.UserId == userId);
        }

        public async Task<IList<Mission>> GetMissionByUserIdAsync(int userId)
        {
            return await _context.Missions.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
