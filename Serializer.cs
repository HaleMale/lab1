using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace lab1
{
    class Serializer
    {
        public static T Load<T>(string _fileName)
        {
            TextReader _writer = null;

            try
            {
                XmlSerializer _Serializer = new XmlSerializer(typeof(T));
                _writer = new StreamReader(_fileName);

                return (T)_Serializer.Deserialize(_writer);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return default(T);
            }
            finally
            {
                if (_writer != null)
                    _writer.Close();
            }
        }

        public static bool Save<T>(T obj, string _file)
        {
            TextWriter _writer = null;
            try
            {
                XmlSerializer _Serializer = new XmlSerializer(typeof(T));
                _writer = new StreamWriter(_file);
                _Serializer.Serialize(_writer, obj);
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
            finally
            {
                if (_writer != null)
                    _writer.Close();
            }

        }
    }
}
