using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductWebAPI.Interface;
using ProductWebAPI.Model;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComplaintController : ControllerBase
    {
        private readonly IComplaintRepository _complaintRepository;

        public ComplaintController(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        [HttpGet("GetComplaint/{id}")]
        public async Task<IActionResult>GetComplaint(int id)
        {
            var complaint = await _complaintRepository.GetComplaintById(id);
            if (complaint is null) return NotFound($"Complaint with id:{id} not found ");
            return Ok(complaint);
        }

        // GET api/<ComplaintController>/5
        [HttpGet("AllComplaints")]
        public async Task<IActionResult> GetAllComplaint()
        {
            var complaint = await _complaintRepository.GetAllComplaints();
            if (complaint is null) return NotFound("No complaint received");
            return Ok(complaint);
        }

        // POST api/<ComplaintController>
        [HttpPost("LodgeComplaint")]
        public async Task<IActionResult> RegisterComplaint(ComplaintRequest request)
        {
            var complaint = new Complaint()
            {
                ComplaintTittle = request.ComplaintTittle,
                Date = request.Date,
                Message = request.Message,
                Name = request.Name,
                ProductName = request.ProductName,
                
            };
            var addRequest = await _complaintRepository.AddComplaint(complaint);
            if(addRequest)
            {
                return Ok("Product Add Successfully");
            }
            return BadRequest("Failed");
        }

        // PUT api/<ComplaintController>/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult>  UpdateComplaint(int id, ComplaintRequest request)
        {
           
           
            var update = await _complaintRepository.GetComplaintById(id);
            if(update is null)
            {
                return BadRequest("failed");
            }
            update.ComplaintTittle = request.ComplaintTittle;
            update.Date = request.Date != null ? request.Date : System.DateTime.UtcNow;
            update.Name = request.Name;
            update.ProductName = request.ProductName;
            update.Message = request.Message;
            return Ok(await _complaintRepository.UpdateComplaint(update));

        }

        // DELETE api/<ComplaintController>/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteComplaint(int id)
        {
            var delete = await _complaintRepository.DeleteComplaint(id);
            if(delete)
            {
                return Ok("Deleted Successfully");
            }
            return BadRequest("Complaint not found");
        }
    }
}
