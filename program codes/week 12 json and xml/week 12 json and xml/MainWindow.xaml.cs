using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace week_12_json_and_xml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [Serializable()]
        public class csPerson
        {
            public string srFirstName = "";
            public string srLastName = "";
            public int irId = 0;
        }

        private void generateJson_Click(object sender, RoutedEventArgs e)
        {
            List<csPerson> lstPersons = new List<csPerson>();
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                csPerson tempPerson = new csPerson();
                tempPerson.srFirstName = "first name " + rand.Next();
                tempPerson.srLastName = "last name " + rand.Next();
                tempPerson.irId = rand.Next();
                lstPersons.Add(tempPerson);
            }

            var obj = Newtonsoft.Json.JsonConvert.SerializeObject(lstPersons);

            File.WriteAllText("json.txt", obj);


            XmlSerializer writer = new XmlSerializer(typeof(List<csPerson>));
            Stream stream = File.OpenWrite("xmlClass.txt");
            writer.Serialize(stream, lstPersons);
            stream.Flush();
            stream.Close();

        }

        private void readJson_Click(object sender, RoutedEventArgs e)
        {
            List<csPerson> myPersons = Newtonsoft.Json.JsonConvert.DeserializeObject<List<csPerson>>(File.ReadAllText("json.txt"));

            XmlSerializer reader = new XmlSerializer(typeof(List<csPerson>));
            Stream stream = File.OpenRead("xmlClass.txt");
          
          List<csPerson> myPersons2=  (List<csPerson>) reader.Deserialize(stream);
            
        }
    }
}
