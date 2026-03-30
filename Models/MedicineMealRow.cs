namespace MediCue.Models
{
    public class MedicineMealRow
    {
        public string? Medicine { get; set; }

        public List<string>? Dosage { get; set; } //Morning / Afternoon / Evening
        public string? MealTiming { get; set; } // "After Meal" / "Before Meal"

        //public List<string>? Mode { get; set; } // "After Meal" / "Before Meal"
    }
}
