
namespace Application.Common.Responses
{
    public class ApiResponse<T>
    {
        public string TraceId { get; set; } = Guid.NewGuid().ToString();
        public bool IsSuccess { get; set; }
        public T? Response { get; set; }
        public List<string>? ErrorMessages { get; set; }

        public static ApiResponse<T> Success(T response) =>
            new() { IsSuccess = true, Response = response };

        public static ApiResponse<T> Fail(IEnumerable<string> errors) =>
            new() { IsSuccess = false, ErrorMessages = errors.ToList() };

        public static ApiResponse<T> Fail(string error) =>
            Fail(new List<string> { error });
    }
}
