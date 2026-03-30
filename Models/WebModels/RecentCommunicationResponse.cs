namespace MediCue.Models.WebModels
{
    public class RecentCommunicationResponse
    {
        public List<RecentCommunication>? OutputCommunicationAsJson { get; set; }
    }

    public class RecentCommunication
    {
        public string? Title { get; set; }
        public string? Text01 { get; set; }
    }
}
