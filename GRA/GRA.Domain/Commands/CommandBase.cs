using FluentValidation.Results;
using GRA.Domain.Entities;
using Newtonsoft.Json;

namespace GRA.Domain.Commands
{
    public abstract class CommandCommonBase : IEntityBase
    {
        public int Id { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        private IEnumerable<ValidationResultResponse>? _validatorsFailures;

        [JsonProperty("failuresMessage")]
        [JsonIgnore]
        public IEnumerable<ValidationResultResponse>? ValidatorFailures
        {
            get { return _validatorsFailures; }
            private set
            {
                if (value == null || !value.Any())
                    this.IsValid = true;

                this._validatorsFailures = value;
            }
        }

        /// <summary>
        /// List of errors message.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<string?>? ErrorsMessage =>
            _validatorsFailures?.Select(f => f?.ErrorMessage);

        /// <summary>
        /// Start as false to wait the validation.
        /// </summary>
        [JsonIgnore]
        public bool IsValid { get; private set; } = false;

        /// <summary>
        /// Get the validation normalized.
        /// </summary>
        /// <param name="validationResult">ValidationResult rules.</param>
        /// <returns></returns>
        public ValidationResult GetValidationResultNormalized(ValidationResult validationResult)
        {
            this.ValidatorFailures = validationResult?.Errors?.Select(f => new ValidationResultResponse
            {
                FormattedMessagePlaceholderValues = f.FormattedMessagePlaceholderValues,
                PropertyName = f.PropertyName,
                CustomState = f.CustomState,
                ErrorMessage = f.ErrorMessage,
                ErrorCode = f.ErrorCode,
                Severity = f.Severity
            });

            return validationResult;
        }
    }

    public abstract class CommandBase : CommandCommonBase
    {
        /// <summary>
        /// Makes the validations about the rules to the properties.
        /// </summary>
        /// <returns></returns>
        public abstract ValidationResult Validator();
    }

    public abstract class CommandBase<Interface> : CommandCommonBase where Interface : class
    {
        public abstract ValidationResult Validator(Interface interfaceService);
    }
}
