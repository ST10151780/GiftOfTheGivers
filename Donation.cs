using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GiftOfTheGivers.Models
{
    public class Donation
    {
        public int DonationId { get; set; }

        [Required]
        public string DonationType { get; set; } = string.Empty; // Money, Food, Clothing, Other

        [DataType(DataType.Currency)]
        [Precision(18, 2)]
        public decimal? Amount { get; set; } // Only for Money donations

        public string? Description { get; set; } // For Food/Clothing/Other

        public int? Quantity { get; set; } // Optional, kg of food or number of items

        public DateTime DonatedAt { get; set; } = DateTime.UtcNow;

        public string? DonorId { get; set; } // Optional if anonymous
        public ApplicationUser? Donor { get; set; }

        public string? ReceiptNumber { get; set; }
    }
}


