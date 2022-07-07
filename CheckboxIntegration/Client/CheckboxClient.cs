using CheckboxIntegration.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CheckboxIntegration.Client
{
    public class CheckboxClient
    {

        private const string BASE_ADDRESS = " https://api.checkbox.ua/api/v1/";
        private readonly HttpClient _client;

        public CheckboxClient()
            : this(null)
        {
        }
        public CheckboxClient(string access_token)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            _client = new HttpClient
            {
                BaseAddress = new Uri(BASE_ADDRESS)
            };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(access_token))
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
            }
        }


        #region Касир

        /// <summary>
        /// Вхід користувача (касира) за допомогою логіна та паролю
        /// </summary>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/v1/cashier/signin</param>
        /// <returns>Basic </returns>
        public CashierSigninRespond CashierSignin(CashierSigninRequest properties)
        {
            var response = _client.PostAsJsonAsync("cashier/signin", properties).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<CashierSigninRespond>().Result;
                return result;
            }
            else 
            {
                var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                return new CashierSigninRespond
                {
                    error = apiErrorResponse
                };
            }
            
        }

        /// <summary>
        /// Завершення сесії користувача (касира) з поточним токеном доступу
        /// </summary>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/redoc#operation/sign_out_cashier_api_v1_cashier_signout_post</param>
        /// <returns>Basic </returns>
        public ErrorMessage SignOutCashier()
        {
            var response = _client.PostAsJsonAsync("cashier/signout", new { }).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                return null;
            }
            else
            {
                var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                return apiErrorResponse;
            }

        }
        #endregion

        #region Зміни

        /// <summary>
        /// Відкриття нової зміни касиром.
        /// </summary>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/redoc#operation/create_shift_api_v1_shifts_post</param>
        /// <returns>CreateShiftRespond</returns>
        public CreateShiftRespond CreateShift(string license_key)
        {
            _client.DefaultRequestHeaders.TryAddWithoutValidation("X-License-Key", license_key);

            var response = _client.PostAsJsonAsync("shifts", new { }).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<CreateShiftRespond>().Result;
                return result;
            }
            else
            {
                var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                return new CreateShiftRespond
                {
                    error = apiErrorResponse
                };
            }
        }

        /// <summary>
        /// Створення Z-Звіту та закриття поточної зміни користувачем (касиром).
        /// </summary>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/redoc#operation/close_shift_api_v1_shifts_close_post</param>
        /// <returns>CreateShiftRespond</returns>
        public CreateShiftRespond CloseShift()
        {
            var response = _client.PostAsJsonAsync("shifts/close", new { }).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<CreateShiftRespond>().Result;
                return result;
            }
            else
            {
                var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                return new CreateShiftRespond
                {
                    error = apiErrorResponse
                };
            }
        }

        /// <summary>
        /// Отримання звіту в текстовому вигляді
        /// </summary>
        /// <param name="report_id">receipt id</param>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/docs#/%D0%97%D0%B2%D1%96%D1%82%D0%B8/get_report_text_api_v1_reports__report_id__text_get</param>
        /// <returns>Text Report</returns>
        public byte[] GetReportText(Guid report_id, ReceiptExportFormat format)
        {
            var response = _client.GetAsync(string.Format("reports/{0}/{1}", report_id, format)).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsByteArrayAsync().Result;
                return result;
            }
            else
            {
                return null;
            }
        }

        

        #endregion


        #region Чеки

        /// <summary>
        /// Створення чеку продажу/повернення, його фіскалізація та доставка клієнту по email.
        /// </summary>
        /// <param name="acc_id">Accession id</param>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/redoc#operation/create_receipt_api_v1_receipts_sell_post</param>
        /// <returns></returns>
        public ReceiptsSellRespond ReceiptsSell(ReceiptsSellRequest properties)
        {
            string jsonString = JsonConvert.SerializeObject(properties);

            var response = _client.PostAsJsonAsync("receipts/sell", properties).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<ReceiptsSellRespond>().Result;
                return result;
            }
            else
            {
                var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                return new ReceiptsSellRespond
                {
                    error = apiErrorResponse
                };
            }
        }

        /// <summary>
        /// Отримання інформації про чек.
        /// </summary>
        /// <param name="receipt_id">receipt id</param>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/docs#/%D0%A7%D0%B5%D0%BA%D0%B8/get_receipt_api_v1_receipts__receipt_id__get</param>
        /// <returns></returns>
        public ReceiptsSellRespond GetReceipt(Guid receipt_id)
        {
            var response = _client.GetAsync(string.Format("receipts/{0}", receipt_id) ).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<ReceiptsSellRespond>().Result;
                return result;
            }
            else
            {
                var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                return new ReceiptsSellRespond
                {
                    error = apiErrorResponse
                };
            }
        }


        /// <summary>
        /// Отримання PDF представлення чеку згідно наказу №329 від 08.06.2021.
        /// </summary>
        /// <param name="receipt_id">receipt id</param>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/docs#/%D0%A7%D0%B5%D0%BA%D0%B8/get_receipt_pdf_api_v1_receipts__receipt_id__pdf_get</param>
        /// <returns></returns>
        public byte[] GetReceiptPdf(Guid receipt_id, ReceiptExportFormat format)
        {
            var response = _client.GetAsync(string.Format("receipts/{0}/{1}", receipt_id, format)).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsByteArrayAsync().Result;
                return result;
            }
            else
            {
                return null;
            }
        }

        #endregion

       

    }
}
