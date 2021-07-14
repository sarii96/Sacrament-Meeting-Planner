using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sacrament_Meeting_Planner.Models
{
    public class SacramentMeetingPlan
    {
     
        public int SacramentMeetingPlanId { get; set; }


        public DateTime Date { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name = "Conducting leader")]
        public string BishopricName { get; set; }

        [Display(Name = "Opening song")]
        [Range(1, 341)]
        [Required]
        public string OpeningSong { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Invocation { get; set; }

        [Display(Name = "Sacrament Hymn")]
        [Range(1, 341)]
        [Required]
        public string SacramentHymn { get; set; }

        [Display(Name = "Intermediate Number")]
        public string IntermediateSong { get; set; } 

        [Display(Name = "Closing song")]
        [Range(1, 341)]
        [Required]
        public string ClosingSong { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Benediction { get; set; }

    }
}
