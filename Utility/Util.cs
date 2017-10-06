using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProjectBase.Utility
{
    public struct ByteFile { public string FileName; public byte[] Data;}

    public static class Util
    {
        public static string EmailRegExp = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public static string DateInStringRegEx = @"(0[1-9]|[12][0-9]|3[01]|[1-9])[- /.](0[1-9]|1[012]|[1-9])[- /.](19|2)\d\d\d";
        
        public static string GenerateHTMLList(IEnumerable<string> data)
        {
            StringBuilder builder = new StringBuilder("<ul>");

            foreach (string item in data)
            {
                builder.Append("<li>");
                builder.Append(item);
                builder.Append("</li>");
            }

            builder.Append("</ul>");

            return builder.ToString();
        }

        public static string generateHTMLTable(DataTable data, string cssClass, string cssClassName)
        {
            StringBuilder builder = new StringBuilder();

            if (cssClass != null)
            {
                builder.Append("<style>");
                builder.Append(cssClass);
                builder.Append("</style>");
            }

            if (cssClassName == null)
            {
                builder.Append("<table>");
            }
            else
            {
                builder.Append("<table class=\"" + cssClassName + "\">");
            }

            builder.Append("<thead>");
            foreach (DataColumn item in data.Columns)
            {
                builder.Append("<th>");
                builder.Append(item.ColumnName);
                builder.Append("</th>");
            }
            builder.Append("</thead>");

            builder.Append("<tbody>");
            foreach (DataRow item in data.Rows)
            {
                builder.Append("<tr>");
                foreach (DataColumn item2 in data.Columns)
                {
                    builder.Append("<td>");
                    builder.Append(item[item2]);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
            }

            builder.Append("</tbody>");
            builder.Append("</table>");

            return builder.ToString();
        }

        public static string SerializeObjectXml<T>(T obj)
        {          
            StringWriter swriter = new StringWriter();
            XmlSerializer sxml = new XmlSerializer(typeof(T));
            sxml.Serialize(swriter, obj);
            return swriter.ToString();
        }

        public static string SerializeObjectXml(object obj, Type objType)
        {
            StringWriter swriter = new StringWriter();
            XmlSerializer sxml = new XmlSerializer(objType);
            sxml.Serialize(swriter, obj);
            return swriter.ToString();
        }

        public static T DeSeriliazeObjectXml<T>(string xmlString)
        {
            XmlSerializer sxml = new XmlSerializer(typeof(T));
            return (T)sxml.Deserialize(new StringReader(xmlString));
        }

        public static object DeSeriliazeObjectXml(string xmlString, Type objectType)
        {
            XmlSerializer sxml = new XmlSerializer(objectType);
            return sxml.Deserialize(new StringReader(xmlString));
        }

        public static byte[] SerializeObjectBinary(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);

            return stream.ToArray();
        }

        public static object DeSerializeObjectBinary(byte[] objBinary)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(objBinary, 0, objBinary.Length);
            stream.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream); ;
        }

        public static bool IsNull(object obj)
        {
            return obj == System.DBNull.Value || obj == null;
        }

        public static T GetProperty<T>(object obj)
        {
            if (IsNull(obj))
                return default(T);
            else
                return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T GetProperty<T>(object obj, T defaultValue)
        {
            if (IsNull(obj))
                return defaultValue;
            else
                return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static object GetProperty(object obj, Type objectType)
        {
            Type  objectTypeTmp = null;

            if (IsNull(obj))
                return null;
            else
            {
                if (Nullable.GetUnderlyingType(objectType) != null)
                {
                    objectTypeTmp = Nullable.GetUnderlyingType(objectType);
                }
                else
                    objectTypeTmp = objectType;

                return Convert.ChangeType(obj, objectTypeTmp);
            }
        }

        public static object GetProperty(object Obj, Type objectType, object defaultValue)
        {
            Type objectTypeTmp = null;

            if (IsNull(Obj))
                return defaultValue;
            else
            {
                if (Nullable.GetUnderlyingType(objectType) != null)
                {
                    objectTypeTmp = Nullable.GetUnderlyingType(objectType);
                }
                else
                    objectTypeTmp = objectType;

                return Convert.ChangeType(Obj, objectTypeTmp);
            }
        }

        public static Nullable<T> getPropertyNullable<T>(object obj) where T : struct
        {
            try
            {
                if (IsNull(obj))
                    return null;
                else
                    return (Nullable<T>)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                return null;
            }
        }

        public static T ToUpperProperty<T>(T obj)
        {
            var props = typeof(T).GetProperties();

            foreach (PropertyInfo inf in props)
            {
                object val = inf.GetValue(obj);

                if (val != null)
                {
                    if (inf.PropertyType == typeof(string))
                    {
                        inf.SetValue(obj, val.ToString().ToUpper(Thread.CurrentThread.CurrentCulture));
                    }
                }
            }

            return obj;
        }

        public static T ToUpperProperty<T>(T obj, params string[] exceptionProperties)
        {
            var props = typeof(T).GetProperties();

            foreach (PropertyInfo inf in props)
            {
                object val = inf.GetValue(obj);

                if (val != null)
                {
                    if (inf.PropertyType == typeof(string) && !exceptionProperties.Contains(inf.Name))
                    {
                        inf.SetValue(obj, val.ToString().ToUpper(Thread.CurrentThread.CurrentCulture));
                    }
                }

            }

            return obj;
        }

        public static T ToLowerProperty<T>(T obj)
        {
            var props = typeof(T).GetProperties();

            foreach (PropertyInfo inf in props)
            {
                object val = inf.GetValue(obj);

                if (val != null)
                {
                    if (inf.PropertyType == typeof(string))
                    {
                        inf.SetValue(obj, val.ToString().ToLower(Thread.CurrentThread.CurrentCulture));
                    }
                }

            }

            return obj;
        }

        public static T ToLowerProperty<T>(T obj, params string[] exceptionProperties)
        {
            var props = typeof(T).GetProperties();

            foreach (PropertyInfo inf in props)
            {
                object val = inf.GetValue(obj);

                if (val != null)
                {
                    if (inf.PropertyType == typeof(string) && !exceptionProperties.Contains(inf.Name))
                    {
                        inf.SetValue(obj, val.ToString().ToLower(Thread.CurrentThread.CurrentCulture));
                    }
                }

            }

            return obj;
        }

        public static string GetUpperString(object val)
        {
            if (!IsNull(val))
            {
                return val.ToString().ToUpper(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }

        public static string GetLowerString(object val)
        {
            if (!IsNull(val))
            {
                return val.ToString().ToLower(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }

        public static string GetClearUpperString(object val)
        {
            string value = ClearString(val);

            if (value != null)
            {
                return value.ToUpper(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }

        public static string GetClearLowerString(object val)
        {
            string value = ClearString(val);

            if (value != null)
            {
                return value.ToLower(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }

        public static string ClearString(object val)
        {
            if (IsNull(val) || string.IsNullOrWhiteSpace(val.ToString()))
            {
                return null;
            }
            else
                return val.ToString().Trim();
        }

        public static T CopyObject<T>(object from, T to)
        {
            Type tfrom = from.GetType();
            Type tto = typeof(T);

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                foreach (PropertyInfo from_property in tfrom.GetProperties())
                {
                    if (from_property.Name == to_property.Name)
                    {
                        to_property.SetValue(to, from_property.GetValue(from));
                        break;
                    }
                }
            }

            return to;            
        }

        public static object GetDynamicObject(object convertObject, Dictionary<string, object> extraProperty)
        {
            dynamic expando = new ExpandoObject();
            var dic = expando as IDictionary<string, object>;
            Type tto = convertObject.GetType();

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                dic.Add(to_property.Name, to_property.GetValue(convertObject));
            }

            if(extraProperty != null)
                foreach (var val in extraProperty)
                {
                    int i = 0;
                    string oldKey = val.Key;
                    string newKey = oldKey;

                    while (dic.Keys.Contains(newKey))
                    {
                        newKey = oldKey + "_" + ++i;
                    }

                    dic.Add(newKey, val.Value);
                }

            return expando;
        }

        public static T CopyObjectDeeper<T>(object from, T to)
        {
            Type tfrom = from.GetType();
            Type tto = typeof(T);
            object newProperty = null;

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                foreach (PropertyInfo from_property in tfrom.GetProperties())
                {
                    if (from_property.Name == to_property.Name)
                    {
                        if (from_property.GetValue(from) != null && !(from_property.GetValue(from) is IConvertible))
                        {
                            if (to_property.GetValue(to) == null)
                            {
                               newProperty = Activator.CreateInstance(to_property.PropertyType);
                            }

                            CopyObjectDeeper(from_property, newProperty, to_property.PropertyType);
                        }

                        to_property.SetValue(to, from_property.GetValue(from));
                        break;
                    }
                }
            }

            return to;
        }

        public static object CopyObjectDeeper(object from, object to, Type objectType)
        {
            Type tfrom = from.GetType();
            Type tto = objectType;
            object newProperty = null;

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                foreach (PropertyInfo from_property in tfrom.GetProperties())
                {
                    if (from_property.Name == to_property.Name)
                    {
                        if (from_property.GetValue(from) != null && !(from_property.GetValue(from) is IConvertible))
                        {
                            if (to_property.GetValue(to) == null)
                            {
                                newProperty = Activator.CreateInstance(to_property.PropertyType);
                            }

                            CopyObjectDeeper(from_property, newProperty, to_property.PropertyType);
                        }

                        to_property.SetValue(to, from_property.GetValue(from));
                        break;
                    }
                }
            }

            return to;
        }

        public static List<string> GetClassList(string assemblyName, string nameSpace, bool removeNameSpace)
        {
            if (!removeNameSpace)
            {
                return Assembly.Load(assemblyName).GetTypes().ToList().Where(t => t.Namespace == nameSpace).Select(r => r.FullName).ToList();
            }
            else
            {
                return Assembly.Load(assemblyName).GetTypes().ToList().Where(t => t.Namespace == nameSpace).Select(r => r.Name).ToList();
            }
        }

        public static List<string> GetClassListWithInterface(string assemblyName, string nameSpace, bool removeNameSpace, params string[] interfaces)
        {
            if (!removeNameSpace)
            {
                return Assembly.Load(assemblyName).GetTypes().ToList().Where(t => t.Namespace == nameSpace && t.GetInterfaces().ToList().Exists(r=>interfaces.Contains(r.Name))).Select(r => r.FullName).ToList();
            }
            else
            {
                return Assembly.Load(assemblyName).GetTypes().ToList().Where(t => t.Namespace == nameSpace && t.GetInterfaces().ToList().Exists(r => interfaces.Contains(r.Name))).Select(r => r.Name).ToList();
            }
        }

        public static List<PropertyDifference> DetectPropertyChanges<T>(T object1, T object2, params string[] exceptionProperties)
        {
            List<PropertyDifference> ChangedProperties = new List<PropertyDifference>();
            Type tto = typeof(T);

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                if (!exceptionProperties.Contains(to_property.Name))
                {
                    object pvalue1 = to_property.GetValue(object1);
                    object pvalue2 = to_property.GetValue(object2);

                    if (!object.Equals(pvalue1, pvalue2))
                    {
                        ChangedProperties.Add(new PropertyDifference
                        {
                            PropertyName = to_property.Name,
                            OldValue = pvalue1,
                            NewValue = pvalue2

                        });
                    }
                }
            }

            return ChangedProperties;
        }

        public static DateTime? FindDateInString(string searchString)
        {
            Regex ex = new Regex(DateInStringRegEx);
            var match = ex.Matches(searchString);

            if (match.Count > 0)
                return Util.GetProperty<DateTime>(match[0].Value);
            else
                return null;
        }

        public static string GetEnglishLocalizedString(string searchString)
        {
            if (!IsNull(searchString))
            {
                char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
                char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

                for (int i = 0; i < turkishChars.Length; i++)
                {
                    searchString = searchString.Replace(turkishChars[i], englishChars[i]);
                }

                return searchString;
            }
            else
                return null;
        }

        public static byte[] ZipThat(IEnumerable<ByteFile> files)
        {
            using (MemoryStream mst = new MemoryStream())
            {
                using (ZipArchive arc = new ZipArchive(mst, ZipArchiveMode.Create))
                {
                    foreach (var item in files)
                    {
                        var zipEntry = arc.CreateEntry(item.FileName);

                        using (Stream st = zipEntry.Open())
                        {
                            st.Write(item.Data, 0, item.Data.Length);
                        }
                    }                 
                }

                return mst.ToArray();
            }
        }
    }
}
