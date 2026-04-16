using CloudGame.Domain.Commom;

namespace CloudGame.Domain.Parameters;

public class FindUsersParameter : PaginationParameters
{
    public string? Name { get; set; }

    public bool? Active { get; set; }

    public bool? IsAdmin { get; set; }
}
