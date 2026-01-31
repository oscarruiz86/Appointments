using Application.Common.Exceptions;

namespace Application.Common.Rules
{
    public static class RuleChecker
    {
        public static async Task CheckAsync(params IRule[] rules)
        {
            foreach (var rule in rules)
            {
                var result = await rule.CheckAsync();

                if (!result.Success)
                    throw new BusinessRuleException(result.Error!);
            }
        }
    }
}
