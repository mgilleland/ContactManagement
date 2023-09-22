using ContactManagement.Core.Aggregates;
using ContactManagement.Core.ValueObjects;
using FastEndpoints;

namespace ContactManagement.BlazorShared.Models.ContactModels.Update;

public class UpdateContactMapper : Mapper<UpdateContactRequest, UpdateContactResponse, Contact>
{
    public override Contact ToEntity(UpdateContactRequest request) => new(request.FirstName, request.LastName,
        request.Address, new PhoneNumberType(request.PhoneNumber, request.Extension), request.Age);

    public override UpdateContactResponse FromEntity(Contact c) =>
        new(new ContactRecord(c.Id, c.FirstName, c.LastName, c.Address, c.PhoneNumber, c.Age));
}