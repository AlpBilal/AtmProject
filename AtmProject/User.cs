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

            this.Balance = this.Balance - amount;


        }
        public void Withdraw(decimal amount)
        {


            this.Balance = this.Balance - amount;
        }
        public void Deposit(decimal amount)
        {

            this.Balance = this.Balance + amount;
        }


    }
}
