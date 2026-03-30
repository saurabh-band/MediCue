

namespace MediCue.Popups;

public partial class MedicineMealTablePopup : Popup
{
    public string Title { get; }
    public ObservableCollection<MedicineMealRow> Items { get; }

    public MedicineMealTablePopup(string title, IEnumerable<MedicineMealRow> items)
    {
        InitializeComponent();
        Title = title;
        Items = new ObservableCollection<MedicineMealRow>(items);
        BindingContext = this;
    }

    private void OnOkClicked(object sender, EventArgs e) => CloseAsync();
}