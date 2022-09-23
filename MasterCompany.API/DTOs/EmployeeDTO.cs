using System.Text.Json.Serialization;

namespace MasterCompany.API.DTOs
{
    public class EmployeeDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public double Salary { get; set; }
        public string Gender { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }

        public ServicesResult<bool> ValidateProperties()
        {
            var result = new ServicesResult<bool>();

            if (string.IsNullOrWhiteSpace(Document))
            {
                result.AddErrorMessage($"El campo [Documento] es requerido.");
                return result;
            }

            if (Document.Length != 11)
            {
                result.AddErrorMessage($"Este documento [{Document}] es invalido.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                result.AddErrorMessage($"El campo [Nombre] es requerido.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                result.AddErrorMessage($"El campo [Apellido] es requerido.");
                return result;
            }

            if (Salary <= 0)
            {
                result.AddErrorMessage($"El campo [Salario] es requerido.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(Gender))
            {
                result.AddErrorMessage($"El campo [Genero] es requerido.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(Position))
            {
                result.AddErrorMessage($"El campo [Posición] es requerido.");
                return result;
            }

            if (string.IsNullOrWhiteSpace(StartDate))
            {
                result.AddErrorMessage($"El campo [Fecha de Inicio] es requerido.");
                return result;
            }

            return result;
        }
    }
}
