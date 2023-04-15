using FluentValidation;
using GRA.Domain.Commands.MoviesFeature;
using GRA.Domain.Interfaces.Repositories.ReadOnly;

namespace GRA.Domain.Validations.MoviesFeature
{
    public abstract class MovieValidator<T> : AbstractValidator<T> where T : MovieCommand
    {
        private readonly string PREFIX_CODE = "MOVIE-";
        private readonly IMovieRepositoryReadOnly _movieRepositoryReadOnly;

        protected MovieValidator(IMovieRepositoryReadOnly movieRepositoryReadOnly)
        {
            this._movieRepositoryReadOnly = movieRepositoryReadOnly;
        }

        protected void ValidateFlowId()
        {
            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Identificador do Filme não foi informado.")
              .WithSeverity(Severity.Error)
              .WithErrorCode($"{PREFIX_CODE}001");
        }

        protected void ValidateTitle()
        {
            RuleFor(x => x.Title)
              .NotNull()
              .NotEmpty()
              .WithMessage("Campo de \"Título\" não foi informado.")
              .WithSeverity(Severity.Error)
              .WithErrorCode($"{PREFIX_CODE}002");

            RuleFor(x => x.Title)
              .MinimumLength(2)
              .MaximumLength(80)
              .WithMessage("Campo de \"Título\" deve conter no mínimo 2 e no máximo 80 caracteres.")
              .WithSeverity(Severity.Error)
              .WithErrorCode($"{PREFIX_CODE}003");
        }

        protected void ValidateStudio()
        {
            RuleFor(x => x.Studio)
              .NotNull()
              .NotEmpty()
              .WithMessage("Campo de \"Studio\" não foi informado.")
              .WithSeverity(Severity.Error)
              .WithErrorCode($"{PREFIX_CODE}004");

            RuleFor(x => x.Studio)
              .MinimumLength(2)
              .MaximumLength(80)
              .WithMessage("Campo de \"Studio\" deve conter no mínimo 2 e no máximo 80 caracteres.")
              .WithSeverity(Severity.Error)
              .WithErrorCode($"{PREFIX_CODE}005");
        }

        protected void ValidateProducer()
        {
            RuleFor(x => x.Producer)
              .NotNull()
              .NotEmpty()
              .WithMessage("Campo de \"Produtor\" não foi informado.")
              .WithSeverity(Severity.Error)
              .WithErrorCode($"{PREFIX_CODE}006");

            RuleFor(x => x.Producer)
              .MinimumLength(2)
              .MaximumLength(300)
              .WithMessage("Campo de \"Produtor\" deve conter no mínimo 2 e no máximo 300 caracteres.")
              .WithSeverity(Severity.Error)
              .WithErrorCode($"{PREFIX_CODE}007");
        }

        protected void ValidateYear()
        {
            const int yearOfFirstMovieLaunched = 1864;
            int yearInsaneForMovieToBeLaunched = DateTime.Now.Year + 100;

            RuleFor(x => x.Year)
              .GreaterThan(yearOfFirstMovieLaunched)                               
              .WithMessage($"Campo \"ano\" está inválido. O ano deve ser maior que {yearOfFirstMovieLaunched}.")
              .WithSeverity(Severity.Error)
              .WithErrorCode($"{PREFIX_CODE}008");

            RuleFor(x => x.Year)
              .LessThan(yearInsaneForMovieToBeLaunched)
              .WithMessage($"Campo \"ano\" de lançamento está muito fora das expectativas.")
              .WithSeverity(Severity.Error)
              .WithErrorCode($"{PREFIX_CODE}009");
        }
    }
}
