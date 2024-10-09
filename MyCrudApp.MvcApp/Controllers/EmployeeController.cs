using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyCrudApp.MvcApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EmployeeController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Salary")] Employee employee, IFormFile? ProfilePhoto)
        {
            if (ModelState.IsValid)
            {
                if (ProfilePhoto != null && ProfilePhoto.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfilePhoto.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ProfilePhoto.CopyToAsync(fileStream);
                    }

                    employee.ProfilePhotoPath = "/uploads/" + uniqueFileName;
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Salary")] Employee employee, IFormFile? ProfilePhoto)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEmployee = await _context.Employees.FindAsync(id);
                    if (existingEmployee == null)
                    {
                        return NotFound();
                    }

                    existingEmployee.Name = employee.Name;
                    existingEmployee.Salary = employee.Salary;

                    if (ProfilePhoto != null && ProfilePhoto.Length > 0)
                    {
                        // Delete existing photo if exists
                        if (!string.IsNullOrEmpty(existingEmployee.ProfilePhotoPath))
                        {
                            string existingFilePath = Path.Combine(_hostEnvironment.WebRootPath, existingEmployee.ProfilePhotoPath.TrimStart('/'));
                            if (System.IO.File.Exists(existingFilePath))
                            {
                                System.IO.File.Delete(existingFilePath);
                            }
                        }

                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfilePhoto.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ProfilePhoto.CopyToAsync(fileStream);
                        }

                        existingEmployee.ProfilePhotoPath = "/uploads/" + uniqueFileName;
                    }

                    _context.Update(existingEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        /// <summary>
        /// Delete Employee Method using HTTP GET.
        /// </summary>
        /// <param name="id">Accepts the int Employee Id.</param>
        /// <returns>Returns the employee view.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                // Delete profile photo if exists
                if (!string.IsNullOrEmpty(employee.ProfilePhotoPath))
                {
                    string filePath = Path.Combine(_hostEnvironment.WebRootPath, employee.ProfilePhotoPath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
