using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        private readonly MissionDbContext _dbContext = dbContext;

        public Task<List<MissionRequestViewModel>> GetAllMissionAsync()
        {
            return _dbContext.Missions.Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).ToListAsync();
        }

        public async Task<MissionRequestViewModel?> GetMissionById(int id)
        {
            return await _dbContext.Missions.Where(m => m.Id == id).Select(m => new MissionRequestViewModel()
            {
                Id = m.Id,
                CityId = m.CityId,
                CountryId = m.CountryId,
                EndDate = m.EndDate,
                MissionDescription = m.MissionDescription,
                MissionImages = m.MissionImages,
                MissionSkillId = m.MissionSkillId,
                MissionThemeId = m.MissionThemeId,
                MissionTitle = m.MissionTitle,
                StartDate = m.StartDate,
                TotalSeats = m.TotalSheets ?? 0,
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddMission(Missions mission)
        {
            try
            {
                _dbContext.Missions.Add(mission);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return true;
        }

        // int userId
        public async Task<IList<Missions>> ClientSideMissionList()
        {
            return await _dbContext.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
               .Include(m => m.MissionTheme)
                .Where(m => !m.IsDeleted)
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }
    }
}
