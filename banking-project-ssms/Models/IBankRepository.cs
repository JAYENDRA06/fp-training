namespace banking_project_ssms.Models
{
    interface IBankRepository
    {
        void NewAccount(SbaccountJay acc);
        List<SbaccountJay>? GetAllAccounts();
        SbaccountJay? GetAccountDetails(int accno);
        void DepositAmount(int accno, decimal amt);
        void WithDrawAmount(int accno, decimal amt);
        List<SbtransactionJay>? GetTransactions(int accno);
    }
}