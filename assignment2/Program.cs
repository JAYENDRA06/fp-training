using Models;

IBankRepository bankRepository = new BankRepository();
bool loop = true;

while (loop)
{
    Console.WriteLine("Choose you options");
    Console.WriteLine("1. Add a new account");
    Console.WriteLine("2. Get all accounts");
    Console.WriteLine("3. Get Account details");
    Console.WriteLine("4. Deposit amount");
    Console.WriteLine("5. Withdraw amount");
    Console.WriteLine("6. Get all transactions");
    Console.WriteLine("7. Exit");

    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            {
                Console.WriteLine("Enter account number, name, address and initial balance");
                int _accountNumber = Convert.ToInt32(Console.ReadLine());
                string? _customerName = Console.ReadLine();
                string? _customerAddress = Console.ReadLine();
                string? temp = Console.ReadLine();
                if (!decimal.TryParse(temp, out decimal _currentBalance))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                    break;
                }

                SBAccount newAccount = new()
                {
                    AccountNumber = _accountNumber,
                    CustomerName = _customerName,
                    CustomerAddress = _customerAddress,
                    CurrentBalance = _currentBalance
                };
                bankRepository.NewAccount(newAccount);

                break;
            }
        case 2:
            {
                List<SBAccount>? accounts = bankRepository.GetAllAccounts();
                if (accounts != null)
                {
                    Console.WriteLine("All account details");
                    foreach (SBAccount account in accounts)
                    {
                        Console.WriteLine(account);
                    }
                }
                else
                {
                    Console.WriteLine("No account found");
                }

                break;
            }
        case 3:
            {
                Console.WriteLine("Enter the account number");
                int _accountNumber = Convert.ToInt32(Console.ReadLine());
                SBAccount? account = bankRepository.GetAccountDetails(_accountNumber);

                if (account != null)
                {
                    Console.WriteLine("Account details");
                    Console.WriteLine(account);
                }
                else
                {
                    Console.WriteLine("Account not found");
                }

                break;
            }
        case 4:
            {
                Console.WriteLine("Enter the account number and amount to deposit");
                int _accountNumber = Convert.ToInt32(Console.ReadLine());
                string? temp = Console.ReadLine();
                if (!decimal.TryParse(temp, out decimal _amount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                    break;
                }

                bankRepository.DepositAmount(_accountNumber, _amount);

                break;
            }
        case 5:
            {
                Console.WriteLine("Enter the account number and amount to withdraw");
                int _accountNumber = Convert.ToInt32(Console.ReadLine());
                string? temp = Console.ReadLine();
                if (!decimal.TryParse(temp, out decimal _amount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                    break;
                }

                bankRepository.WithDrawAmount(_accountNumber, _amount);

                break;
            }
        case 6:
            {
                Console.WriteLine("Enter the account number");
                int _accountNumber = Convert.ToInt32(Console.ReadLine());
                List<SBTransaction>? transactions = bankRepository.GetTransactions(_accountNumber);
                if (transactions != null)
                {
                    Console.WriteLine("All transactions related that customer are");
                    foreach (SBTransaction transaction in transactions)
                    {
                        Console.WriteLine(transaction);
                    }
                }
                else
                {
                    Console.WriteLine("No Transactions found");
                }

                break;
            }
        default:
            loop = false;
            break;
    }
}