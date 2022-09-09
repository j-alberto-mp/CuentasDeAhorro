namespace CuentasAhorro.Services.Wrappers
{
    public class Response<T>
    {
        public Response() { }

        public Response(T data, string? message = null)
        {
            Correct = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Correct = false;
            Message = message;
        }

        public bool Correct { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
    }
}
