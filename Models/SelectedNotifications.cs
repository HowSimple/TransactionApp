namespace TransactionApp.Models
{
    public class SelectedNotifications
    {
        public SelectedNotifications(bool outOfState, bool lowBalance, bool largeWithdraw, double lowBalanceLimit, double largeWithdrawLimit)
        {
            this.outOfState = outOfState;
            this.lowBalance = lowBalance;
            this.largeWithdraw = largeWithdraw;
            this.lowBalanceLimit = lowBalanceLimit;
            this.largeWithdrawLimit = largeWithdrawLimit;
        }
        public SelectedNotifications() { }

        public bool outOfState { get; set; }
   
        public bool lowBalance { get; set; }
        public double lowBalanceLimit { get; set; }
        public bool largeWithdraw { get; set; }
        public double largeWithdrawLimit { get; set; }

    }
}
