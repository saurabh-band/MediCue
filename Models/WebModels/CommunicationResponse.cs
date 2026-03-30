namespace MediCue.Models.WebModels
{
    public class CommunicationResponse
    {
       public List<OutputCommunicationSaveAsJson>? OutputCommunicationSaveAsJsons { get; set; }
    }

    public class OutputCommunicationSaveAsJson
    {
        public string? Message { get; set; }
    }
}
