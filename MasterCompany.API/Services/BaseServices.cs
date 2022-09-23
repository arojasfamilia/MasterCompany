using MasterCompany.API.DTOs;
using System.Text.Json;

namespace MasterCompany.API.Services
{
    public class BaseServices
    {
        private const string fileName = "Employee.txt";
        private const string deactivatedFileName = "DeactivatedEmployee.txt";
        protected static string fileRouteDirectory = $"{Directory.GetCurrentDirectory()}\\Data";
        protected List<EmployeeDTO> _employees { set; get; } = new List<EmployeeDTO>();
        protected List<EmployeeDTO> _deactivatedEmployees { set; get; } = new List<EmployeeDTO>();

        public BaseServices()
        {
            GetEmployees();
            GetDeactivatedEmployees();
        }

        public void GetEmployees()
        {
            try
            {
                TextReader reader = new StreamReader($"{fileRouteDirectory}\\{fileName}");
                var data = reader.ReadToEnd();
                reader.Close();

                if (data != null)
                {
                    var jsonResult = JsonSerializer.Deserialize<List<EmployeeDTO>>(data);

                    if (jsonResult != null)
                        _employees = jsonResult;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void GetDeactivatedEmployees()
        {
            try
            {
                TextReader reader = new StreamReader($"{fileRouteDirectory}\\{deactivatedFileName}");
                var data = reader.ReadToEnd();
                reader.Close();

                if (data != null)
                {
                    var jsonResult = JsonSerializer.Deserialize<List<EmployeeDTO>>(data);

                    if (jsonResult != null)
                        _deactivatedEmployees = jsonResult;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        public void SaveChanges()
        {
            TextWriter writer = new StreamWriter($"{fileRouteDirectory}\\{fileName}");
            writer.WriteLine(JsonSerializer.Serialize(_employees));
            writer.Close();
        }

        public void SaveDeactivatedChanges()
        {
            TextWriter writer = new StreamWriter($"{fileRouteDirectory}\\{deactivatedFileName}");
            writer.WriteLine(JsonSerializer.Serialize(_deactivatedEmployees));
            writer.Close();
        }
    }
}
