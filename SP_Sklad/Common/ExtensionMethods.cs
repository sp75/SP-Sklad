using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.Common
{
    public static class ExtensionMethods
    {


        /// <summary>
        /// map properties
        /// </summary>
        /// <param name="sourceObj"></param>
        /// <param name="targetObj"></param>
        public static void MapProp(object sourceObj, object targetObj)
        {
            Type T1 = sourceObj.GetType();
            Type T2 = targetObj.GetType();

            PropertyInfo[] sourceProprties = T1.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] targetProprties = T2.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var sourceProp in sourceProprties)
            {
                object osourceVal = sourceProp.GetValue(sourceObj, null);
                int entIndex = Array.IndexOf(targetProprties, sourceProp);
                if (entIndex >= 0)
                {
                    var targetProp = targetProprties[entIndex];
                    targetProp.SetValue(targetObj, osourceVal);
                }
            }
        }

        /// <summary>
        /// Converts model from class F to class T
        /// </summary>
        /// <typeparam name="T">To Class</typeparam>
        /// <typeparam name="F">From Class</typeparam>
        /// <returns>model of type class T</returns>
        public static T Map<F, T>(this F from)
        {
            var json = JsonConvert.SerializeObject(from);
            var to = JsonConvert.DeserializeObject<T>(json);
            return to;
        }

    }
}
