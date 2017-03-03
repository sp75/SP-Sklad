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

            public UserSettingsRepository(int user_id , BaseEntities db)
            {
                _user_id = user_id;
                _db = db ;
                _settings_collection = _db.UserSettings.Where(w => w.UserId == user_id).ToList();
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

            public void Set(string name, string value)
            {

                var us = _db.UserSettings.FirstOrDefault(f => f.Name == name && f.UserId == _user_id);
                if (us != null)
                {
                    us.Value = value;
                }
                else{
                    _db.UserSettings.Add(new UserSettings { Name = name, Value = value, UserId = _user_id });
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

            #endregion


            #region Validation

            private int GetRequiredInt(string setting_name)
            {
                var value = _settings_collection.FirstOrDefault(w=> w.Name == setting_name ).Value ;
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
