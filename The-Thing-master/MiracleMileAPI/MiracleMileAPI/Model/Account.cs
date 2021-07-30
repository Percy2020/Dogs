using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiracleMileAPI.Model
{
    public class AccountList
    {
        public List<Account> Accounts { get; set; }
    }
    public class Account
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public IEnumerable<DepositProducts> DepositProducts { get; set; }
        public Boolean EligibleForDeposit { get; set; }
        public string Status { get; set; }
        public string RejectionReason { get; set; }
        public string ChangeType { get; set; }
    }
}
