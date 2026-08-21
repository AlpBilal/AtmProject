using AtmProject;
using System.IO;
using ZXing.Windows.Compatibility;
using System.Drawing;
using System.Drawing.Imaging;

List<User> users = [
    new ("Alp", 5, "abcd", 4524, 100.24M), 
    new ("bh", 4, "bscd", 4526, 100.25M),
    new ("dh", 3, "bscd", 4524, 100.25M)
    ];
List<Transaction> transactions = new List<Transaction>();
List<VotingCategory> votes = [
    new ("Comedy"),
    new ("Sci-fi"),
    new ("Drama"),
];
bool open = true;


// if (int.TryParse(Console.ReadLine(), out int parsedPIN))
//         {
//             PIN = parsedPIN;
//         }
//         else
//         {
//             Console.WriteLine("Invalid PIN format. Please enter a numeric value.");
//             break;
//         }

    while (open)
    {
        Console.WriteLine("How would you like to login...\n1. name and PIN\n2.qrcode scan\n3.get a qrcode");
        var Choice =string.Empty;
        
        var name = string.Empty;
        var PIN = 0;
        User activeUser = null;
        if(!int.TryParse(Console.ReadLine(), out int choice))
        {
           Console.WriteLine("Invalid input. Please enter a number.");
           continue;
        }
        switch (choice){
        case 1:
        Console.WriteLine("Your name: ");
        name= Console.ReadLine();
        Console.WriteLine("Your PIN: ");
        
        if (int.TryParse(Console.ReadLine(), out int parsedPIN))
        {
            PIN = parsedPIN;
        }
        else
        {
            Console.WriteLine("Invalid PIN format. Please enter a numeric value.");
            break;
        }
        activeUser = users.FirstOrDefault(u => u.Username == name && u.PIN == PIN);
        break;
        case 2:{
        var reader = new BarcodeReader();
        
        string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        string filePath = Path.Combine(downloadsPath, "username-qr.png");
        
        try{
            using var barcodeBitmap = (Bitmap)Image.FromFile(filePath);
            var decode = reader.Decode(barcodeBitmap);
      
            if (decode != null)
                    {
                        
                        var RealUser = users.FirstOrDefault(u => u.Username == decode.Text);
                        if(RealUser != null){
                            activeUser = RealUser;
                            name = decode.Text;
                            Console.WriteLine("Congrats you logged in");
                        }
                        else
                        {
                            Console.WriteLine("Not a user");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No QR code could be read.");
                    }
            }
          
        catch (FileNotFoundException)
        {
            Console.WriteLine($"QR code file not found at {filePath}");
            break;
        }
        break;
        }
        case 3:
        Console.WriteLine("Your name: ");
        name= Console.ReadLine();
        var realUser = users.FirstOrDefault(u => u.Username == name);
        if (realUser != null)
            {
                var writer = new BarcodeWriter
                {
                    Format = ZXing.BarcodeFormat.QR_CODE
                };
                using var image = writer.Write(name);
                
                // CHANGE 2: Used the exact same dynamic path here to save the file
                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string filePath = Path.Combine(downloadsPath, "username-qr.png");
                
                image.Save(filePath, ImageFormat.Png);
                Console.WriteLine($"QR code saved to {filePath}");
            }
        else
            {
                Console.WriteLine("You are not a real user");
            }

        break;
        default:
        Console.WriteLine("Not a valid input... Try again");
        break;
    }
    

    if (activeUser != null)
    {

        bool atmaction = true;
        while (atmaction)
        {
            Console.WriteLine("Welcome " + name + " what would you like to do?:\n1.Withdraw money\n2. Deposit money\n3. pay\n4. exit\n5. End of day\n6. Voting");
            int option = 0;
            if (!int.TryParse(Console.ReadLine(), out option))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }
            else if (option == 1)
            {
                Console.WriteLine("How much would you like to withdraw from your account?: ");
                decimal amount = 0;
                if (!decimal.TryParse(Console.ReadLine(), out amount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                    continue;
                }

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
                decimal amount = 0;
                if (!decimal.TryParse(Console.ReadLine(), out amount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                    continue;
                }
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
                decimal amount = 0;
                if (!decimal.TryParse(Console.ReadLine(), out amount))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number.");
                    continue;
                }
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
                    decimal totalvotes = 0;
                    
                    foreach (VotingCategory vote in votes)
                    {
                        totalvotes += vote.voteCount;
                        writer.WriteLine($"{vote.category} : {vote.voteCount}");
                    }
                    foreach (VotingCategory vote in votes)
                    {
                        decimal percentage = totalvotes > 0 ? (vote.voteCount / totalvotes) * 100 : 0;
                        writer.WriteLine($"{vote.category}'s Percentage : {percentage}%");
                    }
                    writer.WriteLine("Total votes: " + totalvotes);
                }

            }
            else if (option == 6)
            {
                Console.WriteLine("What category do you like?");
                int i= 0;
                
                foreach(VotingCategory vote in votes)
                {
                    Console.WriteLine(i + ". " + vote.category);
                    i++;
                }
                try{
                var secim = Convert.ToInt32(Console.ReadLine());
                votes[secim].voteCount++;
                }
                catch(ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
                catch (FormatException){
                Console.WriteLine("You must enter a number!");
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