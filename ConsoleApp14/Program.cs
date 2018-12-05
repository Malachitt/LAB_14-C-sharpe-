using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace ConsoleApp14
{
	class Program
	{
		static void Main(string[] args)
		{
			TV_Program kanal1 = new TV_Program("Tom", "Ege", "NTV");
			TV_Program kanal11;
			TV_Program kanal2 = new TV_Program("Tom", "Bob", "TNT");
			TV_Program kanal22;
			TV_Program kanal3 = new TV_Program("Tom", "Eva", "NTV"); ;
			TV_Program kanal33;
			List<object> kanalOb = new List<object>();
			kanalOb.Add(123);
			kanalOb.Add("stroka");
			kanalOb.Add(456);
			List<object> kanal1Ob = new List<object>();
			//S
			BinaryFormatter binFormat = new BinaryFormatter();
			using (Stream fStream = new FileStream("TV_Program_S.dat", FileMode.Create, FileAccess.Write, FileShare.None))
			{
				binFormat.Serialize(fStream, kanal1);
				Console.WriteLine("Объект сериализован BinFile");
			}
			//D
			using (FileStream fStream = new FileStream("TV_Program_S.dat", FileMode.OpenOrCreate))
			{
				TV_Program newTV_Program = (TV_Program)binFormat.Deserialize(fStream);

				Console.WriteLine("Объект десериализован BinFile");
			}
			//S
			DataContractJsonSerializer jsonFormat = new DataContractJsonSerializer(typeof(TV_Program));
			using (Stream fStream = new FileStream("TV_Program_S.json", FileMode.Create, FileAccess.Write, FileShare.None))
			{
				jsonFormat.WriteObject(fStream, kanal1);
				Console.WriteLine("Объект сериализован JSON");
			}
			//D
			//var myNewObject = JsonConvert.DeserializeObject<TV_Program>(kanal1);
			//S
			XmlSerializer xmlFormat = new XmlSerializer(typeof(TV_Program));
			using (Stream fStream = new FileStream("TV_Program_S.xml", FileMode.Create, FileAccess.Write, FileShare.None))
			{
				xmlFormat.Serialize(fStream, kanal1);
				Console.WriteLine("Объект сериализован XML");
			}
			//D
			using (FileStream fStream = new FileStream("TV_Program_S.xml", FileMode.Open))
			{
				kanal11 = (TV_Program)xmlFormat.Deserialize(fStream);

				Console.WriteLine("Объект десериализован XML");
			}
			//S
			XmlSerializer xmlFormat1 = new XmlSerializer(kanalOb.GetType());
			using (Stream stream = new FileStream("kanalOb_S.xml", FileMode.Create, FileAccess.Write))
			{
				xmlFormat1.Serialize(stream, kanalOb);

				Console.WriteLine("Массив объектов сериализован");
				stream.Close();
			}
			//D
			using (Stream stream = new FileStream("kanalOb_S.xml", FileMode.Open))
			{
				foreach (object o in (List<object>)xmlFormat1.Deserialize(stream))
					kanal1Ob.Add(o);
				Console.WriteLine("Массив объектов десериализован");
				stream.Close();
			}
	
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load("kanalOb_S.xml");

			XmlElement xRoot = xmlDoc.DocumentElement;
			XmlElement user = xmlDoc.CreateElement("user");
			XmlElement name = xmlDoc.CreateElement("name");
			XmlElement surname = xmlDoc.CreateElement("surname");
			XmlElement age = xmlDoc.CreateElement("age");
			XmlText nameText = xmlDoc.CreateTextNode("Alexander");
			XmlText surnameText = xmlDoc.CreateTextNode("Peretyatko");
			XmlText ageText = xmlDoc.CreateTextNode("18");

			age.AppendChild(ageText);
			name.AppendChild(nameText);
			surname.AppendChild(surnameText);
			user.AppendChild(name);
			user.AppendChild(surname);
			user.AppendChild(age);
			xRoot.AppendChild(user);
			xmlDoc.Save("kanalOb_S.xml");



			XDocument xDoc = new XDocument();
			XElement user1 = new XElement("user");
			XElement name1 = new XElement("name");
			XElement surname1 = new XElement("surname");
			XElement age1 = new XElement("age");

			name1.Add("Alexander");
			surname1.Add("Peretyatko");
			age1.Add("18");
			user1.Add(name1);
			user1.Add(surname1);
			user1.Add(age1);
			xDoc.Add(user1);
			xDoc.Save("Linq.xml");

			Console.ReadLine();
		}
	}
	[Serializable]
	public class TV_Program
	{
		public string NameProgram { get; set; }
		public string Name{ get; set; }
		public string LastName { get; set; }
		public TV_Program()
		{

		}
		public TV_Program(string name, string surname, string nameProgram) 
		{
			Name = name;
			LastName = surname;
			NameProgram = nameProgram;
		}
		public virtual void Display()
		{
			Console.WriteLine($"Вызов метода Display в классе TV_Program - Producer {Name} {LastName} program {NameProgram}");
		}
	}
}
