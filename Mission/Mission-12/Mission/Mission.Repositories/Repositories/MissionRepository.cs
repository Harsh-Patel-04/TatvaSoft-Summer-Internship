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

        public Task<List<MissionViewModel>> GetAllMissionAsync()
        {
            //return (from mission in _dbContext.Missions
            //        join theme in _dbContext.MissionThemes
            //        on mission.MissionThemeId equals theme.Id
            //        select new MissionRequestViewModel
            //        {
            //            Id = mission.Id,
            //            CityId = mission.CityId,
            //            CountryId = mission.CountryId,
            //            EndDate = mission.EndDate,
            //            MissionDescription = mission.MissionDescription,
            //            MissionImages = mission.MissionImages,
            //            MissionSkillId = mission.MissionSkillId,
            //            MissionThemeId = mission.MissionThemeId,
            //            MissionThemeName = theme.ThemeName, 
            //            MissionTitle = mission.MissionTitle,
            //            StartDate = mission.StartDate,
            //            TotalSeats = mission.TotalSheets ?? 0,
            //        }).ToListAsync();
            return _dbContext.Missions
                .Where(m => !m.IsDeleted)
                .Include(m => m.City)
                .Include(m => m.Country)
                .Include(m => m.MissionTheme)
                .Select(m => new MissionViewModel()
                {
                    Id = m.Id,
                    CityId = m.CityId,
                    CountryId = m.CountryId,
                    EndDate = m.EndDate,
                    MissionDescription = m.MissionDescription,
                    MissionImages = m.MissionImages,
                    MissionSkillId = m.MissionSkillId,
                    MissionThemeId = m.MissionThemeId,
                    MissionThemeName = m.MissionTheme.ThemeName,
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

        public async Task<bool> AddMission(Missions model)
        {
            try
            {
                var isExist = dbContext.Missions.Where(x =>
                            x.MissionTitle == model.MissionTitle
                            && x.StartDate == model.StartDate
                            && x.EndDate == model.EndDate
                            && x.CityId == model.CityId
                            && !x.IsDeleted
                        ).FirstOrDefault();

                if (isExist != null) throw new Exception("Mission already exist!");

                Missions missions = new Missions()
                {
                    MissionTitle = model.MissionTitle,
                    MissionDescription = model.MissionDescription,
                    MissionImages = model.MissionImages,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    CountryId = model.CountryId,
                    CityId = model.CityId,
                    TotalSheets = model.TotalSheets,
                    MissionThemeId = model.MissionThemeId,
                    MissionSkillId = model.MissionSkillId,


                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                };
                await dbContext.Missions.AddAsync(missions);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
        public async Task<Missions> GetByIdAsync(int id)
        {
            return await _dbContext.Missions.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public string DeleteMission(int id)
        {
            var mission = _dbContext.Missions.Where(x => x.Id == id).FirstOrDefault();

            if (mission == null) throw new Exception("Account does't exist!");

            mission.IsDeleted = true;

            //user.EmailAddress = model.EmailAddress

            mission.ModifiedDate = DateTime.UtcNow;
            _dbContext.Missions.Update(mission);
            _dbContext.SaveChanges();
            return "Account deleted!";
        }

        // int userId
        public async Task<IList<Missions>> ClientSideMissionList()
        {
            return await _dbContext.Missions
                .Include(m => m.City)
                .Include(m => m.Country)
               .Include(m => m.MissionTheme)
               .Include(m => m.MissionApplications)
                .Where(m => !m.IsDeleted && m.MissionApplications.Any(ma => !ma.IsDeleted)) 
                .OrderBy(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<bool> ApplyMission(AddMissionApplicationRequestModel model)
        {
            try
            {
                var mission = _dbContext.Missions.Where(x => x.Id == model.MissionId).FirstOrDefault();

                if (mission == null) throw new Exception("Mission not found");

                var application = _dbContext.MissionApplications.Where(x => x.MissionId == model.MissionId && x.UserId == model.UserId).FirstOrDefault();

                if (application != null) throw new Exception("Already applied!");

                MissionApplication app = new MissionApplication()
                {
                    UserId = model.UserId,
                    MissionId = model.MissionId,
                    AppliedDate = model.AppliedDate,
                    Seats = model.Sheet,
                    Status = model.Status,

                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                };

                mission.TotalSheets -= model.Sheet;

                await _dbContext.MissionApplications.AddAsync(app);
                _dbContext.Missions.Update(mission);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public List<MissionApplication> GetMissionApplicationList()
        //{
        //    return _dbContext.MissionApplications.Where(x => !x.IsDeleted).ToList();
        //}

        public async Task<List<MissionApplicationViewModel>> GetMissionApplicationList()
        {
            return await _dbContext.MissionApplications
                .Where(ma => !ma.IsDeleted) // filter out deleted applications
                .Include(ma => ma.Mission)                  // join with Missions
                    .ThenInclude(m => m.MissionTheme)       // join with MissionTheme through Mission
                .Include(ma => ma.User)                     // join with User
                .Select(ma => new MissionApplicationViewModel
                {
                    Id = ma.Id,
                    MissionId = ma.MissionId,
                    UserId = ma.UserId,
                    MissionTitle = ma.Mission.MissionTitle,           // from Missions
                    UserName = ma.User.FirstName + " " + ma.User.LastName, // from Users
                    ThemeName = ma.Mission.MissionTheme.ThemeName,    // from MissionThemes
                    AppliedDate = ma.AppliedDate,
                    Status = ma.Status,
                    Seats = ma.Seats,
                    //CreatedDate = ma.CreatedDate,
                    //ModifiedDate = ma.ModifiedDate,
                    IsDeleted = ma.IsDeleted
                }).ToListAsync();
        }




        public async Task<bool> MissionApplicationApprove(UpdateMissionApplicationModel missionApplication)
        {
            var tMissionApp = _dbContext.MissionApplications.Where(x => x.Id == missionApplication.Id).FirstOrDefault();

            if (tMissionApp == null) throw new Exception("Mission application not found");

            tMissionApp.Status = true;
            tMissionApp.ModifiedDate = DateTime.Now;

            _dbContext.MissionApplications.Update(tMissionApp);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MissionApplicationDelete(UpdateMissionApplicationModel missionApplication)
        {
            var tMissionApp = _dbContext.MissionApplications.Where(x => x.Id == missionApplication.Id).FirstOrDefault();

            if (tMissionApp == null) throw new Exception("Mission application not found");

            tMissionApp.IsDeleted = true;
            tMissionApp.ModifiedDate = DateTime.Now;

            _dbContext.MissionApplications.Update(tMissionApp);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
