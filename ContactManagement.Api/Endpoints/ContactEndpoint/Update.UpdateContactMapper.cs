using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.BlazorShared.Models.ContactModels.Update;
using ContactManagement.Core.Aggregates;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class UpdateContactMapper : Mapper<UpdateContactRequest, UpdateContactResponse, Contact>
{
    public override Contact ToEntity(UpdateContactRequest request) => new(request.FirstName, request.LastName,
        request.Address, request.PhoneNumber, request.Age);

    public override UpdateContactResponse FromEntity(Contact c) => new(
        new ContactDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Line1 = c.Address.Line1,
            Line2 = c.Address.Line2,
            City = c.Address.City,
            State = c.Address.State,
            Zip = c.Address.Zip,
            Number = c.PhoneNumber.PhoneNumber,
            Extension = c.PhoneNumber.Extension,
            Age = c.Age
        });
}