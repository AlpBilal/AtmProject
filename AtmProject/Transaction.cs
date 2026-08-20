using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmProject
{
    internal class Transaction
    {
        public string date { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public decimal amount { get; set; }
        public Boolean work { get; set; }
        public string actionName { get; set; }

        public Transaction(decimal amount, Boolean work, string actionName)
        {
            this.amount = amount;
            this.work = work;
            this.actionName = actionName;
        }
    }
}
