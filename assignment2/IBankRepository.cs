namespace Models
{
    interface IBankRepository
    {
        void NewAccount(SBAccount acc);
        List<SBAccount>? GetAllAccounts();
        SBAccount? GetAccountDetails(int accno);
        void DepositAmount(int accno, decimal amt);
        void WithDrawAmount(int accno, decimal amt);
        List<SBTransaction>? GetTransactions(int accno);
    }
}