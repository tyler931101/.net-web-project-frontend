namespace frontend.Models
{
    public class TicketModel
    {
        public string Id { get; set; } = string.Empty;         // Ticket Id
        public string Title { get; set; } = string.Empty;      // Ticket title
        public string PerformerId { get; set; } = string.Empty;// User Id
        public string Content { get; set; } = string.Empty;    // Ticket details/content
        public DateTime ExpireDate { get; set; } = DateTime.UtcNow.AddDays(7); // still non-nullable
        public int Weight { get; set; } = 1;                   // Weight or priority
        public string Zone { get; set; } = "Todo";             // Status/Zone
        public string PerformerName { get; set; } = string.Empty;            
    }
}