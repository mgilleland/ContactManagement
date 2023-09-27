using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.BlazorShared.Models.ContactModels.GetById;
using ContactManagement.Core.Aggregates;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class GetContactByIdMapper : Mapper<GetContactByIdRequest, GetContactByIdResponse, Contact>
{
    public override GetContactByIdResponse FromEntity(Contact c) => new(
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