using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sacrament_Meeting_Planner.Models
{
    public class SacramentMeetingPlan
    {
        public int PlanId { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Conducting leader")]
        public string BishopricName { get; set; }

        [Display(Name = "Opening song")]
        public string OpeningSong { get; set; }

        public string Invocation { get; set; }

        [Display(Name = "Sacrament Hymn")]
        public string SacramentHymn { get; set; }

        [Display(Name = "Intermediate Number")]
        //It has to be optional. I add "?" but I am not sure
        public string? IntermediateSong { get; set; } 

        [Display(Name = "Closing song")]
        public string ClosingSong { get; set; }


        public string Benediction { get; set; }



    }
}
