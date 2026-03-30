namespace MediCue.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void OnViewSummaryTapped(object sender, EventArgs e)
        {
            if(sender is Border borders)
            {
                borders.Shadow = new Shadow { Brush = Colors.Transparent };
                borders.BackgroundColor = CommonResources.GetResourceColor("buttonPressedBackgroundColor");

                await Task.Delay(200);

                borders.Shadow = new Shadow { Brush = CommonResources.GetResourceColor("shadowColor"), Opacity = (float)0.15 };
                borders.Shadow.Offset = new Point(0, 6);
                borders.BackgroundColor = CommonResources.GetResourceColor("greyBackgroundColor");
                borders.Shadow.Radius = 2;
            }
        }
    }
}
