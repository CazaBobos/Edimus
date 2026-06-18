using Mediator;

namespace Users.Core.Features.GetUsers;
public class GetUsersRequest : IRequest<GetUsersResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
}
