using HospitalApiApp.DLL.Data;
using HospitalApiApp.DLL.Entities;
using HospitalApiApp.Dtos;
using HospitalApiApp.Dtos.DepartmentCreateDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HospitalApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly HospitalDbContext _context;

        public DepartmentController(HospitalDbContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var departments = await _context.Departments
                            .Include("Doctors")
                            .ToListAsync();
            return Ok(departments);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var existGroup = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (existGroup == null)
            {
                return NotFound();
            }
            return Ok(existGroup);
        }
        [HttpPost("")]
        public async Task<IActionResult> Create(DepartmentDtos departmentDtos)
        {
            if (await _context.Departments.AnyAsync(g => g.Name.ToLower() == departmentDtos.Name.ToLower()))
            {
                return BadRequest("Dublicate Group Name");
            }
            Department department = new Department()
            {
                Name = departmentDtos.Name,
                Limit = departmentDtos.Limit,
            };
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }
        [HttpDelete("")]

        public async Task<IActionResult> Delete(int id)
        {
            var existDepartment = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (existDepartment == null) return NotFound();
            _context.Departments.Remove(existDepartment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Department department)
        {

            var existdepartment = await _context.Departments.FirstOrDefaultAsync(g => g.Id == id);
            if (existdepartment == null)
            {
                return NotFound();
            }
            if (existdepartment.Name != department.Name && await _context.Departments.AnyAsync(g => g.Name.ToLower() == department.Name.ToLower() && g.Id != id))
            {
                return BadRequest("Dublicate Deparment Name");
            }

            existdepartment.Name = department.Name;
            existdepartment.Limit = department.Limit;
            await _context.SaveChangesAsync();
            return Ok(existdepartment);
        }
    }
        
    }
