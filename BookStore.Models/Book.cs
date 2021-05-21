using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public Book()
        {
            Orders = new List<Order>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MinLength(10)]
        public string Description { get; set; }
        
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Publisher { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string Assessment { get; set; }

        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal Prise { get; set; }
        
        [Required]
        [Range(0, (double)decimal.MaxValue)]
        public decimal RecommendedPrise { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int RevisionNumber { get; set; }
        
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}