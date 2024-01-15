using System.Security.Cryptography;

namespace banking_project_ssms.Models
{
    class BankRepository : IBankRepository
    {
        public List<SbaccountJay> SBAccounts = [];
        public List<SbtransactionJay> SBTransactions = [];
        public readonly Ace52024Context db = new();

        public void NewAccount(SbaccountJay sBAccount)
        {
            db.SbaccountJays.Add(sBAccount);
            db.SaveChanges();
        }

        public SbaccountJay? GetAccountDetails(int accno)
        {
            SbaccountJay? sbaccountJay = db.SbaccountJays.Find(accno);
            return sbaccountJay;
        }

        public List<SbaccountJay> GetAllAccounts()
        {
            return [.. db.SbaccountJays.Where(x => true)];
        }

        public void DepositAmount(int accno, decimal amt)
        {
            SbaccountJay? account = db.SbaccountJays.Find(accno);
            if (account != null)
            {
                account.CurrentBalance += amt;
                db.SbaccountJays.Update(account);

                SbtransactionJay sBTransaction = new()
                {
                    TrancationType = "Deposit",
                    TransactionDate = DateTime.Now,
                    AccountNumber = accno,
                    Amount = amt
                };

                db.SbtransactionJays.Add(sBTransaction);
                db.SaveChanges();
                Console.WriteLine("Transaction successfull");
            }
            else
            {
                Console.WriteLine("Account not found");
            }
        }
        public void WithDrawAmount(int accno, decimal amt)
        {
            SbaccountJay? account = db.SbaccountJays.Find(accno);
            if (account != null)
            {
                account.CurrentBalance -= amt;
                db.SbaccountJays.Update(account);

                SbtransactionJay sBTransaction = new()
                {
                    TrancationType = "Withdraw",
                    TransactionDate = DateTime.Now,
                    AccountNumber = accno,
                    Amount = amt
                };

                db.SbtransactionJays.Add(sBTransaction);
                db.SaveChanges();
                Console.WriteLine("Transaction successfull");
            }
            else
            {
                Console.WriteLine("Account not found");
            }
        }
        public List<SbtransactionJay>? GetTransactions(int accno)
        {
            return [.. db.SbtransactionJays.Where(x => x.AccountNumber == accno)];
        }
    }
}