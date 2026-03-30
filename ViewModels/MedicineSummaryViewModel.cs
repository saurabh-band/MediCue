using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using PointF = Syncfusion.Drawing.PointF;


namespace MediCue.ViewModels
{
    public partial class MedicineSummaryViewModel : BaseViewMoedl
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        public ObservableCollection<MedicationEntry>? medicationEntries;

        [ObservableProperty]
        public string? reportSummary;

        public MedicineSummaryViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadSummary();
        }
        public void LoadSummary()
        {

            MedicationEntries = new ObservableCollection<MedicationEntry>
            {
                new() { Date = new DateTime(2026,1,1),  MorningTime = "09:00 AM", AfternoonTime = "02:00 PM", NightTime = "09:20 PM" },
                new() { Date = new DateTime(2026,1,2),  MorningTime = "09:00 AM", AfternoonTime = "02:15 PM", NightTime = "Missed" },
                new() { Date = new DateTime(2026,1,3),  MorningTime = "Missed",   AfternoonTime = "Missed",   NightTime = "09:00 PM" },
                new() { Date = new DateTime(2026,1,4),  MorningTime = "09:00 AM", AfternoonTime = "02:30 PM", NightTime = "09:00 PM" },
                new() { Date = new DateTime(2026,1,5),  MorningTime = "09:00 AM", AfternoonTime = "02:00 PM", NightTime = "09:15 PM" },
                new() { Date = new DateTime(2026,1,6),  MorningTime = "09:00 AM", AfternoonTime = "02:50 PM", NightTime = "Missed" },
                new() { Date = new DateTime(2026,1,7),  MorningTime = "09:30 AM", AfternoonTime = "02:20 PM", NightTime = "09:00 PM" },
                new() { Date = new DateTime(2026,1,8),  MorningTime = "09:15 AM", AfternoonTime = "02:00 PM", NightTime = "10:00 PM" },
                new() { Date = new DateTime(2026,1,9),  MorningTime = "09:00 AM", AfternoonTime = "03:00 PM", NightTime = "09:30 PM" },
                new() { Date = new DateTime(2026,1,10), MorningTime = "09:00 AM", AfternoonTime = "02:00 PM", NightTime = "09:45 PM" },
                new() { Date = new DateTime(2026,1,11), MorningTime = "Missed",   AfternoonTime = "Missed",   NightTime = "Missed" },
                new() { Date = new DateTime(2026,1,12), MorningTime = "09:00 AM", AfternoonTime = "02:50 PM", NightTime = "09:50 PM" },
                new() { Date = new DateTime(2026,1,13), MorningTime = "09:00 AM", AfternoonTime = "02:30 PM", NightTime = "09:43 PM" },
                new() { Date = new DateTime(2026,1,14), MorningTime = "09:00 AM", AfternoonTime = "02:00 PM", NightTime = "09:00 PM" },
                new() { Date = new DateTime(2026,1,15), MorningTime = "10:00 AM", AfternoonTime = "02:00 PM", NightTime = "08:30 PM" },
                new() { Date = new DateTime(2026,1,16), MorningTime = "09:30 AM", AfternoonTime = "02:00 PM", NightTime = "Missed" },
                new() { Date = new DateTime(2026,1,17), MorningTime = "09:00 AM", AfternoonTime = "02:20 PM", NightTime = "09:15 PM" },
                new() { Date = new DateTime(2026,1,18), MorningTime = "09:00 AM", AfternoonTime = "02:50 PM", NightTime = "09:20 PM" },
                new() { Date = new DateTime(2026,1,19), MorningTime = "Missed",   AfternoonTime = "Missed",   NightTime = "09:55 PM" },
                new() { Date = new DateTime(2026,1,20), MorningTime = "Missed",   AfternoonTime = "Missed",   NightTime = "09:00 PM" },
                new() { Date = new DateTime(2026,1,21), MorningTime = "Missed",   AfternoonTime = "Missed",   NightTime = "08:45 PM" },
                new() { Date = new DateTime(2026,1,22), MorningTime = "09:00 AM", AfternoonTime = "02:00 PM", NightTime = "09:00 PM" },
                new() { Date = new DateTime(2026,1,23), MorningTime = "09:00 AM", AfternoonTime = "02:30 PM", NightTime = "Missed" },
                new() { Date = new DateTime(2026,1,24), MorningTime = "Missed",   AfternoonTime = "Missed",   NightTime = "08:50 PM" },
                new() { Date = new DateTime(2026,1,25), MorningTime = "09:00 AM", AfternoonTime = "02:20 PM", NightTime = "09:00 PM" },
                new() { Date = new DateTime(2026,1,26), MorningTime = "09:15 AM", AfternoonTime = "03:00 PM", NightTime = "09:25 PM" },
                new() { Date = new DateTime(2026,1,27), MorningTime = "09:50 AM", AfternoonTime = "02:00 PM", NightTime = "Missed" },
                new() { Date = new DateTime(2026,1,28), MorningTime = "09:25 AM", AfternoonTime = "02:00 PM", NightTime = "09:35 PM" },
                new() { Date = new DateTime(2026,1,29), MorningTime = "09:30 AM", AfternoonTime = "02:20 PM", NightTime = "09:00 PM" },
                new() { Date = new DateTime(2026,1,30), MorningTime = "09:00 AM", AfternoonTime = "02:30 PM", NightTime = "Missed" },
                new() { Date = new DateTime(2026,1,31), MorningTime = "09:00 AM", AfternoonTime = "02:45 PM", NightTime = "09:00 PM" },
            };

        }


        [RelayCommand]
        public async Task GenerateSummary()
        {
            await _navigationService.NavigateToAsync(nameof(MedicineRoutineSummary));
        }

        [RelayCommand]
        public async Task ExportToPdf()
        {
            if (MedicationEntries is null || MedicationEntries.Count == 0)
                return;

            // Create PDF documen
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;

            // Fonts
            PdfStandardFont headerFont = new(PdfFontFamily.Helvetica, 18, PdfFontStyle.Bold);
            PdfStandardFont tableFont = new(PdfFontFamily.Helvetica, 12);
            PdfStandardFont tableHeaderFont = new(PdfFontFamily.Helvetica, 14, PdfFontStyle.Bold);

            // Draw title
            graphics.DrawString("Medication Summary", headerFont, PdfBrushes.Black, new PointF(150, 20));


            float y = 70;


            // ---------- Draw Header Row ----------
            graphics.DrawString("Date", tableHeaderFont, PdfBrushes.Black, new PointF(20, y));
            graphics.DrawString("Morning", tableHeaderFont, PdfBrushes.Black, new PointF(120, y));
            graphics.DrawString("Afternoon", tableHeaderFont, PdfBrushes.Black, new PointF(220, y));
            graphics.DrawString("Night", tableHeaderFont, PdfBrushes.Black, new PointF(320, y));

            y += 25;


            // ----------- Table Rows -------------
            foreach (var item in MedicationEntries)
            {
                graphics.DrawString(item.Date.ToString("dd/MM/yyyy"), tableFont, PdfBrushes.Black, new PointF(20, y));
                graphics.DrawString(item.MorningTime, tableFont, PdfBrushes.Black, new PointF(120, y));
                graphics.DrawString(item.AfternoonTime, tableFont, PdfBrushes.Black, new PointF(220, y));
                graphics.DrawString(item.NightTime, tableFont, PdfBrushes.Black, new PointF(320, y));

                y += 22;
            }

            y += 30;



            // ------------ Summary Section ------------
            graphics.DrawString("Summary:", headerFont, PdfBrushes.Black, new PointF(20, y));
            y += 30;


            graphics.DrawString(ReportSummary ?? "No summary entered.",
                                        tableFont, PdfBrushes.Black,
                                        new RectangleF(20, y, 500, 200));


            // Save to memory stream
            using MemoryStream ms = new();
            document.Save(ms);
            ms.Position = 0;

            document.Close(true);

            // Save to device storage
            string filename = $"MedicationSummary_{DateTime.Now:yyyyMMdd}.pdf";

            await SaveAndOpenPdf(ms, filename);
        }


        // Loads embedded images
        private static async Task<PdfBitmap?> LoadImageAsync(string fileName)
        {
            try
            {
                //var assembly = typeof(App).GetTypeInfo().Assembly;
                //var resourceName = $"{assembly.GetName().Name}.Resources.Images.{fileName}";
                //Stream? stream = assembly.GetManifestResourceStream(resourceName);

                await using Stream stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                return new PdfBitmap(stream);

                if (stream is null)
                    return null;

                return new PdfBitmap(stream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PDF image load failed for '{fileName}': {ex.Message}");
                return null;

            }
        }

        // Saves PDF to device and opens it
        private async Task SaveAndOpenPdf(Stream pdfStream, string filename)
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, filename);

            using (var file = File.Create(filePath))
                pdfStream.CopyTo(file);

            await Launcher.OpenAsync(new OpenFileRequest
            {
                File = new ReadOnlyFile(filePath)
            });
        }



    }
}
