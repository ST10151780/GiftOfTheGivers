using System;
using System.ComponentModel.DataAnnotations;

namespace GiftOfTheGivers.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }

        // Link to Incident
        [Required]
        public int IncidentId { get; set; }
        public Incident Incident { get; set; }

        // Link to Volunteer
        [Required]
        public int VolunteerProfileId { get; set; }       // foreign key
        public VolunteerProfile Volunteer { get; set; }  // navigation property

        public bool Completed { get; set; } = false;

        public DateTime AssignedOn { get; set; } = DateTime.Now;

        // Optional: proof (photo/report)
        public string ProofFilePath { get; set; }
    }
}

