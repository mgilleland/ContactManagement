﻿using ContactManagement.Core.ValueObjects;

namespace ContactManagement.BlazorShared.Models.ContactModels.Update;

public class UpdateContactRequest
{
    public const string Route = "/Contact/{ContactId:int}";
    public static string BuildRoute(int contactId) => Route.Replace("{ContactId:int}", contactId.ToString());

    public int ContactId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Line1 { get; init; }
    public string? Line2 { get; init; }
    public required string City { get; init; }
    public required string State { get; init; }
    public required string Zip { get; init; }
    public required string PhoneNumber { get; init; }
    public string? Extension { get; init; }
    public int Age { get; init; }

}