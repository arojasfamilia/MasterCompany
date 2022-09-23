using MasterCompany.API.DTOs;
using MasterCompany.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MasterCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeServices.GetAll();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateEmployee(EmployeeDTO param)
        {
            var result = _employeeServices.CreateEmployee(param);

            return Ok(result);
        }

        [HttpGet("by-salary-range")]
        public IActionResult GetAllBySalaryRange([FromQuery, Required] SalaryRangeDTO param)
        {
            var result = _employeeServices.GetAllBySalaryRange(param);

            return Ok(result);
        }

        [HttpGet("skiping-duplicates")]
        public IActionResult GetAllSkipingDuplicates()
        {
            var result = _employeeServices.GetAllSkipingDuplicates();

            return Ok(result);
        }

        [HttpGet("salary-increase")]
        public IActionResult SalaryIncrease()
        {
            var result = _employeeServices.SalaryIncrease();

            return Ok(result);
        }

        [HttpGet("gender-percentage")]
        public IActionResult GetGenderPercentage()
        {
            var result = _employeeServices.GetGenderPercentage();

            return Ok(result);
        }

        [HttpGet("delete-employee")]
        public IActionResult DeleteEmployee(string document)
        {
            var result = _employeeServices.DeleteEmployee(document);

            return Ok(result);
        }

        [HttpGet("deactivate-employee")]
        public IActionResult DeactivateEmployee(string document)
        {
            var result = _employeeServices.DeactivateEmployee(document);

            return Ok(result);
        }
    }
}
