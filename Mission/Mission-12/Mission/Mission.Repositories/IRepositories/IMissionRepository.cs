using Mission.Entities;
using Mission.Entities.Models;

namespace Mission.Repositories.IRepositories
{
    public interface IMissionRepository
    {
        Task<List<MissionViewModel>> GetAllMissionAsync();
        Task<MissionRequestViewModel?> GetMissionById(int id);
        Task<bool> AddMission(Missions mission);
        Task<IList<Missions>> ClientSideMissionList();
        Task SaveAsync();
        string DeleteMission(int id);
        Task<Missions> GetByIdAsync(int id);
        Task<bool> ApplyMission(AddMissionApplicationRequestModel model);

        Task<List<MissionApplicationViewModel>> GetMissionApplicationList();
        Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication);
        Task<bool> MissionApplicationDelete(UpdateMissionApplicationModel missionApplication);
    }
}
