namespace HelpDesk.ViewModels
{
    public class FAQViewModel {
        public int FAQId { get; set; }
        public string Header { get; set; } = null!;
        public string Context { get; set; } = null!;
        public int ProductId { get; set; }
    }
}