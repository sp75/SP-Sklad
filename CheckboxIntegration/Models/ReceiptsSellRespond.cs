using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Models
{
     public class Transaction
    {
        public string id { get; set; }
        public string type { get; set; }
        public int serial { get; set; }
        public string status { get; set; }
        public object request_signed_at { get; set; }
        public object request_received_at { get; set; }
        public object response_status { get; set; }
        public object response_error_message { get; set; }
        public object response_id { get; set; }
        public object offline_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string previous_hash { get; set; }
    }

    public class Good2
    {
        public string code { get; set; }
        public string barcode { get; set; }
        public string name { get; set; }
   //     public List<object> excise_barcodes { get; set; }
   //     public object header { get; set; }
 //       public object footer { get; set; }
   //     public object uktzed { get; set; }
        public int price { get; set; }
    }

    public class Good
    {
        public Good2 good { get; set; }
   //     public object good_id { get; set; }
   //     public int sum { get; set; }
        public int quantity { get; set; }
        public bool is_return { get; set; }
     //   public List<object> taxes { get; set; }
        public List<object> discounts { get; set; }
    }

    public class Payment
    {
    //    [JsonProperty("type")]
        public string type { get; set; }
    //    public bool? pawnshop_is_return { get; set; }
        public int value { get; set; }
        public string label { get; set; }
    }

    public enum PaymentType : int
    {
        CASH = 0,
        CARD = 1,
        CASHLESS = 2
    }

    public enum ReceiptExportFormat
    {
        text,
        html,
        pdf,
        png,
        qrcode
    }

    public class InitialTransaction
    {
        public string id { get; set; }
        public string type { get; set; }
        public int serial { get; set; }
        public string status { get; set; }
        public DateTime request_signed_at { get; set; }
        public DateTime request_received_at { get; set; }
        public string response_status { get; set; }
        public object response_error_message { get; set; }
        public string response_id { get; set; }
        public object offline_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string previous_hash { get; set; }
    }

    public class Balance
    {
        public int initial { get; set; }
        public int balance { get; set; }
        public int cash_sales { get; set; }
        public int card_sales { get; set; }
        public int cash_returns { get; set; }
        public int card_returns { get; set; }
        public int service_in { get; set; }
        public int service_out { get; set; }
        public DateTime? updated_at { get; set; }
    }


    public class CashRegister
    {
        public string id { get; set; }
        public string fiscal_number { get; set; }
        public bool active { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }

    public class Cashier
    {
        public string id { get; set; }
        public string full_name { get; set; }
        public string nin { get; set; }
        public string key_id { get; set; }
        public string signature_type { get; set; }
        public Permissions permissions { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public object certificate_end { get; set; }
        public object blocked { get; set; }
    }

    public class Shift
    {
        public Guid id { get; set; }
        public int serial { get; set; }
        public string status { get; set; }
        public object z_report { get; set; }
        public DateTime? opened_at { get; set; }
        public object closed_at { get; set; }
        public InitialTransaction initial_transaction { get; set; }
        public object closing_transaction { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public Balance balance { get; set; }
    //    public List<Tax> taxes { get; set; }
        public CashRegister cash_register { get; set; }
        public Cashier cashier { get; set; }
    }

    public class ReceiptsSellRespond
    {
        public Guid id { get; set; }
        public string type { get; set; }
        public Transaction transaction { get; set; }
        public int serial { get; set; }
        public string status { get; set; }
        public List<Good> goods { get; set; }
        public List<Payment> payments { get; set; }
        public int total_sum { get; set; }
        public int sum { get; set; }
        public int total_payment { get; set; }
        public int total_rest { get; set; }
        public int rest { get; set; }
        public object fiscal_code { get; set; }
        public object fiscal_date { get; set; }
        public object delivered_at { get; set; }
        public DateTime? created_at { get; set; }
        public object updated_at { get; set; }
        public List<object> taxes { get; set; }
        public List<object> discounts { get; set; }
        public object order_id { get; set; }
        public object header { get; set; }
        public object footer { get; set; }
        public object barcode { get; set; }
        public bool is_created_offline { get; set; }
        public bool is_sent_dps { get; set; }
        public object sent_dps_at { get; set; }
        public object tax_url { get; set; }
        public object related_receipt_id { get; set; }
        public bool technical_return { get; set; }
        public object currency_exchange { get; set; }
        public Shift shift { get; set; }
        public object control_number { get; set; }

        public ErrorMessage error { get; set; }
    }


}
