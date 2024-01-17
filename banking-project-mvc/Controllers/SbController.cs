using first_mvc_application.Models;
using Microsoft.AspNetCore.Mvc;

namespace first_mvc_application.Controllers
{
    public class SbController(Ace52024Context _db) : Controller
    {
        readonly Ace52024Context db = _db;

        public IActionResult ShowSbAccounts()
        {
            List<SbaccountJay> sbaccounts = [.. db.SbaccountJays];
            return View(sbaccounts);
        }

        public IActionResult ShowSbTransactions()
        {
            List<SbtransactionJay> sbtransactions = [.. db.SbtransactionJays];
            return View(sbtransactions);
        }

        [HttpGet]
        public IActionResult AddSbAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddSbTransaction()
        {
            return View();
        }

        // Adding
        [HttpPost]
        public IActionResult AddSbAccount(SbaccountJay account)
        {
            db.SbaccountJays.Add(account);
            db.SaveChanges();
            return RedirectToAction("ShowSbAccounts");
        }

        [HttpPost]
        public IActionResult AddSbTransaction(SbtransactionJay tx)
        {
            SbaccountJay? account = db.SbaccountJays.Find(tx.AccountNumber);
            if (account != null)
            {
                if (tx.TrancationType == "Withdraw") account.CurrentBalance -= tx.Amount;
                else if (tx.TrancationType == "Deposit") account.CurrentBalance += tx.Amount;
                else return RedirectToAction("ShowSbTransactions");

                db.SbaccountJays.Update(account);
                db.SbtransactionJays.Add(tx);
                db.SaveChanges();
            }

            return RedirectToAction("ShowSbTransactions");
        }

        // Updating
        [HttpGet]
        public IActionResult UpdateSbAccount(int id)
        {
            SbaccountJay? account = db.SbaccountJays.Find(id);
            return View(account);
        }
        
        [HttpPost]
        public IActionResult UpdateSbAccount(SbaccountJay account)
        {
            db.SbaccountJays.Update(account);
            db.SaveChanges();
            return RedirectToAction("ShowSbAccounts");
        }
        
        [HttpGet]
        public IActionResult UpdateSbTransaction(int id)
        {
            SbtransactionJay? tx = db.SbtransactionJays.Find(id);
            return View(tx);
        }
        
        [HttpPost]
        public IActionResult UpdateSbTransaction(SbtransactionJay tx)
        {
            db.SbtransactionJays.Update(tx);
            db.SaveChanges();
            return RedirectToAction("ShowSbTransactions");
        }

        // Reading individual
        [HttpGet]
        public IActionResult GetSbAccount(int id)
        {
            SbaccountJay? account = db.SbaccountJays.Find(id);
            return View(account);
        }

        [HttpGet]
        public IActionResult GetSbTransaction(int id)
        {
            SbtransactionJay? tx = db.SbtransactionJays.Find(id);
            return View(tx);
        }

        // Deleting

        [HttpGet]
        public IActionResult DeleteAcount(int id)
        {
            SbaccountJay? account = db.SbaccountJays.Find(id);
            return View(account);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteAcountConfirmed(int id)
        {
            SbaccountJay? account = db.SbaccountJays.Find(id);
            if(account != null) db.SbaccountJays.Remove(account);
            return RedirectToAction("ShowSbAccounts");
        }

        [HttpGet]
        public IActionResult DeleteTransaction(int id)
        {
            SbtransactionJay? tx = db.SbtransactionJays.Find(id);
            return View(tx);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteTransactionConfirmed(int id)
        {
            SbtransactionJay? tx = db.SbtransactionJays.Find(id);
            if(tx != null) db.SbtransactionJays.Remove(tx);
            return RedirectToAction("ShowSbTransactions");
        }
    }
}