using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CS_Final_Lesson
{
    public class FileManager
    {
        public void SaveToFileJson(string path, List<User> users)
        {
            string outputJson = JsonConvert.SerializeObject(users);

            System.IO.File.WriteAllText(path, outputJson);
        }

        public List<User> LoadFromFileJson(string path) 
        {
            List<User> users = new List<User>();

            string json = System.IO.File.ReadAllText(path);
            users = JsonConvert.DeserializeObject<List<User>>(json);

            return users;
        }

        public void SaveToFileXML(string path, List<User> users)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<User>));
            using (FileStream fstream = new FileStream(path, FileMode.Truncate))
            {
                xml.Serialize(fstream, users);
            }
        }

        public List<User> LoadFromFileXML(string path)
        {
            List<User> users = new List<User>();

            XDocument xdoc = XDocument.Load(path);
            XElement flowers = xdoc.Element("Users");


            if (flowers != null)
            {

                foreach (XElement element in flowers.Elements("User"))
                {

                    int Id = Convert.ToInt32(element.Element("Id")?.Value);
                    string userName = element.Element("UserName")?.Value;
                    string email = element.Element("Email")?.Value;

                    users.Add(new User(Id, userName, email));

                }

            }

            return users;
        }

        public void SaveToTxtFile(string path, List<User> users)
        {
            UnicodeEncoding unicode = new UnicodeEncoding();
            using (StreamWriter writer = new StreamWriter(path, false, unicode))    // запись буфера в файл
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < users.Count; i++)                      
                {
                    stringBuilder.Append(users[i].ToString() + "\n");            
                }
                writer.WriteLine(stringBuilder.ToString());
            }
        }

        public List<User> LoadFromTxtFile(string path)
        {
            UnicodeEncoding unicode = new UnicodeEncoding();
            List<User> users = new List<User>();
            string s = "";
            StreamReader f = new StreamReader(path);
            while ((s = f.ReadLine()) != null)
            {
                string[] words = s.Split(new char[] { ' ', '\n', '\r', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length > 0)
                {
                    int id = Convert.ToInt32(words[1]);

                    string nameUser = words[3] + " " + words[4];

                    string email = words[6];
                    users.Add(new User(id, nameUser, email));
                }
            }

            
            return users;
        }
    }
}
