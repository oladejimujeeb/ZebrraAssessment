using System;
using System.Collections.Generic;

namespace ProductWebAPI.Model
{
    public class Complaint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ComplaintTittle { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        
    }
}
