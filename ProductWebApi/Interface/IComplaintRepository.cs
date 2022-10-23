using ProductWebAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWebAPI.Interface
{
    public interface IComplaintRepository
    {
        Task<Complaint> GetComplaintById(int id);
        Task<bool>AddComplaint(Complaint complaint);
        Task<bool>DeleteComplaint(int id);
        Task<Complaint> UpdateComplaint(Complaint complaint);
        Task<IEnumerable<Complaint>> GetAllComplaints();    
           
    }
}
