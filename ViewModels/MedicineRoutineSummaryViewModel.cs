namespace MediCue.ViewModels
{
    public partial class MedicineRoutineSummaryViewModel : BaseViewMoedl
    {
        private readonly INavigationService _navigationService;
        [ObservableProperty]
        string medicineRoutineSummaryText = String.Empty;
        [ObservableProperty]
        string medicineRoutineSummaryTitle = String.Empty;
        [ObservableProperty]
        string summaryDate;

        public MedicineRoutineSummaryViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadSummary();
        }

        public void LoadSummary()
        {
            MedicineRoutineSummaryTitle = "January 2026";
            SummaryDate = "01/01/2026 to 31/01/2026";
            MedicineRoutineSummaryText = "✅ 1. Medications in the Schedule\r\nYour dataset tracks five medicines, repeated daily:\r\n\r\n * Glycomet 500 : After meal -> Morning, Afternoon, Night \r\n * Tab Vitagreat : After meal -> Morning, Night \r\n * Tab Lobate 100 : After meal -> Morning, Afternoon, Night \r\n * Tab Rabekind 20 : Before meal -> Morning, Night \r\n * Tab Benidine 4 mg : Before meal -> Night only\r\n\r\n✅ 2. Expected vs. Actual Medication Times\r\nStandard expected schedule:\r\n\r\nMorning: 09:00\r\nAfternoon: 14:00\r\nNight: 21:00\r\n\r\nActual observations:\r\n\r\nMorning doses mostly taken on time (within 0–30 min delay)\r\nAfternoon doses: occasional 15–30 min delays\r\nNight doses: many missed doses or delays up to 1 hour\r\n\r\n\r\n✅ 3. Missed Dose Summary\r\nI calculated missed doses programmatically.\r\nAcross all days:\r\nMedication-wise missed doses\r\n\r\nGlycomet 500: Several misses, especially afternoon & night\r\nVitagreat: Missed nighttime doses more often\r\nLobate 100: Similar pattern—night doses often missed\r\nRabekind 20: Occasionally missed morning doses\r\nBenidine 4mg: Frequently missed (night-only medicine)\r\n\r\nPattern:\r\n\r\nNight doses are missed the most\r\nFew (but some) misses occur in the morning\r\nMidday/afternoon is moderately consistent but still lagging behind expected times\r\n\r\n\r\n✅ 4. Timeliness Analysis (Early / Delayed Intake)\r\n⏱ Morning Dose Timing\r\n\r\nMostly taken between 09:00–09:30, acceptable range\r\nOccasional delays up to 10:00 am\r\n\r\n⏱ Afternoon Dose Timing\r\n\r\nDelays range between 14:15–14:50\r\nSome days entirely missed\r\n\r\n⏱ Night Dose Timing\r\n\r\nTaken between 21:00–22:00\r\nSignificant number of Missed entries\r\n\r\n\r\n✅ 5. Behavior Interpretation & Predicted Pros & Cons\r\nPros (Positive Health Indicators):\r\n✔ Morning routine is strong\r\n✔ Majority of doses are not very late (within 15–30 minutes)\r\n✔ Consistency shows patient awareness and intention to adhere\r\n✔ Multiday stable entries indicate overall manageable adherence pattern\r\n\r\nCons (Negative Health Indicators / Risks):\r\n❌ Night dose inconsistency — most important medications (Glycomet, Rabekind, Lobate, Benidine) depend heavily on timing\r\n❌ High frequency of missed doses on some days (full-day non-adherence occurred)\r\n❌ Meal-dependent medicines (before/after meal) become less effective when delayed\r\n❌ Diabetes-related medicine (Glycomet) inconsistency can directly worsen glucose control\r\n\r\n✅ 6. Predicted Health Implications\r\nIf this pattern continues, the patient may experience:\r\nShort-Term Risks\r\n\r\nBlood sugar fluctuations due to Glycomet irregularity\r\nAcid reflux or gastritis worsening (Rabekind taken late or missed)\r\nVitamin deficiency impact (Vitagreat missing)\r\nReduced therapeutic impact due to timing mismatch with meals\r\n\r\nLong-Term Risks\r\n\r\nHigher chance of uncontrolled diabetes\r\nPoor nutrient absorption due to irregular supplementation\r\nMetabolic stress due to irregular medication timing\r\nPossible worsening of the underlying condition that required Lobate or Benidine\r\n\r\n\r\n✅ 7. Expected Health Behavior (Based on Data)\r\nGiven the patterns:\r\nStrengths in behavior\r\n\r\nPatient likely has a routine for morning meds\r\nAfternoon is manageable but needs optimization\r\nData shows awareness of expected times\r\n\r\nBehavior Challenges\r\n\r\nNighttime discipline is weak — possibly due to fatigue, forgetfulness, irregular sleep routine\r\nFull-day misses on some dates may indicate:\r\n\r\nBusy schedule\r\nStress\r\nLack of reminder system\r\nMedication burnout\r\n\r\n\r\n\r\nBehavior Prediction\r\nWithout intervention, adherence may continue to decline slowly, especially for nighttime doses.\r\n\r\n✅ 8. Recommendations\r\nTo improve adherence:\r\n📌 Use automated reminders / alarms on mobile\r\n📌 Keep medicines near dinner table or near water bottle\r\n📌 Use a weekly pill organizer\r\n📌 Keep a visual chart or checkmark tracker\r\n📌 Consider family reminders for night medication\r\nIf pattern does not improve:\r\n📌 Consult doctor to simplify medication timings\r\n📌 Consider extended-release alternatives (for diabetes / acidity)";
        }
    }
}
