namespace MasterCompany.API.DTOs
{
    public class ServicesResult<TResult> : ServicesResult
    {
        public TResult Data { get; set; }
    }

    public class ServicesResult
    {
        public bool ExecutedSuccessfully { get; set; } = true;
        public string Message { get; set; }

        public void AddErrorMessage(string message)
        {
            ExecutedSuccessfully = false;
            Message = message;
        }

        public void AddMessage(string message)
        {
            Message = message;
        }
    }
}
