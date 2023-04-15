using FluentValidation.Results;
using GRA.Domain.Interfaces.Repositories.ReadOnly;
using GRA.Domain.Validations.MoviesFeature;

namespace GRA.Domain.Commands.MoviesFeature
{
    public class UpdateMovieCommand : MovieCommand
    {
        public override ValidationResult Validator(IMovieRepositoryReadOnly interfaceService)
        {
            var validate = new UpdateMovieValidator(interfaceService).Validate(this);
            return GetValidationResultNormalized(validate);
        }
    }
}
