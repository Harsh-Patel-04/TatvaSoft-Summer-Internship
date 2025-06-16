using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Models
{
    public class MissionApplicationViewModel
    {
        public int Id { get; set; }
        public int MissionId { get; set; }
        public int UserId { get; set; }
        public string MissionTitle { get; set; }
        public string UserName { get; set; }
        public string ThemeName { get; set; }
        public DateTime AppliedDate { get; set; }
        public bool Status { get; set; }
        public int Seats { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
