using GRA.Domain.Commands.MoviesFeature;
using GRA.Domain.Interfaces.Repositories.ReadOnly;

namespace GRA.Domain.Validations.MoviesFeature
{
    public class UpdateMovieValidator : MovieValidator<MovieCommand>
    {
        public UpdateMovieValidator(IMovieRepositoryReadOnly _movieRepositoryReadOnly)
                             : base(_movieRepositoryReadOnly)
        {
            ValidateId();
            ValidateTitle();
            ValidateProducer();
            ValidateStudio();
            ValidateYear();
        }
    }
}
