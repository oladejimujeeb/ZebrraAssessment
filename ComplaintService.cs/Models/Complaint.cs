using System;

namespace ComplaintService.cs.Models
{
    public class Complaint
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string ComplaintTittle { get; set; }
        public string Message { get; set; }
        public int ProductId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string ProductName { get; set; }
    }
}
