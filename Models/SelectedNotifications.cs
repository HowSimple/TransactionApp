namespace Commerce_TransactionApp.Models
{
    public class SelectedNotifications
    {
        public SelectedNotifications(bool outOfState, bool lowBalance, bool largeWithdraw)
        {
            this.outOfState = outOfState;
            this.lowBalance = lowBalance;
            this.largeWithdraw = largeWithdraw;
        }
        public SelectedNotifications() { }

        public bool outOfState { get; set; }
        public bool lowBalance { get; set; }
        public bool largeWithdraw { get; set; }
    }
}
