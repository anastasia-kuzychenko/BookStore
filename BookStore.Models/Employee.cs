using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Employee
    {
        public Employee()
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
        [MinLength(2)]
        [MaxLength(50)]
        public string Surname { get; set; }
        
        [Required]
        [Phone]
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")]
        public string Phone { get; set; }

        [Required]
        public DateTime Birthday { get; set; }
        
        [Required]
        public DateTime FirstDay { get; set; }
        
        [Required]
        public EmployeePosition Position { get; set; }
        
        [NotMapped]
        public string FullName => string.Format("{0} {1}", Name, Surname);

        public virtual List<Order> Orders { get; set; }
    }
}