using ContactManagement.Core.Aggregates;
using FastEndpoints;
using MediatR;
using System;
using ContactManagement.Core.ValueObjects;

namespace ContactManagement.BlazorShared.Models.ContactModels.Create;

public class CreateContactMapper : Mapper<CreateContactRequest, CreateContactResponse, Contact>
{
    // Hard-coding country code to only accept US numbers
    public override Contact ToEntity(CreateContactRequest request) => new(request.FirstName, request.LastName,
        request.Address, new PhoneNumberType("1", request.PhoneNumber, request.Extension), request.Age);

    public override CreateContactResponse FromEntity(Contact c) =>
        new(new ContactRecord(c.Id, c.FirstName, c.LastName, c.Address, c.PhoneNumber, c.Age));
}