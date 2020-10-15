using System;
using System.IO;
using System.Xml.Serialization;

namespace Practice
{
    class MyConsole
    {
        //members are private by default in C#
        internal static double getDouble(string question)
        {
            Console.WriteLine(question);
            return double.Parse(Console.ReadLine());
        }

        internal static string getString(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        internal static int getNumber(string question)
        {
            return int.Parse(getString(question));
        }

        internal static DateTime getDate(string question)
        {
            return DateTime.Parse(getString(question));
        }
    }
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public long Phone { get; set; }

        public override string ToString()
        {
            return string.Format($"The name: {Name} from {Address} is available at {Phone}");
        }
    }
    class SerializationXML
    {
        static void Main(string[] args)
        {
            xmlExample();
            Console.ReadKey();
        }

        private static void xmlExample()
        {
            Console.WriteLine("What do U want to do today: Read or Write");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "r")
                deserializingXml();
            else
                serializingXml();
        }

        private static void deserializingXml()
        {
            try
            {
                XmlSerializer sl = new XmlSerializer(typeof(Student));
                FileStream fs = new FileStream("Data.xml", FileMode.Open, FileAccess.Read);
                Student s = (Student)sl.Deserialize(fs);
                //old style type casting(UNBOXING)
                Console.WriteLine(s);
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void serializingXml()
        {
            Student s = new Student();
            s.Name = MyConsole.getString("Enter the name");
            s.Address = MyConsole.getString("Enter the address");
            s.Phone = MyConsole.getNumber("Enter the landline Phone no");
            FileStream fs = new FileStream("Data.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer sl = new XmlSerializer(typeof(Student));
            sl.Serialize(fs, s);
            fs.Flush();//Clears the buffer into the destination so that no unused stream is left over before U close the Stream...
            fs.Close();

        }
    }
}
/*
Limits of serialization:
data can easily get corruptted. 
File reading and writing is not good for multi user environment. 
While writing U will not be able to read the data. 
If more users are using the program, scalability becomes a problem.
These limits are handled if U store the data in an external software called database...
*/
