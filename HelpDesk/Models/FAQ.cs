namespace HelpDesk.Models
{
    public class FAQ {
        public int FAQId { get; set; }
        public string Header { get; set; } = null!;
        public string Context { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}