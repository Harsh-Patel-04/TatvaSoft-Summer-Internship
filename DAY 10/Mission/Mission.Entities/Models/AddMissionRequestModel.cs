using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Models
{
    public class AddMissionRequestModel
    {
        public string MissionTitle { get; set; }

        public string MissionDescription { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public int MissionThemeId { get; set; }

        public string MissionSkillId { get; set; }
        public string MissionImages { get; set; }
        public int CountryId { get; set; }

        public int CityId { get; set; }
        public int? TotalSheets { get; set; }
    }
}
