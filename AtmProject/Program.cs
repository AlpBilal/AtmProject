using AtmProject;
using System.IO;
using ZXing.Windows.Compatibility;
using System.Drawing;
using System.Drawing.Imaging;

List<User> users = [
    new User("Alp", 5, "abcd", 4524, 100.24M), 
    new ("bh", 4, "bscd", 4526, 100.25M),
    new ("dh", 3, "bscd", 4524, 100.25M)
    ];
List<Transaction> transactions = new List<Transaction>();
bool open = true;

    while (open)
    {
        Console.WriteLine("How would you like to login...\n1. name and PIN\n2.qrcode scan\n3.get a qrcode");
        var choice = Convert.ToInt32(Console.ReadLine());
        var name = string.Empty;
        var PIN = 0;
        User activeUser = null;
        
        switch (choice){
        case 1:
        Console.WriteLine("Your name: ");
        name= Console.ReadLine();
        Console.WriteLine("Your PIN: ");
        PIN = Convert.ToInt32(Console.ReadLine());
        activeUser = users.FirstOrDefault(u => u.Username == name && u.PIN == PIN);
        break;
        case 2:{
        var reader = new BarcodeReader();
        using var barcodeBitmap = (Bitmap)Image.FromFile(@"c:\Users\Alpbilal.basar\Downloads\username-qr.png");
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
        break;
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
                var image = writer.Write(name);
                var filePath = @"c:\Users\Alpbilal.basar\Downloads\username-qr.png";
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




