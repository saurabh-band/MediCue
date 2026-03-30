namespace MediCue.ViewModels
{

    public partial class ReportSummaryTextViewModel : BaseViewMoedl
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        ReportItem reportItem;

        [ObservableProperty]
        string reportSummaryText = String.Empty;

        [ObservableProperty]
        string reportSummaryTitle = String.Empty;

        [ObservableProperty]
        DateTime reportDate;

        public ReportSummaryTextViewModel(INavigationService navigationService) : base(navigationService)
        {

            _navigationService = navigationService;

            // Show warning popup

            LoadSummary();
        }

        public void LoadSummary()
        {
            if(ReportItem != null && !string.IsNullOrEmpty(ReportItem.Summary))
            {
                ReportSummaryTitle = ReportItem.Heading;
                ReportDate = ReportItem.ReportDate;
                ReportSummaryText = ReportItem.Summary;
                return;
            }

            ReportSummaryTitle = "Multi Vitamin";
            ReportDate = new DateTime(2026, 1, 20);
            ReportSummaryText = "✅ Easy Summary of Your Multivitamin Report\r\nThis multivitamin report shows the levels of different vitamins in the body. Some values are normal, while others are too low or slightly high. Here is a clear interpretation:\r\n\r\n\U0001f7e2 Vitamins That Are Normal\r\nThese vitamins are within the healthy range:\r\n\r\nVitamin B1 (Thiamine)\r\nVitamin B2 (Riboflavin)\r\nVitamin B3 (Niacin)\r\nVitamin K1\r\n\r\nThese do not require any action.\r\n [Multivitam..._MultiPage | PDF]\r\n\r\n\U0001f7e1 Borderline Issues (Slightly Low or Slightly High)\r\nThese vitamins are close to the border of normal and may require monitoring:\r\nBorderline Low\r\n\r\nVitamin B12\r\nVitamin C\r\nVitamin D\r\n\r\nMeaning:\r\nYour levels are just below the ideal range. They are not dangerously low but could cause symptoms such as tiredness, low immunity, or weakness if they drop further.\r\nBorderline High\r\n\r\nVitamin A\r\nVitamin E\r\n\r\nMeaning:\r\nThese levels are near the upper limit. Not dangerous now, but they could become high if supplements are taken excessively.\r\n [Multivitam..._MultiPage | PDF]\r\n\r\n🔴 Clearly Low Vitamins (Need Attention)\r\nVitamin B9 (Folate) – Low\r\nMeaning:\r\nYour folate level is below normal. Low folate can cause fatigue, weakness, or anemia. Increasing green vegetables, lentils, or folate supplements may help.\r\n [Multivitam..._MultiPage | PDF]\r\n\r\n🔴 Clearly High Vitamins (Need Attention)\r\nVitamin B6 (PLP) – High\r\nMeaning:\r\nThis is above the recommended range. High B6 often happens due to taking supplements. Very high B6 over time can cause nerve‑related symptoms like tingling or numbness.\r\n [Multivitam..._MultiPage | PDF]\r\n\r\n\U0001f9fe Overall Interpretation (Simple)\r\n\r\nYou have multiple borderline low vitamins, especially Vitamin D, B12, and C.\r\nYou have one clearly low vitamin (Folate), which may need dietary improvement or supplements.\r\nYou have two vitamins near the upper limit (A and E) and one high vitamin (B6), likely due to supplements.\r\nMost other vitamins are in the normal range. [Multivitam..._MultiPage | PDF]\r\n\r\n\r\n⭐ What This Means in Everyday Language\r\nYour body seems to be:\r\n\r\nLacking some important vitamins (especially D, B12, Folate, C).\r\nPossibly getting too much of certain vitamins (B6, A, and E), often from supplements.\r\nThis is not dangerous, but it shows your diet or supplements may need balancing.\r\n\r\n\r\n📌 Simple Recommendations\r\n(Not medical advice—just a general explanation based on the report.)\r\n💊 If you take multivitamins:\r\nYou may need to reduce doses of:\r\n\r\nVitamin B6\r\nVitamin A\r\nVitamin E\r\n\r\n🍎 Improve intake of:\r\n\r\nFruits & vegetables → Vitamin C, Folate\r\nSun exposure / Vitamin D supplement → Vitamin D\r\nDairy, eggs, or B12 supplement → Vitamin B12\r\n\r\n\U0001fa7a If symptoms exist (fatigue, tingling, weakness):\r\nYou may want to review with a doctor.";
        }

        public async override void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            try
            {
                base.ApplyQueryAttributes(query);
                if (query != null && query.ContainsKey("ReportItem"))
                {
                    ReportItem = query["ReportItem"] as ReportItem;
                    if(ReportItem != null)
                    {
                        // Here you can load the summary text based on the ReportItem
                        LoadSummary();
                    }
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                // Log or handle exception as needed
                System.Diagnostics.Debug.WriteLine($"Error in ApplyQuerryAttributes: {ex.Message}");
            }
        }
    }
}
