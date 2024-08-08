using HospitalApiApp.DLL.Data;
using HospitalApiApp.DLL.Entities;
using HospitalApiApp.Dtos.DepartmentCreateDto;
using HospitalApiApp.Dtos.DoctorCreateDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public DoctorController(HospitalDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var doctors = await _context.Doctors
                            .ToListAsync();
            return Ok(doctors);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (existDoctor == null)
            {
                return NotFound();
            }
            return Ok(existDoctor);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(DoctorDtos doctorDtos)
        {
            if (await _context.Doctors.AnyAsync(g => g.Name.ToLower() == doctorDtos.Name.ToLower()))
            {
                return BadRequest("Dublicate Group Name");
            }
            Doctor doctor = new Doctor()
            {
                Name = doctorDtos.Name,
                Experience = doctorDtos.Experience,
            };
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpDelete("")]

        public async Task<IActionResult> Delete(int id)
        {
            var existDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (existDoctor == null) return NotFound();
            _context.Doctors.Remove(existDoctor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Doctor doctor)
        {

            var existDoctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            if (existDoctor == null)
            {
                return NotFound();
            }
            if (existDoctor.Name != doctor.Name && await _context.Doctors.AnyAsync(g => g.Name.ToLower() == doctor.Name.ToLower() && g.Id != id))
            {
                return BadRequest("Dublicate Deparment Name");
            }

            existDoctor.Name = doctor.Name;
            existDoctor.Experience = doctor.Experience;
            await _context.SaveChangesAsync();
            return Ok(existDoctor);
        }
    }
}
