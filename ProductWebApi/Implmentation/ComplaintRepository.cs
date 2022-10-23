using Microsoft.EntityFrameworkCore;
using ProductWebAPI.AppContext;
using ProductWebAPI.Interface;
using ProductWebAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWebAPI.Implmentation
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ApplicationContext _context;

        public ComplaintRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> AddComplaint(Complaint complaint)
        {
            await _context.Complaintes.AddAsync(complaint);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteComplaint(int id)
        {
            var complaint = await _context.Complaintes.FindAsync(id);
            if(complaint != null)
            {
                _context.Complaintes.Remove(complaint);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Complaint>> GetAllComplaints()
        {
            return await _context.Complaintes.ToListAsync();
        }

        public async Task<Complaint> GetComplaintById(int id)
        {
            return await _context.Complaintes.FindAsync(id);
        }

        public async Task<Complaint> UpdateComplaint(Complaint complaint)
        {
            var update = _context.Complaintes.Update(complaint);
            await _context.SaveChangesAsync();
            return update.Entity;
        }
    }
}
