using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Sklad.SkladData;

namespace SP_Sklad.Common
{
    public sealed class AppSettingRepository : IDisposable
    {
        #region Init

        public AppSettingRepository( BaseEntities db)
        {
            _db = db;
            _settings_collection = _db.SettingApp.ToList();
        }

        public AppSettingRepository()
            : this(new BaseEntities())
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
            return _settings_collection.Any(a => a.SName == name);
        }

        #endregion

        #region Settings

        public void Set(string name, object value)
        {
            var _str_v = Convert.ToString(value);

            var us = _db.SettingApp.FirstOrDefault(f => f.SName == name );
            if (us != null)
            {
                us.SValue = _str_v;
            }
            else
            {
                _db.SettingApp.Add(new SettingApp { SName = name, SValue = _str_v });
            }

            _db.SaveChanges();
        }

        public string Get(string name)
        {
            return _settings_collection.Any(f => f.SName == name) ? _settings_collection.FirstOrDefault(f => f.SName == name).SValue : "";
        }

        #endregion



        #region show app
        /// <summary>
        /// Show Trade
        /// </summary>
        public bool ShowTradeApp
        {
            get
            {
                var v = Get("ShowTradeApp");
                return string.IsNullOrEmpty(v) ? false : Convert.ToBoolean(v);
            }
         
        }
        #endregion


        #region Validation

        private int GetRequiredInt(string setting_name)
        {
            var value = _settings_collection.FirstOrDefault(w => w.SName == setting_name).SValue;
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

       
        private readonly BaseEntities _db;
        private readonly List<SettingApp> _settings_collection;

        #endregion

        public void Dispose()
        {
            //      _db.Dispose();
        }
    }
}
