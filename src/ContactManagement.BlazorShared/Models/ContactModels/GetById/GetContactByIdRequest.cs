namespace ContactManagement.BlazorShared.Models.ContactModels.GetById;

public class GetContactByIdRequest
{
    public const string Route = "/Contacts/{ContactId:int}";
    public static string BuildRoute(int contactId) => Route.Replace("{ContactId:int}", contactId.ToString());

    public int ContactId { get; set; }
}