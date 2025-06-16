using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission.Repositories.IRepositories;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;

namespace Mission.Repositories.Repositories
{
    public class MissionRepository(MissionDbContext dbContext) : IMissionRepository
    {
        public List<Missions> GetMissionList()
        {
            return dbContext.Missions.Where(x => !x.IsDeleted).ToList();
        }

        public async Task<string> AddMission(AddMissionRequestModel model)
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
                MissionOrganisationName = "",
                MissionOrganisationDetail = "",
                MissionType = "",
                MissionDocuments = "",
                MissionAvailability = "",
                MissionVideoUrl = "",


                IsDeleted = false,
                CreatedDate = DateTime.Now,
            };
            await dbContext.Missions.AddAsync(missions);
            dbContext.SaveChanges();

            return "Added!";
        }
    }
}
