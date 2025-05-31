public class AdminRepository : IAdminRepository
{
    private readonly AdminDbContext _context;

    public AdminRepository(AdminDbContext context)
    {
        _context = context;
    }

    public async Task<AdminUser> GetAdminByEmailAsync(string email)
    {
        return await _context.AdminUsers.FirstOrDefaultAsync(a => a.Email == email);
    }

    public async Task AddAdminAsync(AdminUser adminUser)
    {
        _context.AdminUsers.Add(adminUser);
        await _context.SaveChangesAsync();
    }

    public async Task LogActivityAsync(SystemActivity activity)
    {
        _context.SystemActivities.Add(activity);
        await _context.SaveChangesAsync();
    }
}