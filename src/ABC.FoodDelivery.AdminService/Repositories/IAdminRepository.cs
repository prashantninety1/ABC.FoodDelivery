public interface IAdminRepository
{
    Task<AdminUser> GetAdminByEmailAsync(string email);
    Task AddAdminAsync(AdminUser adminUser);
    Task LogActivityAsync(SystemActivity activity);
}