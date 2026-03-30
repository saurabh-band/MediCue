namespace MediCue.ViewModels
{
    public partial class ReportsAndSummaryViewModel : BaseViewMoedl
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        public ObservableCollection<ReportItem>? reports;

        [ObservableProperty]
        ReportItem? selectedFile;

        [ObservableProperty]
        ReportItem checkBoxSelectedReport;

        partial void OnSelectedFileChanged(ReportItem? value)
        {
            if(value != null)
            {
                OnReportItemSelected(value);
            }
        }

        public ReportsAndSummaryViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            
        }

        [RelayCommand]
        private async Task Appearing()
        {
            await LoadSummary();
        }

        [RelayCommand]
        public async Task UploadReport()
        {
            try
            {
                await _navigationService.NavigateToAsync(nameof(UploadReportPage));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UploadReport navigation failed: {ex}");
            }
        }

        [RelayCommand]
        public async Task GenerateSummary()
        {
            try
            {
                if (CheckBoxSelectedReport == null)
                {

                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        if (Application.Current?.MainPage is not null)
                        {
                            await Application.Current.MainPage.DisplayAlert(
                                "WARNING",
                                "No report is selected, Please select a Report to generate a summary",
                                "OK");
                        }
                    });

                    return;
                }
                var navigationParameters = new Dictionary<string, object>
                {
                    { "ReportItem", CheckBoxSelectedReport }
                };
                await _navigationService.NavigateToAsync(nameof(ReportSummaryTextPage), navigationParameters);
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation to ReportSummaryTextPage failed: {ex}");
                // optional: show an alert/toast
            }
        }

        public async Task LoadSummary()
        {
            // Unsubscribe from old items if LoadSummary() can be called multiple times
            if (Reports != null)
            {
                foreach (var old in Reports)
                    old.PropertyChanged -= ReportItem_PropertyChanged;
            }

            Reports = new ObservableCollection<ReportItem>();
            Reports?.Add(new ReportItem { ReportDate = new DateTime(2024, 07, 01), Heading = "Fasting PP", FileName = "20240701_fasting_pp.pdf", Summary= "✅ Fasting & Post‑Prandial (PP) Blood Sugar Report — Dated 01/07/2024\r\n📌 Findings:\r\n\r\nFasting Glucose: 92 mg/dL (Normal: 70–99 mg/dL) [20240701_fasting_pp | PDF]\r\nPP (after meal) Glucose: 118 mg/dL (Normal: <140 mg/dL) [20240701_fasting_pp | PDF]\r\n\r\n\U0001f7e2 Summary (Simple Words):\r\nYour fasting and after‑meal sugar levels are completely normal. There is no sign of diabetes or prediabetes based on this report." });
            Reports?.Add(new ReportItem { ReportDate = new DateTime(2024, 10, 02), Heading = "TSH", FileName = "20241002_tsh.pdf", Summary= "✅ Thyroid Function Test (TSH & Free T4) — Dated 02/10/2024\r\n📌 Findings:\r\n\r\nTSH: 2.1 µIU/mL (Normal: 0.4–4.0 µIU/mL) [20241002_tsh | PDF]\r\nFree T4: 1.2 ng/dL (Normal: 0.8–1.8 ng/dL) [20241002_tsh | PDF]\r\n\r\n\U0001f7e2 Summary (Simple Words):\r\nYour thyroid hormones are perfectly normal. There is no sign of hypothyroidism or hyperthyroidism." });
            Reports?.Add(new ReportItem { ReportDate = new DateTime(2025, 01, 25), Heading = "Lipid Profile", FileName = "20250125_lipid_profile.pdf", Summary= "✅ Lipid Profile (Cholesterol Test) — Dated 25/01/2025\r\n📌 Findings:\r\n\r\nTotal Cholesterol: 172 mg/dL (Normal: <200 mg/dL) [20250125_l...id_profile | PDF]\r\nHDL (Good Cholesterol): 58 mg/dL (Good: ≥50 mg/dL for females) [20250125_l...id_profile | PDF]\r\nLDL (Bad Cholesterol): 96 mg/dL (Normal: <100 mg/dL) [20250125_l...id_profile | PDF]\r\nTriglycerides: 110 mg/dL (Normal: <150 mg/dL) [20250125_l...id_profile | PDF]\r\n\r\n\U0001f7e2 Summary (Simple Words):\r\nYour cholesterol levels are healthy and within the ideal range.\r\nGood cholesterol (HDL) is strong, and bad cholesterol (LDL) is low. This indicates good heart health." });
            Reports?.Add(new ReportItem { ReportDate = new DateTime(2025, 05, 04), Heading = "TSH", FileName = "20250504_tsh.pdf", Summary= "✅ Thyroid Function Test (TSH & Free T4) — Dated 04/05/2025\r\n📌 Report Values:\r\n\r\nTSH: 2.1 µIU/mL (Normal: 0.4–4.0 µIU/mL) [20250504_tsh | PDF]\r\nFree T4: 1.2 ng/dL (Normal: 0.8–1.8 ng/dL) [20250504_tsh | PDF]\r\n\r\n\U0001f7e2 Simple Summary:\r\nYour thyroid hormone levels are perfectly normal.\r\nThere is no sign of thyroid under‑activity or over‑activity." });
            Reports?.Add(new ReportItem { ReportDate = new DateTime(2025, 07, 05), Heading = "Fasting PP", FileName = "20250705_fasting_pp.pdf", Summary= "✅ Fasting & Post‑Prandial (PP) Sugar Report — Dated 05/07/2025\r\n📌 Report Values:\r\n\r\nFasting Sugar: 92 mg/dL (Normal: 70–99 mg/dL) [20250705_fasting_pp | PDF]\r\nPP (after meal) Sugar: 118 mg/dL (Normal: <140 mg/dL) [20250705_fasting_pp | PDF]\r\n\r\n\U0001f7e2 Simple Summary:\r\nBoth fasting and after‑meal sugar levels are within the normal range.\r\nYour body is handling sugar properly — no signs of diabetes." });
            Reports?.Add(new ReportItem { ReportDate = new DateTime(2025, 08, 15), Heading = "HbA1c", FileName = "20250815_hba1c.pdf", Summary= "✅ HbA1c Report — Dated 15/08/2025\r\n📌 Report Values:\r\n\r\nHbA1c: 5.4% (Normal: <5.7%) [20250815_hba1c | PDF]\r\nEstimated Average Glucose: 108 mg/dL [20250815_hba1c | PDF]\r\n\r\n\U0001f7e2 Simple Summary:\r\nYour HbA1c value is normal, which means your 3‑month average blood sugar level is healthy.\r\nNo indication of diabetes or prediabetes." });
            Reports?.Add(new ReportItem { ReportDate = new DateTime(2025, 11, 20), Heading = "Lipid Profile", FileName = "20251120_lipid_profile.pdf", Summary= "✅ Lipid (Cholesterol) Profile — 20 Nov 2025\r\n📌 Values:\r\n\r\nTotal Cholesterol: 172 mg/dL (Normal: <200 mg/dL) [20251120_l...id_profile | PDF]\r\nHDL (Good Cholesterol): 58 mg/dL (Good ≥50 mg/dL) [20251120_l...id_profile | PDF]\r\nLDL (Bad Cholesterol): 96 mg/dL (Normal <100 mg/dL) [20251120_l...id_profile | PDF]\r\nTriglycerides: 110 mg/dL (Normal <150 mg/dL) [20251120_l...id_profile | PDF]\r\n\r\n\U0001f7e2 Simple Summary:\r\nYour cholesterol levels are healthy and well‑balanced.\r\nGood cholesterol is strong, and bad cholesterol is low — good for heart health." });
            Reports?.Add(new ReportItem { ReportDate = new DateTime(2026, 01, 15), Heading = "Fasting PP", FileName = "20260115_fasting_pp.pdf", Summary= "✅ Fasting & Post‑Meal (PP) Sugar Report — 15 Jan 2026\r\n📌 Values:\r\n\r\nFasting Sugar: 92 mg/dL (Normal: 70–99 mg/dL) [20260115_fasting_pp | PDF]\r\nPost‑Meal (PP) Sugar: 118 mg/dL (Normal: <140 mg/dL) [20260115_fasting_pp | PDF]\r\n\r\n\U0001f7e2 Simple Summary:\r\nYour blood sugar levels are normal both before and after eating.\r\nThere is no indication of diabetes or prediabetes." });
            Reports?.Add(new ReportItem { ReportDate = new DateTime(2026, 01, 20), Heading = "Multivitamin Lab Report", FileName = "Multivitamin_Lab_Report_Dummy_Female_MultiPage.pdf", Summary= "✅ Multivitamin Lab Report — 20 Jan 2026\r\nSummary of Key Vitamin Levels\r\n\U0001f7e2 Vitamins in Normal Range:\r\n\r\nVitamin B1, B2, B3, K1 — all normal and healthy. [Multivitam..._MultiPage | PDF]\r\n\r\n\U0001f7e1 Borderline / Slightly Abnormal Values:\r\n\r\nVitamin A: High‑normal (just slightly above the ideal range).\r\nVitamin C: Borderline low.\r\nVitamin D: Slightly low (insufficient).\r\nVitamin E: Upper normal / borderline high.\r\nVitamin B12: Borderline low.\r\n [Multivitam..._MultiPage | PDF]\r\n\r\n🔴 Clearly Low:\r\n\r\nFolate (Vitamin B9): Low — may indicate low dietary intake or absorption issues.\r\n [Multivitam..._MultiPage | PDF]\r\n\r\n🔺 High Value:\r\n\r\nVitamin B6: Higher than normal — often due to supplements; high levels may cause nerve‑related symptoms.\r\n [Multivitam..._MultiPage | PDF]\r\n\r\n\U0001f7e2 Simple Summary (Easy Words):\r\n\r\nMost of your vitamins are normal.\r\nA few vitamins (B9, B12, C, D) are low or slightly low, which may mean your diet needs more fruits, vegetables, or sunlight.\r\nVitamin B6 is too high, usually from supplements — you may need to reduce dosage.\r\nVitamins A and E are on the higher side but not dangerously high.\r\nNothing is extremely abnormal, but some improvements in diet or supplement adjustments will help." });

            // Subscribe to each item's IsSelected changes
            foreach (var item in Reports!)
                item.PropertyChanged += ReportItem_PropertyChanged;

            await Task.CompletedTask;

        }

        private void ReportItem_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is not ReportItem item)
                return;

            if (e.PropertyName != nameof(ReportItem.IsSelected))
                return;

            var selected = Reports?.Where(r => r.IsSelected).ToList();

            if (selected?.Count == 0)
            {
                // none selected => all enabled
                foreach (var r in Reports!)
                    r.IsCheckBoxEnable = true;

                CheckBoxSelectedReport = null;

                return;
            }

            var keep = selected?[0];

            foreach (var r in Reports!)
            {
                if (!ReferenceEquals(r, keep))
                    r.IsSelected = false;

                r.IsCheckBoxEnable = ReferenceEquals(r, keep);
            }

            OnItemCheckBoxSelected(item);
            
        }

        private async void OnReportItemSelected(ReportItem reportItem)
        {
            if (string.IsNullOrWhiteSpace(reportItem.FileName))
                return;

            try
            {
                // 1) Read the PDF from Resources/Raw (MauiAsset)
                await using Stream input = await FileSystem.OpenAppPackageFileAsync(reportItem.FileName);

                // 2) Copy to a physical file path (cache is fine for viewing)
                var targetPath = Path.Combine(FileSystem.CacheDirectory, reportItem.FileName);

                using (var output = File.Create(targetPath))
                    await input.CopyToAsync(output);

                // 3) Ask the OS to open it with the default PDF viewer
                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(targetPath)
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to open PDF '{reportItem.FileName}': {ex}");
                // optional: show an alert/toast
            }
        }

        private void OnItemCheckBoxSelected(ReportItem reportItem)
        {
            CheckBoxSelectedReport = reportItem;
        }

        private void ClearSelection()
        {
            SelectedFile = null;
            CheckBoxSelectedReport = null;

            if (Reports is null)
                return;

            foreach (var r in Reports)
            {
                r.IsSelected = false;
                r.IsCheckBoxEnable = true;
            }
        }

        [RelayCommand]
        private void SelectReport(ReportItem report)
        {
            CheckBoxSelectedReport = report;
            SelectedFile = report;
        }
    }
}
