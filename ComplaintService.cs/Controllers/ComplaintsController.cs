using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComplaintService.cs.Context;
using ComplaintService.cs.Models;
using ComplaintService.cs.Interface;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ComplaintService.cs.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IGenerateToken _token;

        public ComplaintsController(ApplicationContext context, IGenerateToken token)
        {
            _context = context;
            _token = token; 
        }

 
        public async Task<IActionResult> Index()
        {
            var token = await _token.GetToken("Complain");
            using HttpClient client = new();
            client.SetBearerToken(token.AccessToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("https://localhost:44319/api/Complaint/AllComplaints");
            if (response.IsSuccessStatusCode)
            {
                var responseModel = await response.Content.ReadAsStringAsync();
                var complaintReport = JsonConvert.DeserializeObject<List<Complaint>>(responseModel);
                return View(complaintReport);
            }
            return new NotFoundResult();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var token = await _token.GetToken("Complain");
            using HttpClient client = new();
            client.SetBearerToken(token.AccessToken);
            var response = await client.GetAsync($"https://localhost:44319/api/Complaint/GetComplaint/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseModel = await response.Content.ReadAsStringAsync();
                var complaintReport = JsonConvert.DeserializeObject<Complaint>(responseModel);
                return View(complaintReport);
            }

            return NotFound();
        }
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,ComplaintTittle,Message,ProductId,Date,ProductName")] Complaint request)
        {

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(new
                {

                    ComplaintTittle = request.ComplaintTittle,
                    Date = request.Date,
                    Message = request.Message,
                    Name = request.Name,
                    ProductName = request.ProductName,
                }), Encoding.UTF8, "application/json");
                var token = await _token.GetToken("Complain");
                using HttpClient client = new();
                client.SetBearerToken(token.AccessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync("https://localhost:44319/api/Complaint/LodgeComplaint", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
               
            }
            return View(request);
        }

    
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var token = await _token.GetToken("Complain");
            using HttpClient client = new();
            client.SetBearerToken(token.AccessToken);
            var response = await client.GetAsync($"https://localhost:44319/api/Complaint/GetComplaint/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseModel = await response.Content.ReadAsStringAsync();
                var complaintReport = JsonConvert.DeserializeObject<Complaint>(responseModel);
                return View(complaintReport);
            }

            return NotFound();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ComplaintTittle,Message,ProductId,Date,ProductName")] Complaint request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(new
                {

                    ComplaintTittle = request.ComplaintTittle,
                    Date = request.Date,
                    Message = request.Message,
                    Name = request.Name,
                    ProductName = request.ProductName,
                }), Encoding.UTF8, "application/json");
                var token = await _token.GetToken("Complain");
                using HttpClient client = new();
                client.SetBearerToken(token.AccessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PutAsync($"https://localhost:44319/api/Complaint/Update/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(request);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var token = await _token.GetToken("Complain");
            using HttpClient client = new();
            client.SetBearerToken(token.AccessToken);
            var response = await client.GetAsync($"https://localhost:44319/api/Complaint/GetComplaint/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseModel = await response.Content.ReadAsStringAsync();
                var complaintReport = JsonConvert.DeserializeObject<Complaint>(responseModel);
                return View(complaintReport);
            }

            return NotFound();
        }

     
        [HttpGet]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var token = await _token.GetToken("Complain");
            using HttpClient client = new();
            client.SetBearerToken(token.AccessToken);
            var response = await client.DeleteAsync($"https://localhost:44319/api/Complaint/Delete/{id}");
            if (response.IsSuccessStatusCode)
            {
               
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }

        private bool ComplaintExists(int id)
        {
            return _context.Complaints.Any(e => e.Id == id);
        }
    }
}
