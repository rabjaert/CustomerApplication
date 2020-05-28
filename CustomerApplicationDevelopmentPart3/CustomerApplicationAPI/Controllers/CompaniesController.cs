using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerApplication.DataAccess.Data;
using CustomerApplication.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace CustomerApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly DataContext _context;

        public CompaniesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await _context.Companies.ToListAsync();
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // PUT: api/Companies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // GET api/Companies/RelatedEmployees/2
        [HttpGet("RelatedEmployees/{id}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesFromCompany(int id)
        {

            IList<Company> companyList = _context.Companies.Include(p => p.Employees).ToList();
            List<Employee> returnedEmployees = new List<Employee>();

            foreach (var companylist in companyList)
            {
                if (companylist.Id == id)
                {
                    foreach (var employee in companylist.Employees) {

                        returnedEmployees.Add(employee);
                    }
                
                
                }
            
            }

            if (returnedEmployees == null) {

                return NotFound();
            }
            return returnedEmployees;

        }

         // GET api/Companies/RelatedInventories/2
        [HttpGet("RelatedInventories/{id}")]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventoryFromCompany(int id)
        {

            IList<Company> companyList = _context.Companies.Include(p => p.Inventories).ToList();
            List<Inventory> returnedInventory = new List<Inventory>();

            foreach (var companylist in companyList)
            {
                if (companylist.Id == id)
                {
                    foreach (var inventory in companylist.Inventories) {

                        returnedInventory.Add(inventory);
                    }
                
                
                }
            
            }

            if (returnedInventory == null) {

                return NotFound();
            }
            return returnedInventory;

        }
        [AllowAnonymous]
        //GET: api/Companies/GetCompanyCount
        [HttpGet("GetCompanyCount")]
        public int GetCompanyCount()
        {
            return _context.Companies.Count();
        }

        // POST: api/Companies
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return company;
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    
    
    
    
    
    
    
    }
}
