namespace MediCue.Views;

public partial class AddNewMedicineSchedulePage : ContentPage
{
    private const string SelectedImage = "correctcheckmark.png";
    private const string UnselectedImage = "incorrect.png";
    public bool IsAfterMeal { get; set; } = false;
    public bool IsBeforeMeal { get; set; } = false;
    private Border? _beforeMealBorder;
    private Border? _afterMealBorder;

    public AddNewMedicineSchedulePage()
	{
		InitializeComponent();
	}

    private void ApplyToggleVisual(Border border, bool isSelected)
    {
        border.Shadow = new Shadow { Brush = CommonResources.GetResourceColor("shadowColor"), Opacity = 0.15f };
        border.Shadow.Offset = new Point(0, 6);
        border.Shadow.Radius = 2;

        border.BackgroundColor = isSelected
            ? Colors.Green
            : CommonResources.GetResourceColor("buttonPressedBackgroundColor");

        if (border.Content is VerticalStackLayout layout)
        {
            var image = layout.OfType<Image>().FirstOrDefault();
            if (image is not null)
            {
                image.Source = isSelected ? SelectedImage : UnselectedImage;
            }
        }
    }

    private async void OnViewSummaryTapped(object sender, EventArgs e)
    {
        if (sender is not Border border)
            return;

        var isSelected = border.BackgroundColor == CommonResources.GetResourceColor("buttonPressedBackgroundColor");

        // Tap feedback
        border.Shadow = new Shadow { Brush = Colors.Transparent };
        border.BackgroundColor = Colors.White;

        await Task.Delay(200);

        border.Shadow = new Shadow { Brush = CommonResources.GetResourceColor("shadowColor"), Opacity = 0.15f };
        border.Shadow.Offset = new Point(0, 6);
        border.Shadow.Radius = 2;

        border.BackgroundColor = isSelected
            ? Colors.Green
            : CommonResources.GetResourceColor("buttonPressedBackgroundColor");

        if (border.Content is VerticalStackLayout layout)
        {
            var image = layout.OfType<Image>().FirstOrDefault();
            if (image is not null)
            {
                image.Source = isSelected ? SelectedImage : UnselectedImage;
            }
        }
    }

    private async void OnBeforeMealBtnTapped(object sender, EventArgs e)
    {
        if (sender is not Border border)
            return;

        _beforeMealBorder ??= border;

        var newBeforeSelected = !IsBeforeMeal;

        IsBeforeMeal = newBeforeSelected;
        IsAfterMeal = newBeforeSelected ? false : IsAfterMeal;

        if (newBeforeSelected && _afterMealBorder is not null)
            ApplyToggleVisual(_afterMealBorder, isSelected: false);

        // Tap feedback
        border.Shadow = new Shadow { Brush = Colors.Transparent };
        border.BackgroundColor = Colors.White;

        await Task.Delay(200);

        ApplyToggleVisual(border, IsBeforeMeal);
    }

    private async void OnAfterMealBtnTapped(object sender, EventArgs e)
    {
        if (sender is not Border border)
            return;

        _afterMealBorder ??= border;

        var newAfterSelected = !IsAfterMeal;

        IsAfterMeal = newAfterSelected;
        IsBeforeMeal = newAfterSelected ? false : IsBeforeMeal;

        if (newAfterSelected && _beforeMealBorder is not null)
            ApplyToggleVisual(_beforeMealBorder, isSelected: false);

        // Tap feedback
        border.Shadow = new Shadow { Brush = Colors.Transparent };
        border.BackgroundColor = Colors.White;

        await Task.Delay(200);

        ApplyToggleVisual(border, IsAfterMeal);
    }
}