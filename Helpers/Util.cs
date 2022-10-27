using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ePaperWeb
{
    public class Util
    {
        public static string PasswordHash(string value)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(Encoding.UTF8.GetBytes(value))
                );
        }

        public class ObjectSerializer<T> where T : class
        {
            public static string Serialize(T obj)
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
                using (var sww = new StringWriter())
                {
                    using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
                    {
                        xsSubmit.Serialize(writer, obj);
                        return sww.ToString();
                    }
                }
            }
        }
    }
}