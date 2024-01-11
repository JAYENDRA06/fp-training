namespace Models
{
    class SBAccount
    {
        public int AccountNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public decimal CurrentBalance { get; set; }
        public override string ToString()
        {
            return $"AccountNumber {AccountNumber}, Name = {CustomerName}, Address = {CustomerAddress}, Balance = {CurrentBalance}";
        }
    }
}