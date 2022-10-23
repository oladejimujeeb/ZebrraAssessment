using System;
using System.ComponentModel.DataAnnotations;

namespace ProductWebAPI.Model
{
    public class ComplaintRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ComplaintTittle { get; set; }
        [Required]
        public string Message { get; set; }
        
        public int ProductId { get; set; }
        public DateTime Date { get; set; }= DateTime.UtcNow;
        [Required]
        public string ProductName { get; set; }
    }
}
