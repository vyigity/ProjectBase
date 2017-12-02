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

    /// <summary>
    /// Includes beneficial util functions.
    /// </summary>
    public static class Util
    {
        public static string EmailRegExp = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";

        public static string DateInStringRegEx = @"(0[1-9]|[12][0-9]|3[01]|[1-9])[- /.](0[1-9]|1[012]|[1-9])[- /.](19|2)\d\d\d";

        /// <summary>
        /// Generates HTML coded list from a enumerable object.
        /// </summary>
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
        /// <summary>
        /// Generates HTML coded table from a data table object.
        /// </summary>
        public static string GenerateHTMLTable(DataTable data, string cssClass, string cssClassName)
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
        /// <summary>
        /// Serializes a object to XML
        /// </summary>
        public static string SerializeObjectXml<T>(T obj)
        {          
            StringWriter swriter = new StringWriter();
            XmlSerializer sxml = new XmlSerializer(typeof(T));
            sxml.Serialize(swriter, obj);
            return swriter.ToString();
        }
        /// <summary>
        /// Serializes a object to XML according to given object type.
        /// </summary>
        public static string SerializeObjectXml(object obj, Type objType)
        {
            StringWriter swriter = new StringWriter();
            XmlSerializer sxml = new XmlSerializer(objType);
            sxml.Serialize(swriter, obj);
            return swriter.ToString();
        }
        /// <summary>
        /// Deserializes a object from XML
        /// </summary>
        public static T DeSeriliazeObjectXml<T>(string xmlString)
        {
            XmlSerializer sxml = new XmlSerializer(typeof(T));
            return (T)sxml.Deserialize(new StringReader(xmlString));
        }
        /// <summary>
        /// Deserializes a object from XML according to given object type.
        /// </summary>
        public static object DeSeriliazeObjectXml(string xmlString, Type objectType)
        {
            XmlSerializer sxml = new XmlSerializer(objectType);
            return sxml.Deserialize(new StringReader(xmlString));
        }
        /// <summary>
        /// Serializes a object to binary Array
        /// </summary>
        public static byte[] SerializeObjectBinary(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);

            return stream.ToArray();
        }
        /// <summary>
        /// Deserializes a object from binary Array
        /// </summary>
        public static object DeSerializeObjectBinary(byte[] objBinary)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(objBinary, 0, objBinary.Length);
            stream.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream); ;
        }
        /// <summary>
        /// If value is null or System.DbNull, returns true.
        /// </summary>
        public static bool IsNull(object obj)
        {
            return obj == System.DBNull.Value || obj == null;
        }
        /// <summary>
        /// Converts a object data type to another acorrding to T.
        /// </summary>
        public static T GetProperty<T>(object obj)
        {
            if (IsNull(obj))
                return default(T);
            else
                return (T)Convert.ChangeType(obj, typeof(T));
        }
        /// <summary>
        /// Converts a object data type to another acorrding to T. If value is null, returns given default value.
        /// </summary>
        public static T GetProperty<T>(object obj, T defaultValue)
        {
            if (IsNull(obj))
                return defaultValue;
            else
                return (T)Convert.ChangeType(obj, typeof(T));
        }
        /// <summary>
        /// Converts a object data type to another acorrding to given object type.
        /// </summary>
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
        /// <summary>
        /// Converts a object data type to another acorrding to given object type. If value is null, returns given default value.
        /// </summary>
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
        /// <summary>
        /// Converts a nullable object data type to another acorrding to T.
        /// </summary>
        public static Nullable<T> GetPropertyNullable<T>(object obj) where T : struct
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
        /// <summary>
        /// Converts a object's all properties' values to upper case.
        /// </summary>
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
        /// <summary>
        /// Converts a object's all properties' values to upper case except some given properties.
        /// </summary>
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
        /// <summary>
        /// Converts a object's all properties' values to lower case.
        /// </summary>
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
        /// <summary>
        /// Converts a object's all properties' values to lower case except some given properties.
        /// </summary>
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
        /// <summary>
        /// Converts a string value to upper case. If string value is null, returns null.
        /// </summary>
        public static string GetUpperString(object val)
        {
            if (!IsNull(val))
            {
                return val.ToString().ToUpper(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }
        /// <summary>
        /// Converts a string value to lower case. If string value is null, returns null.
        /// </summary>
        public static string GetLowerString(object val)
        {
            if (!IsNull(val))
            {
                return val.ToString().ToLower(Thread.CurrentThread.CurrentCulture);
            }
            else
                return null;
        }
        /// <summary>
        /// Converts a string value to space cleared upper case. If string value is null, returns null.
        /// </summary>
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
        /// <summary>
        /// Converts a string value to space cleared lower case. If string value is null, returns null.
        /// </summary>
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
        /// <summary>
        /// Clears a string from spaces. If string value is null, returns null.
        /// </summary>
        public static string ClearString(object val)
        {
            if (IsNull(val) || string.IsNullOrWhiteSpace(val.ToString()))
            {
                return null;
            }
            else
                return val.ToString().Trim();
        }
        /// <summary>
        /// Copies a object's properties' values to another.
        /// </summary>
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
        /// <summary>
        /// Generates a dynamic object from a object.
        /// </summary>
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
        /// <summary>
        /// Copies a object's properties' values to another with a deeper methot.
        /// </summary>
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
        /// <summary>
        /// Copies a object's properties' values to another with a deeper methot.
        /// </summary>
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
        /// <summary>
        /// Get class names in a given assembly name and namespace.
        /// </summary>
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
        /// <summary>
        /// Get class and interface names in a given assembly name and namespace.
        /// </summary>
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
        /// <summary>
        /// Detects property value changes between two objects.
        /// </summary>
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
        /// <summary>
        /// Finds date string in a given string with regular expression.
        /// </summary>
        public static DateTime? FindDateInString(string searchString)
        {
            Regex ex = new Regex(DateInStringRegEx);
            var match = ex.Matches(searchString);

            if (match.Count > 0)
                return Util.GetProperty<DateTime>(match[0].Value);
            else
                return null;
        }
        /// <summary>
        /// Returns english localized string from given turkish localized string.
        /// </summary>
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
        /// <summary>
        /// Returns zipped file that includes given files.
        /// </summary>
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
