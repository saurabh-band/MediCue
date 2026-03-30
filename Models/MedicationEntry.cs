namespace MediCue.Models
{
    public class MedicationEntry
    {

        public DateTime Date { get; set; }
        public string? MorningTime { get; set; }
        public string? AfternoonTime { get; set; }
        public string? NightTime { get; set; }

    }
}
