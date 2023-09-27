using ContactManagement.BlazorShared.Models.ContactModels;
using ContactManagement.Core.Aggregates;
using FastEndpoints;

namespace ContactManagement.Api.Endpoints.ContactEndpoint;

public class ContactMapper : Mapper<ContactDto, ContactDto, Contact>, IResponseMapper
{
    public override ContactDto FromEntity(Contact c)
    {
        return new ContactDto
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
        };
    }
}