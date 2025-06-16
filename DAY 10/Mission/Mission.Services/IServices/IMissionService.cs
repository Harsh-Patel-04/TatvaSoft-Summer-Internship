using Mission.Entities;
using Mission.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Services.IServices
{
    public interface IMissionService
    {
        List<Missions> GetMissionList();
        Task<string> AddMission(AddMissionRequestModel model);
    }
}
