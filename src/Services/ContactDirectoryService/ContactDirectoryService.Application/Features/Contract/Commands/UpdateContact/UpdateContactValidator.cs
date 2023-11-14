using FluentValidation;

namespace ContactDirectoryService.Application.Features.Contract.Commands.UpdateContact
{
    public class UpdateContactValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("Firstname field cannot be empty.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("LastName field cannot be empty.");
            RuleFor(c => c.Company).NotEmpty().WithMessage("Company field cannot be empty.");
            RuleFor(c => c.FirstName).MaximumLength(100).WithMessage("Firstname must be max 100 character.");
            RuleFor(c => c.LastName).MaximumLength(100).WithMessage("LastName must be max 100 character.");
            RuleFor(c => c.Company).MaximumLength(200).WithMessage("Company must be max 200 character.");
        }
    }
}