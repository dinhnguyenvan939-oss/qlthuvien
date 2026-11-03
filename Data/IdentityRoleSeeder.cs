using Microsoft.AspNetCore.Identity;

namespace qlthuvien.Data;

public static class IdentityRoleSeeder
{
    private static readonly string[] Roles = ["Admin", "Librarian", "Reader"];

    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Could not create role '{role}': {string.Join("; ", result.Errors.Select(error => error.Description))}");
                }
            }
        }
    }
}
