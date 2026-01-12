using KaratIQ.ViewModels;

namespace KaratIQ.Views;

public partial class BillingPage : ContentPage
{
    public BillingPage(BillingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}