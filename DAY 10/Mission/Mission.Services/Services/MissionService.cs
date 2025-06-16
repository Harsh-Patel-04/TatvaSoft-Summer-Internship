using Mission.Entities;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;
using Mission.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Services.Services
{
    public class MissionService(IMissionRepository missionRepository) : IMissionService
    {
        public List<Missions> GetMissionList()
        {
            return missionRepository.GetMissionList();
        }

        public Task<string> AddMission(AddMissionRequestModel model)
        {
            return missionRepository.AddMission(model);
        }
    }
}
