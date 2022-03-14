namespace Commerce_TransactionApp.Models
{
    public class SelectedNotifications
    {
        public bool outOfState { get; set; }
        public bool lowBalance { get; set; }
        public bool largeWithdraw { get; set; }
    }
}
