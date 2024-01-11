namespace Models
{
    class SBTransaction
    {
        public int? TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string? TrancationType { get; set; }
        public override string ToString()
        {
            return $"TransactionId {TransactionId}, TransactionDate = {TransactionDate}, AccountNumber = {AccountNumber}, Amount = {Amount}, TrancationType = {TrancationType}";
        }
    }
}