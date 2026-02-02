
using Application.Common.Responses;

namespace Application.Common.Rules
{
    public static class RuleResultExtensions
    {
        public static ApiResponse<T> ToApiResponse<T>(this RuleResult result, T? response = default)
        {
            if (result.Success) return ApiResponse<T>.Success(response);

            return ApiResponse<T>.Fail(result.Error != null ? new List<string> { result.Error } : new List<string> { "Error desconocido" });
        }

        public static ApiResponse<object> ToApiResponse(this RuleResult result)
        {
            if (result.Success) return ApiResponse<object>.Success(new { message = "Operación exitosa" });

            return ApiResponse<object>.Fail(result.Error != null ? new List<string> { result.Error } : new List<string> { "Error desconocido" });
        }
    }
}
