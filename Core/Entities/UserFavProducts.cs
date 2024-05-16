namespace Core.Entities;

public class UserFavProducts : BaseEntity
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
}