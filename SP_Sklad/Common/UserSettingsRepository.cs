using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Sklad.SkladData;

namespace SP_Sklad.Common
{
    public sealed class UserSettingsRepository : IDisposable
    {
        #region Init

        public UserSettingsRepository(int user_id, BaseEntities db)
        {
            _user_id = user_id;
            _db = db;
            _settings_collection = _db.UserSettings.Where(w => w.UserId == _user_id).ToList();
        }

        public UserSettingsRepository()
            : this(UserSession.UserId, new BaseEntities())
        {
        }

        #endregion

        #region Fuctions

        /// <summary>
        /// Check if setting is specified
        /// </summary>
        /// <param name="name">Setting name</param>
        /// <returns>True - if setting exists and not empty, false otherwise</returns>
        public bool HasValue(string name)
        {
            return _settings_collection.Any(a => a.Name == name);
        }

        #endregion

        #region Settings

        public void Set(string name, object value)
        {
            var _str_v = Convert.ToString(value);

            var us = _db.UserSettings.FirstOrDefault(f => f.Name == name && f.UserId == _user_id);
            if (us != null)
            {
                us.Value = _str_v;
            }
            else
            {
                _db.UserSettings.Add(new UserSettings { Name = name, Value = _str_v, UserId = _user_id });
            }

            _db.SaveChanges();
        }

        public string Get(string name)
        {
            return _settings_collection.Any(f => f.Name == name) ? _settings_collection.FirstOrDefault(f => f.Name == name).Value : "";
        }

        #endregion



        #region comport
        /// <summary>
        /// COMPORT: Name
        /// </summary>
        public string ComPortName
        {
            get { return Get("COM_PORT_NAME"); }
            set { Set("COM_PORT_NAME", value); }
        }

        /// <summary>
        /// COMPORT: speed
        /// </summary>
        public string ComPortSpeed
        {
            get { return Get("COM_PORT_SPEED"); }
            set { Set("COM_PORT_SPEED", value); }
        }

        /// <summary>
        /// External Access: edit weight
        /// </summary>
        public string AccessEditWeight
        {
            get { return Get("ACCESS_EDIT_WEIGHT"); }
            set { Set("ACCESS_EDIT_WEIGHT", value); }
        }

        /// <summary>
        /// External Access: edit personid
        /// </summary>
        public string AccessEditPersonId
        {
            get { return Get("ACCESS_EDIT_PERSONID"); }
            set { Set("ACCESS_EDIT_PERSONID", value); }
        }

        /// <summary>
        /// External Access: edit price
        /// </summary>
        public bool AccessEditPrice
        {
            get
            {
                var v = Get("ACCESS_EDIT_PRICE");
                return string.IsNullOrEmpty(v) ? false : Convert.ToBoolean(v);
            }
            set { Set("ACCESS_EDIT_PRICE", value); }
        }


        /// <summary>
        /// External Access: edit doc_num
        /// </summary>
        public bool AccessEditDocNum
        {
            get
            {
                var v = Get("ACCESS_EDIT_DOCNUM");
                return string.IsNullOrEmpty(v) ? false : Convert.ToBoolean(v);
            }
            set
            {
                Set("ACCESS_EDIT_DOCNUM", value);
            }
        }

        public decimal GridFontSize
        {
            get
            {
                var v = Get("GRID_FONT_SIZE");
                return string.IsNullOrEmpty(v) ? 10 : Convert.ToDecimal(v);
            }
            set { Set("GRID_FONT_SIZE", value); }
        }

        public string GridFontName
        {
            get
            {
                var v = Get("GRID_FONT_NAME");
                return string.IsNullOrEmpty(v) ? "Tahoma" : v;
            }
            set { Set("GRID_FONT_NAME", value); }
        }

        public int DefaultBuyer
        {
            get
            {
                var v = Get("DEFAULT_BUYER");
                return string.IsNullOrEmpty(v) ? DBHelper.Kagents.FirstOrDefault().KaId : Convert.ToInt32(v);
            }
            set { Set("DEFAULT_BUYER", value); }
        }

        public int DefaultChargeTypeByRMK
        {
            get
            {
                var v = Get("DEFAULT_CHARGE_TYPE_RMK");
                return string.IsNullOrEmpty(v) ? DBHelper.ChargeTypes.FirstOrDefault(f => f.Def == 1).CTypeId : Convert.ToInt32(v);
            }
            set { Set("DEFAULT_CHARGE_TYPE_RMK", value); }
        }

        public int CashDesksDefaultRMK
        {
            get
            {
                var v = Get("CASHDESK_DEFAULT_RMK");
                return string.IsNullOrEmpty(v) ? 0 : Convert.ToInt32(v);
            }
            set { Set("CASHDESK_DEFAULT_RMK", value); }
        }

        public int AccountDefaultRMK
        {
            get
            {
                var v = Get("ACCOUNT_DEFAULT_RMK");
                return string.IsNullOrEmpty(v) ? 0 : Convert.ToInt32(v);
            }
            set { Set("ACCOUNT_DEFAULT_RMK", value); }
        }


        public string CashierLoginCheckbox
        {
            get
            {
                var v = Get("CASHIER_LOGIN_RMK");
                return string.IsNullOrEmpty(v) ? "" : v;
            }
            set { Set("CASHIER_LOGIN_RMK", value); }
        }

        public string CashierPasswordCheckbox
        {
            get
            {
                var v = Get("CASHIER_PASS_RMK");
                return string.IsNullOrEmpty(v) ? "" : v;
            }
            set { Set("CASHIER_PASS_RMK", value); }
        }

        /// <summary>
        /// not show message copy documents
        /// </summary>
        public bool NotShowMessageCopyDocuments
        {
            get
            {
                var v = Get("NOT_SHOW_MESSAGE_COPY_DOCUMENTS");
                return string.IsNullOrEmpty(v) ? false : Convert.ToBoolean(v);
            }
            set { Set("NOT_SHOW_MESSAGE_COPY_DOCUMENTS", value); }
        }

        #endregion


        #region Validation

        private int GetRequiredInt(string setting_name)
        {
            var value = _settings_collection.FirstOrDefault(w => w.Name == setting_name).Value;
            int int_value;
            if (int.TryParse(value, out int_value))
            {
                return int_value;
            }

            var error_message = string.Format("Setting {0} is required to have int value.", setting_name);
            throw new Exception(error_message);
        }

        #endregion

        #region Fields

        private readonly int _user_id;
        private readonly BaseEntities _db;
        private readonly List<UserSettings> _settings_collection;

        #endregion

        public void Dispose()
        {
            //      _db.Dispose();
        }
    }
}
