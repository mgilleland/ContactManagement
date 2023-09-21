using Ardalis.GuardClauses;
using Ardalis.SharedKernel;
using ContactManagement.Core.ValueObjects;

namespace ContactManagement.Core.Aggregates;

public class Contact : EntityBase, IAggregateRoot
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public AddressType Address { get; private set; }
    public PhoneNumberType PhoneNumber { get; private set; }
    public int Age { get; private set; }

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

    public void UpdateNames(string firstName, string lastName)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
    }

    public void UpdateAddress(AddressType address)
    {
        Address = Guard.Against.Null(address, nameof(address));
    }

    public void UpdatePhoneNumber(PhoneNumberType phoneNumber)
    {
        PhoneNumber = Guard.Against.Null(phoneNumber, nameof(phoneNumber));
    }

    public void UpdateAge(int age)
    {
        Age = Guard.Against.OutOfRange(age, nameof(age), 0, MaxAge);
    }

    public const int MaxNameLength = 50;
    public const int MaxAge = 135;
}