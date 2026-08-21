using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmProject
{
    internal class User
    {
        //The Goal:and a structure for logs (type of action, timestamp).
        public String Username { get; set; }
        public int ID { get; set; }
        public String Password { get; set; }
        public int PIN { get; set; }
        public decimal Balance { get; set; }

        public User(string username, int iD, string password, int pIN, decimal balance)
        {
            this.Username = username;
            ID = iD;
            this.Password = password;
            PIN = pIN;
            this.Balance = balance;
        }


        public void pay(decimal amount)
        {

            if(amount <= 0){
            Console.WriteLine("The money you wish to pay is not enough \nTry again");
            }
            else
            {
                this.Balance = this.Balance - amount;
            }


        }
        public void Withdraw(decimal amount)
        {

            if(amount <= 0){
            Console.WriteLine("The money you wish to withdraw is not enough \nTry again");
            }
            else
            {
                this.Balance = this.Balance - amount;
            }
        }

        
        public void Deposit(decimal amount)
        {

            if(amount <= 0){
            Console.WriteLine("The money you wish to Deposit is not enough \nTry again");
            }
            else
            {
                this.Balance = this.Balance + amount;
            }
        }


    }
}
