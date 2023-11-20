using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Final_Lesson
{
    public delegate void action(User user);
    public class Program
    {
       
        static void Main(string[] args)
        {
            string input_json_file = "C:\\Users\\Ж - 4\\Гоголин\\CS_Final_Lesson\\CS_Final_Lesson\\students.json";
            string output_xml_file = "C:\\Users\\Ж - 4\\Гоголин\\CS_Final_Lesson\\CS_Final_Lesson\\students_output.xml";
            string output_json_file = "C:\\Users\\Ж - 4\\Гоголин\\CS_Final_Lesson\\CS_Final_Lesson\\students_output.json";
            string output_txt_file = "C:\\Users\\Ж - 4\\Гоголин\\CS_Final_Lesson\\CS_Final_Lesson\\students_output.txt";

            UserStorage userStorage = new UserStorage();
            FileManager fileManager = new FileManager();

            userStorage.Users = fileManager.LoadFromFileJson(input_json_file);
            //userStorage.Users = fileManager.LoadFromTxtFile(output_txt_file);
            userStorage.UserAdded += (User u) => { Console.WriteLine("Пользователь " + 
                                                    u.ToString() + 
                                                    " добавлен."); };
            userStorage.UserDeleted += (User u) => { Console.WriteLine("Пользователь " + u.ToString() +
                                                    " удалён."); };
            int choice = 0;

            do
            {

                do
                {
                    Console.WriteLine(userStorage.ToString());
                    Console.WriteLine("Введите номер пункта меню: ");
                    Console.WriteLine("1. Добавление нового пользователя.");
                    Console.WriteLine("2. Получение пользователя по его идентификатору");
                    Console.WriteLine("3. Обновление информации о пользователе.");
                    Console.WriteLine("4. Удаление пользователя.");
                    Console.WriteLine("5. Выход.");

                    choice = Convert.ToInt32(Console.ReadLine());

                } while (choice < 1 && choice > 5);

                if(choice == 1)
                {
                    AddUser(userStorage);
                }else if(choice == 2)
                {
                    GetUserByID(userStorage);
                }else if(choice == 3)
                {
                    UpdateInformation(userStorage);
                }
                else if(choice == 4)
                {
                    DeleteUserByID(userStorage);
                }else if(choice == 5)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка ввода!");
                }
                Console.ReadKey();
                Console.Clear();
            } while (true);

            WriteToXMLFile(fileManager, userStorage, output_xml_file);
            WriteToJsonFile(fileManager, userStorage, output_json_file);
            WriteToTxtFile(fileManager, userStorage, output_txt_file);

        }

        public static void AddUser(UserStorage userStorage)
        {
            Console.WriteLine("Введите Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите имя пользователя: ");
            string nameUser = Console.ReadLine();
            Console.WriteLine("Введите email пользователя: ");
            string email = Console.ReadLine();
            userStorage.AddUser(new User(id, nameUser, email));
        }

        public static void GetUserByID(UserStorage userStorage)
        {
            Console.WriteLine("Введите Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            User u = userStorage.GetUser(id);
            Console.WriteLine(u.ToString());
        }

        public static void UpdateInformation(UserStorage userStorage)
        {
            Console.WriteLine("Введите Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите имя пользователя: ");
            string nameUser = Console.ReadLine();
            Console.WriteLine("Введите email пользователя: ");
            string email = Console.ReadLine();
            User u = userStorage.GetUser(id);
            u.Username = nameUser;
            u.Email = email;
        }

        public static void DeleteUserByID(UserStorage userStorage)
        {
            Console.WriteLine("Введите Id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            userStorage.DeleteUser(id);
        }

        public static void WriteToXMLFile(FileManager fileManager, UserStorage userStorage, string path)
        {
            fileManager.SaveToFileXML(path, userStorage.Users);
        }

        public static void WriteToJsonFile(FileManager fileManager, UserStorage userStorage, string path)
        {
            fileManager.SaveToFileJson(path, userStorage.Users);
        }

        public static void WriteToTxtFile(FileManager fileManager, UserStorage userStorage, string path)
        {
            fileManager.SaveToTxtFile(path, userStorage.Users);
        }

        

    }
}
