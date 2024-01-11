namespace Models
{
    class BankRepository : IBankRepository
    {
        public int CurrentTransactionId { get; set; } = 0;
        public List<SBAccount> SBAccounts = new();
        public List<SBTransaction> SBTransactions = new();

        public void NewAccount(SBAccount sBAccount)
        {
            SBAccounts.Add(sBAccount);
        }

        public SBAccount? GetAccountDetails(int accno)
        {
            return SBAccounts.Find(sbAccount => sbAccount.AccountNumber == accno);
        }

        public List<SBAccount> GetAllAccounts()
        {
            return SBAccounts;
        }

        public void DepositAmount(int accno, decimal amt)
        {
            SBAccount? account = SBAccounts.Find(sbAccount => sbAccount.AccountNumber == accno);
            if (account != null)
            {
                account.CurrentBalance += amt;
                CurrentTransactionId++;

                SBTransaction sBTransaction = new()
                {
                    TransactionId = CurrentTransactionId,
                    TrancationType = "Deposit",
                    TransactionDate = DateTime.Now,
                    AccountNumber = accno,
                    Amount = amt
                };

                SBTransactions.Add(sBTransaction);
                Console.WriteLine("Transaction successfull");
            }
            else
            {
                Console.WriteLine("Account not found");
            }
        }
        public void WithDrawAmount(int accno, decimal amt)
        {
            SBAccount? account = SBAccounts.Find(sbAccount => sbAccount.AccountNumber == accno);
            if (account != null)
            {
                account.CurrentBalance -= amt;
                CurrentTransactionId++;

                SBTransaction sBTransaction = new()
                {
                    TransactionId = CurrentTransactionId,
                    TrancationType = "Withdraw",
                    TransactionDate = DateTime.Now,
                    AccountNumber = accno,
                    Amount = amt
                };

                SBTransactions.Add(sBTransaction);
                Console.WriteLine("Transaction successfull");
            }
            else
            {
                Console.WriteLine("Account not found");
            }
        }
        public List<SBTransaction>? GetTransactions(int accno)
        {
            return SBTransactions.FindAll(sbTx => sbTx.AccountNumber == accno);
        }
    }
}