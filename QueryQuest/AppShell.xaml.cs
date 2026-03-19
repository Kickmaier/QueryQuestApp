using QueryQuest.Views;
namespace QueryQuest
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(QuizPage), typeof(QuizPage));
        }
    }
}
