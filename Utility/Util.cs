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
        
        public static string generateHTMLList(IEnumerable<string> Data)
        {
            StringBuilder builder = new StringBuilder("<ul>");

            foreach (string item in Data)
            {
                builder.Append("<li>");
                builder.Append(item);
                builder.Append("</li>");
            }

            builder.Append("</ul>");

            return builder.ToString();
        }

        public static string generateHTMLTable(DataTable Data, string CssClass, string CssClassName)
        {
            StringBuilder builder = new StringBuilder();

            if (CssClass != null)
            {
                builder.Append("<style>");
                builder.Append(CssClass);
                builder.Append("</style>");
            }

            if (CssClassName == null)
            {
                builder.Append("<table>");
            }
            else
            {
                builder.Append("<table class=\"" + CssClassName + "\">");
            }

            builder.Append("<thead>");
            foreach (DataColumn item in Data.Columns)
            {
                builder.Append("<th>");
                builder.Append(item.ColumnName);
                builder.Append("</th>");
            }
            builder.Append("</thead>");

            builder.Append("<tbody>");
            foreach (DataRow item in Data.Rows)
            {
                builder.Append("<tr>");
                foreach (DataColumn item2 in Data.Columns)
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

        public static string SerializeObjectXml<T>(T Obj)
        {          
            StringWriter swriter = new StringWriter();
            XmlSerializer sxml = new XmlSerializer(typeof(T));
            sxml.Serialize(swriter, Obj);
            return swriter.ToString();
        }

        public static string SerializeObjectXml(object Obj, Type ObjType)
        {
            StringWriter swriter = new StringWriter();
            XmlSerializer sxml = new XmlSerializer(ObjType);
            sxml.Serialize(swriter, Obj);
            return swriter.ToString();
        }

        public static T DeSeriliazeObjectXml<T>(string XmlString)
        {
            XmlSerializer sxml = new XmlSerializer(typeof(T));
            return (T)sxml.Deserialize(new StringReader(XmlString));
        }

        public static object DeSeriliazeObjectXml(string XmlString, Type ObjectType)
        {
            XmlSerializer sxml = new XmlSerializer(ObjectType);
            return sxml.Deserialize(new StringReader(XmlString));
        }

        public static byte[] SerializeObjectBinary(object Obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, Obj);

            return stream.ToArray();
        }

        public static object DeSerializeObjectBinary(byte[] ObjBinary)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(ObjBinary, 0, ObjBinary.Length);
            stream.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream); ;
        }

        public static bool IsNull(object Obj)
        {
            return Obj == System.DBNull.Value || Obj == null;
        }

        public static T getProperty<T>(object Obj)
        {
            if (IsNull(Obj))
                return default(T);
            else
                return (T)Convert.ChangeType(Obj, typeof(T));
        }

        public static T getProperty<T>(object Obj, T Default)
        {
            if (IsNull(Obj))
                return Default;
            else
                return (T)Convert.ChangeType(Obj, typeof(T));
        }

        public static object getProperty(object Obj, Type ObjectType)
        {
            Type  objectType = null;

            if (IsNull(Obj))
                return null;
            else
            {
                if (Nullable.GetUnderlyingType(ObjectType) != null)
                {
                    objectType = Nullable.GetUnderlyingType(ObjectType);
                }
                else
                    objectType = ObjectType;

                return Convert.ChangeType(Obj, objectType);
            }
        }

        public static object getProperty(object Obj, Type ObjectType, object DefaultValue)
        {
            Type objectType = null;

            if (IsNull(Obj))
                return DefaultValue;
            else
            {
                if (Nullable.GetUnderlyingType(ObjectType) != null)
                {
                    objectType = Nullable.GetUnderlyingType(ObjectType);
                }
                else
                    objectType = ObjectType;

                return Convert.ChangeType(Obj, objectType);
            }
        }

        public static Nullable<T> getPropertyNullable<T>(object Obj) where T : struct
        {
            try
            {
                if (IsNull(Obj))
                    return null;
                else
                    return (Nullable<T>)Convert.ChangeType(Obj, typeof(T));
            }
            catch
            {
                return null;
            }
        }

        public static bool IsTcKimlikNo(string Data)
        {
            if (11 != Data.Length)  // Girilen sayı 11 haneli olmak zorunda
            {
                return false;
            }
            else
            {
                int toplam = 0; // 1. Açıklama

                for (int i = 0; i < Data.Length - 1; i++) // 2. Açıklama
                {
                    toplam += Convert.ToInt32(Data[i].ToString()); // 3. Açıklama
                }

                if (toplam.ToString()[1] == Data[10]) // 4. Açıklama  
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public static T ToUpperProperty<T>(T obj, params string[] ExceptionProperties)
        {
            var props = typeof(T).GetProperties();

            foreach (PropertyInfo inf in props)
            {
                object val = inf.GetValue(obj);

                if (val != null)
                {
                    if (inf.PropertyType == typeof(string) && !ExceptionProperties.Contains(inf.Name))
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

        public static T ToLowerProperty<T>(T obj, params string[] ExceptionProperties)
        {
            var props = typeof(T).GetProperties();

            foreach (PropertyInfo inf in props)
            {
                object val = inf.GetValue(obj);

                if (val != null)
                {
                    if (inf.PropertyType == typeof(string) && !ExceptionProperties.Contains(inf.Name))
                    {
                        inf.SetValue(obj, val.ToString().ToLower(Thread.CurrentThread.CurrentCulture));
                    }
                }

            }

            return obj;
        }

        public static string GetUpperString(object Value)
        {
            if (!IsNull(Value))
            {
                return Value.ToString().ToUpper(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }

        public static string GetLowerString(object Value)
        {
            if (!IsNull(Value))
            {
                return Value.ToString().ToLower(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }

        public static string GetClearUpperString(object Value)
        {
            string value = ClearString(Value);

            if (value != null)
            {
                return value.ToUpper(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }

        public static string GetClearLowerString(object Value)
        {
            string value = ClearString(Value);

            if (value != null)
            {
                return value.ToLower(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }

        public static string ClearString(object Value)
        {
            if (IsNull(Value) || string.IsNullOrWhiteSpace(Value.ToString()))
            {
                return null;
            }
            else
                return Value.ToString().Trim();
        }

        public static T CopyObject<T>(object From, T To)
        {
            Type tfrom = From.GetType();
            Type tto = typeof(T);

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                foreach (PropertyInfo from_property in tfrom.GetProperties())
                {
                    if (from_property.Name == to_property.Name)
                    {
                        to_property.SetValue(To, from_property.GetValue(From));
                        break;
                    }
                }
            }

            return To;            
        }

        public static object GetDynamicObject(object ConvertObject, Dictionary<string, object> ExtraProperty)
        {
            dynamic expando = new ExpandoObject();
            var dic = expando as IDictionary<string, object>;
            Type tto = ConvertObject.GetType();

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                dic.Add(to_property.Name, to_property.GetValue(ConvertObject));
            }

            if(ExtraProperty != null)
                foreach (var val in ExtraProperty)
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

        public static T CopyObjectDeeper<T>(object From, T To)
        {
            Type tfrom = From.GetType();
            Type tto = typeof(T);
            object newProperty = null;

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                foreach (PropertyInfo from_property in tfrom.GetProperties())
                {
                    if (from_property.Name == to_property.Name)
                    {
                        if (from_property.GetValue(From) != null && !(from_property.GetValue(From) is IConvertible))
                        {
                            if (to_property.GetValue(To) == null)
                            {
                               newProperty = Activator.CreateInstance(to_property.PropertyType);
                            }

                            CopyObjectDeeper(from_property, newProperty, to_property.PropertyType);
                        }

                        to_property.SetValue(To, from_property.GetValue(From));
                        break;
                    }
                }
            }

            return To;
        }

        public static object CopyObjectDeeper(object From, object To, Type ObjectType)
        {
            Type tfrom = From.GetType();
            Type tto = ObjectType;
            object newProperty = null;

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                foreach (PropertyInfo from_property in tfrom.GetProperties())
                {
                    if (from_property.Name == to_property.Name)
                    {
                        if (from_property.GetValue(From) != null && !(from_property.GetValue(From) is IConvertible))
                        {
                            if (to_property.GetValue(To) == null)
                            {
                                newProperty = Activator.CreateInstance(to_property.PropertyType);
                            }

                            CopyObjectDeeper(from_property, newProperty, to_property.PropertyType);
                        }

                        to_property.SetValue(To, from_property.GetValue(From));
                        break;
                    }
                }
            }

            return To;
        }

        public static List<string> GetClassList(string AssemblyName, string NameSpace, bool RemoveNameSpace)
        {
            if (!RemoveNameSpace)
            {
                return Assembly.Load(AssemblyName).GetTypes().ToList().Where(t => t.Namespace == NameSpace).Select(r => r.FullName).ToList();
            }
            else
            {
                return Assembly.Load(AssemblyName).GetTypes().ToList().Where(t => t.Namespace == NameSpace).Select(r => r.Name).ToList();
            }
        }

        public static List<string> GetClassListWithInterface(string AssemblyName, string NameSpace, bool RemoveNameSpace, params string[] Interfaces)
        {
            if (!RemoveNameSpace)
            {
                return Assembly.Load(AssemblyName).GetTypes().ToList().Where(t => t.Namespace == NameSpace && t.GetInterfaces().ToList().Exists(r=>Interfaces.Contains(r.Name))).Select(r => r.FullName).ToList();
            }
            else
            {
                return Assembly.Load(AssemblyName).GetTypes().ToList().Where(t => t.Namespace == NameSpace && t.GetInterfaces().ToList().Exists(r => Interfaces.Contains(r.Name))).Select(r => r.Name).ToList();
            }
        }

        public static List<PropertyDifference> DetectPropertyChanges<T>(T Object1, T Object2, params string[] ExceptionProperties)
        {
            List<PropertyDifference> ChangedProperties = new List<PropertyDifference>();
            Type tto = typeof(T);

            foreach (PropertyInfo to_property in tto.GetProperties())
            {
                if (!ExceptionProperties.Contains(to_property.Name))
                {
                    object pvalue1 = to_property.GetValue(Object1);
                    object pvalue2 = to_property.GetValue(Object2);

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

        public static DateTime? findDateInString(string SearchString)
        {
            Regex ex = new Regex(DateInStringRegEx);
            var match = ex.Matches(SearchString);

            if (match.Count > 0)
                return Util.getProperty<DateTime>(match[0].Value);
            else
                return null;
        }

        public static string GetEnglishLocalizedString(string SearchString)
        {
            if (!IsNull(SearchString))
            {
                char[] turkishChars = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
                char[] englishChars = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };

                for (int i = 0; i < turkishChars.Length; i++)
                {
                    SearchString = SearchString.Replace(turkishChars[i], englishChars[i]);
                }

                return SearchString;
            }
            else
                return null;
        }

        public static byte[] ZipThat(IEnumerable<ByteFile> Files)
        {
            using (MemoryStream mst = new MemoryStream())
            {
                using (ZipArchive arc = new ZipArchive(mst, ZipArchiveMode.Create))
                {
                    foreach (var item in Files)
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
