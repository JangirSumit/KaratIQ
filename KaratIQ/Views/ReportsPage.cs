namespace KaratIQ.Views;

public class ReportsPage : ContentPage
{
    public ReportsPage()
    {
        Title = "Reports";
        Content = new StackLayout
        {
            Padding = 20,
            VerticalOptions = LayoutOptions.Center,
            Children = {
                new Label { Text = "📊 Reports & Analytics", FontSize = 24, HorizontalOptions = LayoutOptions.Center },
                new Label { Text = "Coming Soon...", FontSize = 16, HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(0, 20, 0, 0) }
            }
        };
    }
}