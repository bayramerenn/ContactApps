using FluentValidation;

namespace ContactDirectoryService.Application.Features.ContactInformations.Command
{
    public class CreateContactInformationValitor : AbstractValidator<CreateContactInformationCommand>
    {
        public CreateContactInformationValitor()
        {
            RuleFor(ci => ci.Content).NotEmpty().WithMessage("Content field cannot be empty.");
            RuleFor(ci => ci.Content).MaximumLength(100).WithMessage("Content must be max 100 charachter.");

            RuleFor(ci => ci.ContactType)
                .IsInEnum()
                .WithMessage("Invalid ContactType value. Correct values: Phone, Email, Location");
        }
    }
}