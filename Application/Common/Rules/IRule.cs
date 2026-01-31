
namespace Application.Common.Rules
{
    public interface IRule
    {
        Task<RuleResult> CheckAsync();
    }
}
