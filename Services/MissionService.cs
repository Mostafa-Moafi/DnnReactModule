// MIT License

using AutoMapper;
using DnnReactModule.Data.IRepositories;
using DnnReactModule.Dto.Missions;
using DnnReactModule.Entities;
using DnnReactModule.Utilities.Exceptions;
using DotNetNuke.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnnReactModule.Services
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

        public async Task<MissionSelectDto> AddAsync(AddOrUpdateMissionDto dto, UserInfo creator)
        {
            var mission = dto.ToEntity(_mapper);
            mission.UserId = creator.UserID;
            _repositoryManager.Mission.Add(mission);
            await _repositoryManager.SaveChangesAsync();

            return MissionSelectDto.FromEntity(_mapper, mission);
        }

        public async Task DeleteAsync(int id, UserInfo modifier)
        {
            var mission = await _repositoryManager.Mission.GetMissionByUserIdAsync(id, modifier.UserID);
            if (mission == null)
                throw new NotFoundException("Mission Not Found");

            _repositoryManager.Mission.Delete(mission);
            await _repositoryManager.SaveChangesAsync();

            await Task.CompletedTask;
        }

        public async Task<List<MissionSelectDto>> GetByUserAsync(int userId)
        {
            var missions = await _repositoryManager.Mission.GetMissionByUserIdAsync(userId);
            return _mapper.Map<List<MissionSelectDto>>(missions);
        }

        public async Task<MissionSelectDto> UpdateAsync(int id, AddOrUpdateMissionDto dto, UserInfo modifier)
        {

            var mission = await _repositoryManager.Mission.GetMissionByUserIdAsync(id, modifier.UserID);
            if (mission == null)
                throw new NotFoundException("Mission Not Found");

            mission = dto.ToEntity(_mapper, mission);
            mission.UpdatedDate = DateTime.Now;

            _repositoryManager.Mission.Update(mission);
            await _repositoryManager.SaveChangesAsync();

            return MissionSelectDto.FromEntity(_mapper, mission);
        }
    }
}
