namespace KonsultationApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();  
        SetupUI();
    }

    private void SetupUI()
    {
        
        Content = new VerticalStackLayout
        {
            Children =
            {
                new Label
                {
                    Text = "Добро пожаловать в KonsultationApp!",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 20
                }
            }
        };
    }
}