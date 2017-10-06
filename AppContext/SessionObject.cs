using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectBase.AppContext
{
    public abstract class SessionObject
    {
        public SessionObject()
        {
            string sessionName = this.GetType().Name;
            if (HttpContext.Current != null)
                HttpContext.Current.Session["OBJ" + "_" + this.GetType().Name] = this;
        }

        public static T GetObjectInstance<T>()
        {
            string TypeName = typeof(T).Name;

            if (HttpContext.Current != null && HttpContext.Current.Session["OBJ" + "_" + TypeName] != null)
                return (T)HttpContext.Current.Session["OBJ" + "_" + typeof(T).Name];
            else
                return default(T);
        }

        public static void ClearObjectInstance<T>()
        {
            string TypeName = typeof(T).Name;
            if (HttpContext.Current != null)
                HttpContext.Current.Session["OBJ" + "_" + TypeName] = null;
        }
    }
}
