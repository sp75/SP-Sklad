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

        private const string BASE_ADDRESS = "https://api.checkbox.ua/api/v1/";
        private readonly HttpClient _client;
        private string _access_token { get; set; }

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

            _access_token = access_token;
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
                _access_token = result.access_token;

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

        /// <summary>
        /// Cashier Signature Status
        /// </summary>
        /// <param name="properties">https://api.checkbox.ua/api/docs#/%D0%9A%D0%B0%D1%81%D0%B8%D1%80/check_signature_api_v1_cashier_check_signature_get</param>
        /// <returns>Cashier Signature Status</returns>
        public CashierSignatureStatus CheckSignature()
        {
            var response = _client.GetAsync("cashier/check-signature").Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<CashierSignatureStatus>().Result;
                return result;
            }
            else
            {
                var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                return new CashierSignatureStatus
                {
                    error = apiErrorResponse
                };
            }
        }

        /// <summary>
        /// Отримання інформації про активну зміну користувача (касира)
        /// </summary>
        /// <param name="properties">https://api.checkbox.ua/api/docs#/%D0%9A%D0%B0%D1%81%D0%B8%D1%80/get_cashier_shift_api_v1_cashier_shift_get</param>
        /// <returns>Cashier Signature Status</returns>
        public ShiftWithCashRegisterModel GetCashierShift()
        {
            var response = _client.GetAsync("cashier/shift").Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<ShiftWithCashRegisterModel>().Result;
                return result;
            }
            else
            {
                var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                return new ShiftWithCashRegisterModel
                {
                    error = apiErrorResponse
                };
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
        /// Отримання інформації про зміну
        /// </summary>
        /// <param name="shift_id ">shift id</param>
        /// <param name="properties">https://api.checkbox.ua/api/docs#/%D0%97%D0%BC%D1%96%D0%BD%D0%B8/get_shift_api_v1_shifts__shift_id__get</param>
        /// <returns>Get Shift</returns>
        public CreateShiftRespond GetShift(Guid shift_id)
        {
            var response = _client.GetAsync(string.Format("shifts/{0}", shift_id)).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<CreateShiftRespond>().Result;
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
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/redoc#operation/create_receipt_api_v1_receipts_sell_post</param>
        /// <returns></returns>
        public ReceiptsSellRespond CreateReceipt(ReceiptSellPayload properties)
        {
            string jsonString = JsonConvert.SerializeObject(properties);

            var response = _client.PostAsJsonAsync("receipts/sell", properties).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<ReceiptsSellRespond>().Result;

                result.WaitingReceiptFiscalCode(_access_token);
                if (!result.fiscal_date.HasValue)
                {
                    result.error = new ErrorMessage { message = $"Не вдалось отримати фіскальний номер !" };
                }

                return result;
            }
            else
            {
                try
                {
                    var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                    return new ReceiptsSellRespond
                    {
                        id = properties.id,
                        error = apiErrorResponse
                    };
                }
                catch (Exception err)
                {
                    return new ReceiptsSellRespond
                    {
                        id = properties.id,
                        error = new ErrorMessage {  message = $"{err.Message}" }
                    };
                }
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
                try
                {
                    var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                    return new ReceiptsSellRespond
                    {
                        error = apiErrorResponse
                    };
                }
                catch (Exception err)
                {
                    return new ReceiptsSellRespond
                    {
                     //   id = properties.id,
                        error = new ErrorMessage { message = $"{err.Message} [{response.Content.ReadAsStringAsync().Result}]" }
                    };
                }
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

        /// <summary>
        /// Отримання графічного представлення чека для термопринтеру згідно наказу №329 від 08.06.2021.
        /// </summary>
        /// <param name="receipt_id">receipt id</param>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/docs#/%D0%A7%D0%B5%D0%BA%D0%B8/get_receipt_pdf_api_v1_receipts__receipt_id__pdf_get</param>
        /// <returns></returns>
        public byte[] GetReceiptPng(Guid receipt_id, int paper_width = 58, int qrcode_scale = 50)
        {
            var response = _client.GetAsync(string.Format("receipts/{0}/png?paper_width={1}&qrcode_scale={2}", receipt_id, paper_width, qrcode_scale)).Result;

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

        /// <summary>
        /// Отримання текстового представлення чека для термопринтеру згідно наказу №329 від 08.06.2021.
        /// </summary>
        /// <param name="receipt_id">receipt id</param>
        /// <param name="width">Кількість символів у рядку</param>
        /// <param name="properties">https://dev-api.checkbox.in.ua/api/docs#/%D0%A7%D0%B5%D0%BA%D0%B8/get_receipt_pdf_api_v1_receipts__receipt_id__pdf_get</param>
        /// <returns></returns>
        public byte[] GetReceiptTxt(Guid receipt_id, int width = 42)
        {

            var response = _client.GetAsync(string.Format("receipts/{0}/text?width={1}", receipt_id, width)).Result;

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

        /// <summary>
        /// Створення сервісного чеку внесення або винесення коштів. Для чеку сума винесення повинна бути вказана зі знаком мінус, а для внесення зі знаком плюс.
        /// </summary>
        /// <param name="receipt_id">receipt id</param>
        /// <param name="properties">https://api.checkbox.ua/api/docs#/%D0%A7%D0%B5%D0%BA%D0%B8/create_service_receipt_api_v1_receipts_service_post</param>
        /// <returns></returns>
        public ReceiptsSellRespond CreateServiceReceipt(ReceiptServicePayload properties)
        {
            string jsonString = JsonConvert.SerializeObject(properties);

            var response = _client.PostAsJsonAsync("receipts/service", properties).Result;

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

        #endregion

        #region Звіти
        /// <summary>
        /// Генерація X звіту
        /// </summary>
        /// <param name="properties">https://api.checkbox.ua/api/docs#/%D0%97%D0%B2%D1%96%D1%82%D0%B8/create_x_report_api_v1_reports_post</param>
        /// <returns>ReportModel</returns>
        public ReportModel CreateXReport()
        {
            var response = _client.PostAsJsonAsync("reports", new { }).Result;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var result = response.Content.ReadAsAsync<ReportModel>().Result;
                return result;
            }
            else
            {
                var apiErrorResponse = response.Content.ReadAsAsync<ErrorMessage>().Result;
                return new ReportModel
                {
                    error = apiErrorResponse
                };
            }
        }


        /// <summary>
        /// Отримання звіту в текстовому вигляді
        /// </summary>
        /// <param name="receipt_id">receipt id</param>
        /// <param name="width">Кількість символів у рядку</param>
        /// <param name="properties"></param>
        /// <returns></returns>
        public byte[] GetReportText(Guid receipt_id, int width = 42)
        {
            var response = _client.GetAsync(string.Format("reports/{0}/text?width={1}", receipt_id, width)).Result;

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
