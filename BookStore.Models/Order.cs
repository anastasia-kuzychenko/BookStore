using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    public class Order
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int BookCount { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        
        [Required]
        public DateTime DeliveryDate { get; set; }
        
        [Required]
        public PaymentType PaymentType { get; set; }

        [Required]
        public OrderState State { get; set; }

        [NotMapped]
        public decimal Sum => (Book?.Prise ?? 0) * BookCount;

        public Guid BookId { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Book Book { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Customer Customer { get; set; }
    }
}