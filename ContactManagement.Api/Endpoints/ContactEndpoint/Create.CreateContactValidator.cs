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
        RuleFor(c => c.Address.Line1)
            .MaximumLength(AddressType.MaxAddressLength);
        RuleFor(c => c.Address.Line2)
            .MaximumLength(AddressType.MaxAddressLength);
        RuleFor(c => c.Address.City)
            .MaximumLength(AddressType.MaxAddressLength);
        RuleFor(c => c.Address.State)
            .Matches(AddressType.StateValidator);
        RuleFor(c => c.Address.Zip)
            .Matches(AddressType.ZipValidator);
        RuleFor(c => c.PhoneNumber.PhoneNumber)
            .Matches(PhoneNumberType.PhoneNumberValidator);
        RuleFor(c => c.PhoneNumber.Extension)
            .Matches(PhoneNumberType.ExtensionValidator);
        RuleFor(c => c.Age)
            .NotEmpty()
            .InclusiveBetween(0, Contact.MaxAge);
    }

}