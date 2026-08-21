# Console ATM Project

This is a C# console application that simulates an ATM, but with a few extra features like a QR code login system and a built-in voting mechanic. I built this to practice object-oriented programming, file handling, and robust exception handling in .NET.

## Features

* **Smart Login System:** Log in normally using a Name and PIN, or use the QR code scanner to log in instantly.
* **QR Code Generator:** The app can generate a personalized login QR code and save it dynamically to your computer's `Downloads` folder.
* **Standard Banking:** Securely Deposit, Withdraw, and Pay. The app is fully crash-resistant (using `TryParse`) so it handles bad user inputs gracefully.
* **Voting Feature:** Users can vote on categories (Comedy, Sci-fi, Drama) directly from the ATM menu. 
* **End of Day (EOD) Logging:** Exiting the ATM generates a daily `.txt` log file that records all successful and failed transactions, plus the final voting percentages.

## Requirements & Setup

To run this project, you will need .NET installed, along with a few NuGet packages for the QR code functionality.

1. Clone or download the repository.
2. Open your terminal in the project folder and run the following commands to install the required dependencies:
   ```bash
   dotnet add package ZXing.Net
   dotnet add package ZXing.Net.Bindings.Windows.Compatibility
   dotnet add package System.Drawing.Common

How It Works
First time? Select Option 3 at the login screen to generate your QR code. The app will save a username-qr.png file to your Downloads folder.

Next time? Select Option 2 at the login screen, and the app will automatically find and scan the QR code from your Downloads folder to log you in.

Author
Alp Bilal Başar