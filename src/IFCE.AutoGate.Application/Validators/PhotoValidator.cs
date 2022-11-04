using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace IFCE.AutoGate.Application.Validators;

public class PhotoValidator : AbstractValidator<IFormFile>
{
    private const int MaxSizeInMb = 2;

    private readonly IList<string> SupportedFileTypes =
        new List<string> { "image/jpeg", "image/jpg", "image/png" };

    public PhotoValidator()
    {
        RuleFor(f => f.Length)
            .ExclusiveBetween(0, 2 * 1024 * 1024)
            .WithMessage($"A foto não pode ultrapassar o tamanho de {MaxSizeInMb} MB")
            .When(f => f is not null);

        RuleFor(f => f.ContentType)
            .Must(filename => SupportedFileTypes.Contains(filename))
            .WithMessage("O formato da foto não é permitido (apenas .png, .jpg ou .jpeg)")
            .When(f => f is not null);
    }
}
