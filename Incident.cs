using System;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Models
{
    public class Incident
    {
        public int IncidentId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Severity { get; set; }

        // Link to the user who reported
        public string ReportedById { get; set; }
        public ApplicationUser ReportedBy { get; set; }

        // Use ReportedOn (not ReportedAt)
        public DateTime ReportedOn { get; set; } = DateTime.Now;
    }
}

