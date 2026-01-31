
namespace Application.Common.Rules
{
    public class RuleResult
    {
        public bool Success { get; }
        public string? Error { get; }

        private RuleResult(bool success, string? error = null)
        {
            Success = success;
            Error = error;
        }

        public static RuleResult Ok() => new(true);

        public static RuleResult Fail(string error) => new(false, error);
    }
}
