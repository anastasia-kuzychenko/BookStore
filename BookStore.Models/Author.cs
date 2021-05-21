using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Author
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Surname { get; set; }
        
        [Required]
        [MinLength(5)]
        public string Info { get; set; }
        
        [Required]
        public DateTime Birthday { get; set; }
        
        public DateTime? Death { get; set; }
        
        [NotMapped]
        public string FullName => string.Format("{0} {1}", Name, Surname);
        
        public virtual List<Book> Books { get; set; }
    }
}