public class DeliveryRepository
{
    private readonly DeliveryDbContext _context;

    public DeliveryRepository(DeliveryDbContext context)
    {
        _context = context;
    }

    public async Task AssignDeliveryAsync(DeliveryAssignment assignment)
    {
        await _context.DeliveryAssignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task<DeliveryAssignment> GetDeliveryAssignmentByOrderIdAsync(Guid orderId)
    {
        return await _context.DeliveryAssignments.FirstOrDefaultAsync(d => d.OrderId == orderId);
    }
}