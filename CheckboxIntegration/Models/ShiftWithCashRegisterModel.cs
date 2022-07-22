using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class ShiftWithCashRegisterModel
    {
        public Guid id { get; set; }
        public Int32 serial { get; set; }
        public ShiftStatus status { get; set; }
        public object z_report { get; set; }
        public DateTime? opened_at { get; set; }
        public DateTime? closed_at { get; set; }
        public object initial_transaction { get; set; }
        public object closing_transaction { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public BalanceModel balance { get; set; }
        public List<ShiftTaxModel> taxes { get; set; }
        public CashRegisterModel cash_register { get; set; }
        public ErrorMessage error { get; set; }

        public bool IsError()
        {
            return error != null;
        }

    }
}
