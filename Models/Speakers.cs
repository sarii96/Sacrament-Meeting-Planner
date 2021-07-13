using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sacrament_Meeting_Planner.Models
{
    public class Speakers
    {
        public int SpeakersId { get; set; }

        public string MemberName { get; set; }

        public string Topic { get; set; }

        public int SacramentMeetingPlanId { get; set; }

        public SacramentMeetingPlan SacramentMeetingPlan { get; set; }
    }
}
