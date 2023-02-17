using FluentValidation;
using JobFinder.Models;

namespace JobFinder.Validators;

public class JobsValidator: AbstractValidator<Job>
{
    public JobsValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(200).WithMessage("Description is required and must be less than 200 characters");
        RuleFor(x => x.Location).Must(a => a?.ToLower().Contains("street") == true).WithMessage("Location requires a street name");
        RuleFor(x => x.Company).NotEmpty().WithMessage("Company is required");
        RuleFor(x => x.Contact).NotEmpty().WithMessage("Contact is required");
        RuleFor(x => x.PaymentPerHour).NotEmpty().WithMessage("Payment per hour is required");
    }
}