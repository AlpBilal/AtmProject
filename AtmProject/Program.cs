using AtmProject;
using System.IO;
using ZXing;

List<User> users = [
    new User("Alp", 5, "abcd", 4524, 100.24M), 
    new ("bh", 4, "bscd", 4526, 100.25M),
    new ("bh", 4, "bscd", 4526, 100.25M)
    ];
List<Transaction> transactions = new List<Transaction>();
bool open = true;
    while (open)
    {

    Console.WriteLine("Your name: ");
    var name= Console.ReadLine();
    Console.WriteLine("Your PIN: ");
    var PIN = Convert.ToInt32(Console.ReadLine());
    User activeUser = users.FirstOrDefault(u => u.Username == name && u.PIN == PIN);

    if (activeUser != null)
    {

        bool atmaction = true;
        while (atmaction)
        {
            Console.WriteLine("Welcome " + name + " what would you like to do?:\n1.Withdraw money\n2. Deposit money\n3. pay\n4. exit\n5. End of day");
            int option = Convert.ToInt32(Console.ReadLine());
            if (option == 1)
            {
                Console.WriteLine("How much would you like to withdraw from your account?: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                if (amount > activeUser.Balance)
                {
                    Console.WriteLine("The money you wish to withdraw is more than your account has...\nTry again");
                    transactions.Add(new Transaction(amount, false, "Withdrawl"));
                }
                else
                {
                    activeUser.Withdraw(amount);
                    Console.WriteLine("New balance is " + activeUser.Balance);
                    transactions.Add(new Transaction(amount, true, "Withdrawl"));
                }
            }
            else if (option == 2)
            {
                Console.WriteLine("How much would you like to deposit to your account?: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                if (amount <= 0)
                {
                    Console.WriteLine("The money you wish to deposit is not enough \nTry again");
                    transactions.Add(new Transaction(amount, false, "Deposit"));
                }
                else
                {
                    activeUser.Deposit(amount);
                    Console.WriteLine("New balance is " + activeUser.Balance);
                    transactions.Add(new Transaction(amount, true, "Deposit"));
                }

            }
            else if (option == 3)
            {
                Console.WriteLine("How much would you like to pay from your account?: ");
                decimal amount = Convert.ToDecimal(Console.ReadLine());
                if (amount > activeUser.Balance)
                {
                    Console.WriteLine("The money you wish to pay is more than your account has...\nTry again");
                    transactions.Add(new Transaction(amount, false, "Pay"));
                }
                else
                {
                    activeUser.Withdraw(amount);
                    Console.WriteLine("New balance is " + activeUser.Balance);
                    transactions.Add(new Transaction(amount, true, "Pay"));
                }

            }
            else if (option == 4)
            {
                Console.WriteLine("Exiting...");
                atmaction = false;
            }
            else if (option == 5)
            {
                atmaction = false;
                open = false;
                string dateString = DateTime.Now.ToString("ddMMyyyy");
                string fileName = $"EOD_{dateString}.txt";

                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (Transaction t in transactions)
                    {
                        writer.WriteLine($"Action: {t.actionName}, Amount: {t.amount}, Success: {t.work}, Date: {t.date}");
                    }
                }

            }
            else
            {
                Console.WriteLine(option + " option is not valid try again!");
            }
        } }
    else
    {
        transactions.Add(new Transaction(0, false, "Fraud"));
    }
}




