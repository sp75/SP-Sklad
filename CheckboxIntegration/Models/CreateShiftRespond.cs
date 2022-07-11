using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
    public class ZReportPayment
    {
        public string id { get; set; }
        public int? code { get; set; }
        public string type { get; set; }
        public string label { get; set; }
        public int? sell_sum { get; set; }
        public int? return_sum { get; set; }
        public int? service_in { get; set; }
        public int? service_out { get; set; }
    }

    public class Tax
    {
        public string id { get; set; }
        public int? code { get; set; }
        public string label { get; set; }
        public string symbol { get; set; }
        public int? rate { get; set; }
        public int? sell_sum { get; set; }
        public int? return_sum { get; set; }
        public int? sales_turnover { get; set; }
        public int? returns_turnover { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? setup_date { get; set; }
        public int? extra_rate { get; set; }
        public bool included { get; set; }
        public DateTime? updated_at { get; set; }
        public int? sales { get; set; }
        public int? returns { get; set; }
    }

    public class ZReport
    {
        public string id { get; set; }
        public int? serial { get; set; }
        public bool is_z_report { get; set; }
        public List<ZReportPayment> payments { get; set; }
   //     public List<Tax> taxes { get; set; }
        public int? sell_receipts_count { get; set; }
        public int? return_receipts_count { get; set; }
        public int? transfers_count { get; set; }
        public int? transfers_sum { get; set; }
        public int? balance { get; set; }
        public int? initial { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }


    public class ClosingTransaction
    {
        public string id { get; set; }
        public int? serial { get; set; }
        public DateTime? request_signed_at { get; set; }
        public DateTime? request_received_at { get; set; }
        public string response_status { get; set; }
        public string response_error_message { get; set; }
        public string response_id { get; set; }
        public string offline_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string previous_hash { get; set; }
    }


    public class Permissions
    {
        public bool orders { get; set; }
    }

    public class CreateShiftRespond
    {
        public Guid id { get; set; }
        public int serial { get; set; }
        public ZReport z_report { get; set; }
        public DateTime? opened_at { get; set; }
        public DateTime? closed_at { get; set; }
   //     public InitialTransaction initial_transaction { get; set; }
  //      public ClosingTransaction closing_transaction { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
  //      public Balance balance { get; set; }
   //     public List<Tax> taxes { get; set; }
   //     public CashRegister cash_register { get; set; }
     //   public Cashier cashier { get; set; }
        public ErrorMessage error { get; set; }
    }


}
