using Mission.Entities.Models;
using Mission.Entities;

namespace Mission.Services.IServices
{
    public interface IMissionService
    {
        Task<List<MissionViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(MissionRequestViewModel model);
        Task<IList<MissionDetailResponseModel>> ClientSideMissionList(int userId);
        Task<bool> UpdateMission(MissionRequestViewModel model);
        string MissionDelete(int id);
        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);
        public Task<List<MissionApplicationViewModel>> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);
        Task<bool> MissionApplicationDelete(UpdateMissionApplicationModel missionApplication);
    }
}
