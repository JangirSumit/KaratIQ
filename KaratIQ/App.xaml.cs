using KaratIQ.Data;

namespace KaratIQ;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
    
    public App(KaratIQContext context) : this()
    {
        // Initialize database
        context.Database.EnsureCreated();
    }
}