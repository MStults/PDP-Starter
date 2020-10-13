namespace PDP.Web.API
{
    public class Response<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; } = true;

        public string Message { get; set; } = null;

        public static Response<T> Fail(string msg)
        {
            return new Response<T>
            {
                Message = msg,
                Success = false,
                Data = default
            };
        }

        public static Response<T> Succeed(T data, string msg = null)
        {
            return new Response<T>
            {
                Message = msg,
                Success = true,
                Data = data
            };
        }
    }
}