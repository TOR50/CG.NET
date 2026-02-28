using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollegeApplication.Data;
using CollegeApplication.Models;
using System.Text;

namespace CollegeApplication.Controllers
{
    public class ApplicationFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApplicationForm
        public async Task<IActionResult> Index()
        {
            var applications = await _context.ApplicationForms.OrderByDescending(a => a.ApplicationDate).ToListAsync();
            return View(applications);
        }

        // GET: ApplicationForm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var applicationForm = await _context.ApplicationForms.FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (applicationForm == null) return NotFound();

            return View(applicationForm);
        }

        // GET: ApplicationForm/Create
        public IActionResult Create() => View();

        // POST: ApplicationForm/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,FatherName,MotherName,DateOfBirth,Gender,Email,PhoneNumber,Address,City,State,Pincode,Course,TenthPercentage,TwelfthPercentage")] ApplicationForm applicationForm)
        {
            if (ModelState.IsValid)
            {
                applicationForm.ApplicationDate = DateTime.Now;
                _context.Add(applicationForm);
                await _context.SaveChangesAsync();

                // Redirect to ID Card page after successful submission
                return RedirectToAction(nameof(IDCard), new { id = applicationForm.ApplicationId });
            }
            return View(applicationForm);
        }

        // GET: ApplicationForm/IDCard/5
        public async Task<IActionResult> IDCard(int? id)
        {
            if (id == null) return NotFound();

            var applicationForm = await _context.ApplicationForms.FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (applicationForm == null) return NotFound();

            return View(applicationForm);
        }

        // GET: ApplicationForm/DownloadIDCard/5
        public async Task<IActionResult> DownloadIDCard(int? id)
        {
            if (id == null) return NotFound();

            var app = await _context.ApplicationForms.FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (app == null) return NotFound();

            // Generate simple HTML-based PDF using browser print
            var studentId = $"LPU{app.ApplicationDate.Year}{app.ApplicationId:D4}";
            var initials = string.Join("", app.FullName.Split(' ').Select(n => n.Length > 0 ? n[0].ToString().ToUpper() : ""));

            var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>LPU Student ID Card - {app.FullName}</title>
    <style>
        * {{ margin: 0; padding: 0; box-sizing: border-box; }}
        body {{ font-family: Arial, sans-serif; padding: 20px; background: #f0f0f0; }}
        .card {{ width: 350px; margin: 0 auto; background: white; border-radius: 15px; overflow: hidden; box-shadow: 0 4px 20px rgba(0,0,0,0.2); }}
        .header {{ background: linear-gradient(135deg, #4f46e5, #06b6d4); color: white; padding: 20px; text-align: center; }}
        .header h2 {{ font-size: 18px; margin-bottom: 5px; }}
        .header p {{ font-size: 12px; opacity: 0.9; }}
        .body {{ padding: 20px; }}
        .photo {{ width: 80px; height: 80px; background: linear-gradient(135deg, #4f46e5, #06b6d4); border-radius: 50%; margin: 0 auto 15px; display: flex; align-items: center; justify-content: center; color: white; font-size: 28px; font-weight: bold; }}
        .row {{ display: flex; justify-content: space-between; padding: 8px 0; border-bottom: 1px dashed #e0e0e0; }}
        .row:last-child {{ border: none; }}
        .label {{ color: #666; font-size: 12px; }}
        .value {{ color: #333; font-weight: 600; font-size: 13px; }}
        .footer {{ background: #f8f8f8; padding: 15px; text-align: center; }}
        .student-id {{ font-family: 'Courier New', monospace; font-size: 18px; font-weight: bold; color: #4f46e5; letter-spacing: 2px; }}
        .app-id {{ font-size: 11px; color: #999; margin-top: 5px; }}
        @@media print {{ body {{ background: white; }} .card {{ box-shadow: none; }} }}
    </style>
</head>
<body>
    <div class='card'>
        <div class='header'>
            <h2>🎓 LPU Student ID Card</h2>
            <p>Lovely Professional University</p>
        </div>
        <div class='body'>
            <div class='photo'>{initials}</div>
            <div class='row'><span class='label'>Name</span><span class='value'>{app.FullName}</span></div>
            <div class='row'><span class='label'>Course</span><span class='value'>{app.Course}</span></div>
            <div class='row'><span class='label'>Email</span><span class='value'>{app.Email}</span></div>
            <div class='row'><span class='label'>Phone</span><span class='value'>{app.PhoneNumber}</span></div>
            <div class='row'><span class='label'>DOB</span><span class='value'>{app.DateOfBirth:dd MMM yyyy}</span></div>
        </div>
        <div class='footer'>
            <div class='student-id'>{studentId}</div>
            <div class='app-id'>Application #{app.ApplicationId}</div>
        </div>
    </div>
    <script>window.onload = function() {{ window.print(); }}</script>
</body>
</html>";

            return Content(html, "text/html", Encoding.UTF8);
        }

        // GET: ApplicationForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var applicationForm = await _context.ApplicationForms.FindAsync(id);
            if (applicationForm == null) return NotFound();

            return View(applicationForm);
        }

        // POST: ApplicationForm/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicationId,FullName,FatherName,MotherName,DateOfBirth,Gender,Email,PhoneNumber,Address,City,State,Pincode,Course,TenthPercentage,TwelfthPercentage,ApplicationDate")] ApplicationForm applicationForm)
        {
            if (id != applicationForm.ApplicationId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationForm);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Application updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ApplicationForms.Any(e => e.ApplicationId == id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationForm);
        }

        // GET: ApplicationForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var applicationForm = await _context.ApplicationForms.FirstOrDefaultAsync(m => m.ApplicationId == id);
            if (applicationForm == null) return NotFound();

            return View(applicationForm);
        }

        // POST: ApplicationForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationForm = await _context.ApplicationForms.FindAsync(id);
            if (applicationForm != null)
            {
                _context.ApplicationForms.Remove(applicationForm);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Application deleted successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
