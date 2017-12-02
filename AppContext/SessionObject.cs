using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProjectBase.AppContext
{
    /// <summary>
    /// Makes child object a session stored object. Object is stored in session with its name.
    /// </summary>
    public abstract class SessionObject
    {
        public SessionObject()
        {
            string sessionName = this.GetType().Name;
            if (HttpContext.Current != null)
                HttpContext.Current.Session["OBJ" + "_" + this.GetType().Name] = this;
        }
        /// <summary>
        /// Gets object from session.
        /// </summary>
        public static T GetObjectInstance<T>()
        {
            string TypeName = typeof(T).Name;

            if (HttpContext.Current != null && HttpContext.Current.Session["OBJ" + "_" + TypeName] != null)
                return (T)HttpContext.Current.Session["OBJ" + "_" + typeof(T).Name];
            else
                return default(T);
        }
        /// <summary>
        /// Clears object from session.
        /// </summary>
        public static void ClearObjectInstance<T>()
        {
            string TypeName = typeof(T).Name;
            if (HttpContext.Current != null)
                HttpContext.Current.Session["OBJ" + "_" + TypeName] = null;
        }
    }
}
