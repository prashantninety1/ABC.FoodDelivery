namespace ABC.FoodDelivery.CustomerService.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; } // Foreign Key
        public required string Message { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; } // Tracks whether notification is viewed

        public required Customer Customer { get; set; }
    }
}