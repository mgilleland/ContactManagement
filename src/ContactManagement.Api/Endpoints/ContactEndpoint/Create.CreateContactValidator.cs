using ContactManagement.BlazorShared.Models.ContactModels.Create;
using ContactManagement.Core.Aggregates;
using ContactManagement.Core.ValueObjects;
using FastEndpoints;
using FluentValidation;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class CreateContactValidator : Validator<CreateContactRequest>
{
    public CreateContactValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .MaximumLength(Contact.MaxNameLength);
        RuleFor(c => c.LastName)
            .NotEmpty()
            .MaximumLength(Contact.MaxNameLength);
        RuleFor(c => c.Line1)
            .NotEmpty()
            .MaximumLength(AddressType.MaxAddressLength);
        RuleFor(c => c.Line2)
            .MaximumLength(AddressType.MaxAddressLength);
        RuleFor(c => c.City)
            .NotEmpty()
            .MaximumLength(AddressType.MaxAddressLength);
        RuleFor(c => c.State)
            .Matches(AddressType.StateValidator);
        RuleFor(c => c.Zip)
            .Matches(AddressType.ZipValidator);
        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .Matches(PhoneNumberType.PhoneNumberValidator);
        RuleFor(c => c.Extension)
            .Matches(PhoneNumberType.ExtensionValidator);
        RuleFor(c => c.Age)
            .InclusiveBetween(0, Contact.MaxAge);
    }
}