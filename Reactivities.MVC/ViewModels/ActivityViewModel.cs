namespace Reactivities.MVC.ViewModels
{
    public class ActivityViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        public string Category { get; set; }
    }
}
