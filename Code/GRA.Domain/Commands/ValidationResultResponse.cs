using FluentValidation;

namespace GRA.Domain.Commands
{
    public class ValidationResultResponse
    {
        public object? CustomState { get; set; }

        public string? ErrorCode { get; set; }

        public string? ErrorMessage { get; set; }

        public Dictionary<string, object>? FormattedMessagePlaceholderValues { get; set; }

        public string? PropertyName { get; set; }

        public Severity? Severity { get; set; }
    }
}
