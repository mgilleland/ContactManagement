using ContactManagement.Core.Aggregates;
using FastEndpoints;

namespace ContactManagement.BlazorShared.Models.ContactModels.GetById;

public class GetContactByIdMapper : Mapper<GetContactByIdRequest, GetContactByIdResponse, Contact>
{
    public override GetContactByIdResponse FromEntity(Contact c) =>
        new(new ContactRecord(c.Id, c.FirstName, c.LastName, c.Address, c.PhoneNumber, c.Age));
}