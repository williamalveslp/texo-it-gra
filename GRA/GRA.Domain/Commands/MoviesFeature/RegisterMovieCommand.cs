using GRA.Domain.Interfaces.Repositories.ReadOnly;
using GRA.Domain.Validations.MoviesFeature;
using FluentValidation.Results;

namespace GRA.Domain.Commands.MoviesFeature
{
    public class RegisterMovieCommand : MovieCommand
    {
        public override ValidationResult Validator(IMovieRepositoryReadOnly interfaceService)
        {
            var validate = new RegisterMovieValidator(interfaceService).Validate(this);
            return GetValidationResultNormalized(validate);
        }
    }
}
