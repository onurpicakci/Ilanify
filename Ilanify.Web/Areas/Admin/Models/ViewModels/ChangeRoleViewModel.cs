using Microsoft.AspNetCore.Identity;

namespace Ilanify.Areas.Admin.Models.ViewModels;

public class ChangeRoleViewModel
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    public IList<string> UserRoles { get; set; }
    public IList<IdentityRole> AllRoles { get; set; }
}