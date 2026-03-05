namespace PresentationViewModel
{
    public class MainView
    {
        public string DisplayText { get; set; }

        public void LoadData()
        {
            var repo = new Data.Repo();
            var service = new BusinessLogic.GreetService();
            DisplayText = service.FormatGreeting(repo.GetNameFromDb());
        }
    }
}
