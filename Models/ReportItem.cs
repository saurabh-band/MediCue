namespace MediCue.Models
{
    public partial class ReportItem : ObservableObject
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime ReportDate { get; set; }
        public string Heading { get; set; } = string.Empty;
        // Name of a PDF file we'll put in Resources/Raw
        public string FileName { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        // For checkbox binding
        [ObservableProperty]
        public bool isSelected;

        [ObservableProperty]
        public bool isCheckBoxEnable = true;
    }
}
