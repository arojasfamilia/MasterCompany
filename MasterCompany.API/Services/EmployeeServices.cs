using MasterCompany.API.DTOs;
using System.Text.Json;

namespace MasterCompany.API.Services
{
    public class EmployeeServices : BaseServices, IEmployeeServices
    {
        public ServicesResult<List<EmployeeDTO>> GetAll()
        {
            var result = new ServicesResult<List<EmployeeDTO>>();

            try
            {
                if (_employees.Count == 0)
                {
                    result.AddErrorMessage("No se ha encontrado empleados.");
                    return result;
                }

                result.Data = _employees;
                return result;
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
                return result;
            }
        }

        public ServicesResult CreateEmployee(EmployeeDTO param)
        {
            var result = new ServicesResult();

            try
            {
                var ValidatePropertiesResult = param.ValidateProperties();

                if (!ValidatePropertiesResult.ExecutedSuccessfully)
                {
                    return ValidatePropertiesResult;
                }

                var existAnyEmployee = _employees
                    .Any(x => x.Document == param.Document);

                if (existAnyEmployee)
                {
                    result.AddErrorMessage($"Ya existe un empleado con este documento [{param.Document}].");
                    return result;
                }

                param.Gender = param.Gender[0].ToString().ToUpper();
                _employees.Add(param);

                SaveChanges();

                result.AddMessage("Empleado creado satisfactoriamente.");
                return result;
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
                return result;
            }
        }

        public ServicesResult<List<EmployeeDTO>> GetAllBySalaryRange(SalaryRangeDTO param)
        {
            var result = new ServicesResult<List<EmployeeDTO>>();

            try
            {
                var data = _employees
                    .Where(x => x.Salary >= param.From &&
                                x.Salary <= param.To)
                    .OrderBy(x => x.Salary)
                    .ToList();

                if (data is null)
                {
                    result.AddErrorMessage("No se ha encontrado empleados.");
                    return result;
                }

                result.Data = data;
                return result;
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
                return result;
            }
        }

        public ServicesResult<List<EmployeeDTO>> GetAllSkipingDuplicates()
        {
            var result = new ServicesResult<List<EmployeeDTO>>();

            try
            {
                var data = _employees
                    .GroupBy(g => new { g.Document })
                    .Select(x => new EmployeeDTO
                    {
                        Name = x.FirstOrDefault().Name,
                        LastName = x.FirstOrDefault().LastName,
                        Document = x.FirstOrDefault().Document,
                        Salary = x.FirstOrDefault().Salary,
                        Gender = x.FirstOrDefault().Gender,
                        Position = x.FirstOrDefault().Position,
                        StartDate = x.FirstOrDefault().StartDate
                    })
                    .ToList();

                if (data is null)
                {
                    result.AddErrorMessage("No se ha encontrado empleados.");
                    return result;
                }

                result.Data = data;
                return result;
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
                return result;
            }
        }

        public ServicesResult<List<EmployeeDTO>> SalaryIncrease()
        {
            var result = new ServicesResult<List<EmployeeDTO>>();
            var data = new List<EmployeeDTO>();

            try
            {
                data.AddRange(_employees);

                foreach (var employee in data)
                {
                    double toIncrease = 0;

                    if (employee.Salary > 100000)
                    {
                        toIncrease = employee.Salary * 0.25;
                        employee.Salary += toIncrease;
                        continue;
                    }

                    toIncrease = employee.Salary * 0.3;
                    employee.Salary += toIncrease;
                }

                result.Data = _employees;
                return result;
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
                return result;
            }
        }

        public ServicesResult<GenderPercentageDTO> GetGenderPercentage()
        {
            var result = new ServicesResult<GenderPercentageDTO>();
            var data = new GenderPercentageDTO();

            try
            {
                var men = _employees
                    .Where(x => x.Gender == "M")
                    .Count();

                var women = _employees
                    .Where(x => x.Gender == "F")
                    .Count();

                data.Men = getPercentage(men);
                data.Women = getPercentage(women);

                result.Data = data;
                return result;
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
                return result;
            }
        }

        private decimal getPercentage(decimal totalGender)
        {
            return decimal.Round(100 * (totalGender / _employees.Count), 2);
        }

        public ServicesResult DeleteEmployee(string document)
        {
            var result = new ServicesResult();

            try
            {
                var employeeToDelete = _employees
                    .FirstOrDefault(x => x.Document == document);

                if (employeeToDelete is null)
                {
                    result.AddErrorMessage($"No existe un empleado con este documento [{document}].");
                    return result;
                }

                _employees.Remove(employeeToDelete);

                SaveChanges();

                result.AddMessage("Empleado eliminado satisfactoriamente.");
                return result;
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
                return result;
            }
        }

        public ServicesResult DeactivateEmployee(string document)
        {
            var result = new ServicesResult();

            try
            {
                var employeeToDeactivate = _employees
                    .FirstOrDefault(x => x.Document == document);

                if (employeeToDeactivate is null)
                {
                    result.AddErrorMessage($"No existe un empleado con este documento [{document}].");
                    return result;
                }

                _deactivatedEmployees.Add(employeeToDeactivate);
                SaveDeactivatedChanges();

                _employees.Remove(employeeToDeactivate);
                SaveChanges();

                result.AddMessage("Empleado Desactivado satisfactoriamente.");
                return result;
            }
            catch (Exception ex)
            {
                result.AddErrorMessage(ex.Message);
                return result;
            }
        }
    }

    public interface IEmployeeServices
    {
        ServicesResult<List<EmployeeDTO>> GetAll();
        ServicesResult CreateEmployee(EmployeeDTO param);
        ServicesResult<List<EmployeeDTO>> GetAllBySalaryRange(SalaryRangeDTO param);
        ServicesResult<List<EmployeeDTO>> GetAllSkipingDuplicates();
        ServicesResult<List<EmployeeDTO>> SalaryIncrease();
        ServicesResult<GenderPercentageDTO> GetGenderPercentage();
        ServicesResult DeleteEmployee(string document);
        ServicesResult DeactivateEmployee(string document);
    }
}
