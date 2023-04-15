using GRA.Domain.Commands.MoviesFeature;
using GRA.Domain.Interfaces.Repositories.ReadOnly;

namespace GRA.Domain.Validations.MoviesFeature
{
    public class RegisterMovieValidator : MovieValidator<MovieCommand>
    {
        public RegisterMovieValidator(IMovieRepositoryReadOnly _movieRepositoryReadOnly)
                                     : base(_movieRepositoryReadOnly)
        {
            ValidateTitle();
            ValidateProducer();
            ValidateStudio();
            ValidateYear();
        }
    }
}
