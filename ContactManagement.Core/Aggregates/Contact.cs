using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using ContactManagement.Core.ValueObjects;

namespace ContactManagement.Core.Aggregates;

public class Contact : EntityBase, IAggregateRoot
{
    public string FirstName { get; }
    public string LastName { get; }
    public AddressType Address { get; }
    public PhoneNumberType PhoneNumber { get; }
    public int Age { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Contact() { }  // For EF Only
#pragma warning restore CS8618

    public Contact(string firstName, string lastName, AddressType address, PhoneNumberType phoneNumber, int age)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        Address = Guard.Against.Null(address, nameof(address));
        PhoneNumber = Guard.Against.Null(phoneNumber, nameof(phoneNumber));
        Age = Guard.Against.OutOfRange(age, nameof(age), 0, MaxAge);
    }

    public const int MaxNameLength = 50;
    public const int MaxAge = 135;
}