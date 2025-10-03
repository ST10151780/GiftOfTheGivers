using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Models
{
    public class VolunteerProfile
    {
        public int VolunteerProfileId { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required, StringLength(500)]
        public string Skills { get; set; }

        [Required, StringLength(200)]
        public string Availability { get; set; }

        public bool Approved { get; set; } = false;

        // NEW: Collection of assigned tasks
        public ICollection<Assignment>? Assignments { get; set; }
    }
}

