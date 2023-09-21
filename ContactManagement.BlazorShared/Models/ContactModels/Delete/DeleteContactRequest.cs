namespace ContactManagement.BlazorShared.Models.ContactModels.Delete;

public class DeleteContactRequest
{
    public const string Route = "/Contacts/{ContactId:int}";
    public static string BuildRoute(int contactId) => Route.Replace("{ContactId:int}", contactId.ToString());

    public int ContactId { get; set; }
}