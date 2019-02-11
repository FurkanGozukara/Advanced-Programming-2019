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

namespace week_2
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

        Dictionary<int, int> dicNumbers;

        private void btnRandomNumberRatios_Click(object sender, RoutedEventArgs e)
        {
            //here we are initilizating the variable. until here it is null
            dicNumbers = new Dictionary<int, int>();

            Random myrandGenerator = new Random();

            for (int i = 0; i < 1000000; i++)
            {
                //this will generate a random number between 1 and 101 [1,2,3,4...100]
                int irMyRandNumber = myrandGenerator.Next(1, 101);

                if (dicNumbers.ContainsKey(irMyRandNumber))
                {
                    dicNumbers[irMyRandNumber]++; // equals to dicNumbers[irMyRandNumber] = dicNumbers[irMyRandNumber]+1;
                    // equals to dicNumbers[irMyRandNumber]+=1;
                }
                else
                    dicNumbers.Add(irMyRandNumber, 1);
            }

            dicNumbers = dicNumbers.OrderBy(pr => pr.Key).ToDictionary(pr => pr.Key, pr => pr.Value);
            Dictionary<int, int> dicBigToSmall = dicNumbers.OrderByDescending(pr => pr.Key).ToDictionary(pr => pr.Key, pr => pr.Value);

            File.WriteAllLines("ordered by ascending.txt", dicNumbers.Select(pr => pr.Key + "\t" + pr.Value));

            using (StreamWriter swWrite = new StreamWriter("ordered by ascending 2.txt"))
            {
                foreach (var vrKeyAndValue in dicNumbers)
                {
                    swWrite.WriteLine(vrKeyAndValue.Key + "\t" + vrKeyAndValue.Value);
                }
            }

            File.Delete("ordered by ascending 3.txt");
            var vrNumbersList = dicNumbers.ToList();
            for (int i = 0; i < dicNumbers.Count; i++)
            {
                File.AppendAllText("ordered by ascending 3.txt",
                    vrNumbersList[i].Key + "\t" + vrNumbersList[i].Value + Environment.NewLine);
            }

            Dictionary<int, int> ReaderDict = new Dictionary<int, int>();

            using (StreamReader swRead = new StreamReader("ordered by ascending.txt"))
            {
                while (true)
                {
                    var vrLine = swRead.ReadLine();
                    if (vrLine == null)
                        break;

                    string srKeyLine = vrLine.Split('\t').First();
                    //string srKeyLine = vrLine.Split('\t')[0];

                    string srValueLine = vrLine.Split('\t').Last();
                    //string srValueLine = vrLine.Split('\t')[1];

                    int irKey = Convert.ToInt32(srKeyLine);
                    //int irKey = Int32.Parse(srKeyLine);

                    int irVal = Convert.ToInt32(srValueLine);

                    ReaderDict.Add(irKey, irVal);
                }
            }

            ReaderDict = new Dictionary<int, int>();

            foreach (var vrLine in File.ReadLines("ordered by ascending.txt"))
            {
                string srKeyLine = vrLine.Split('\t').First();

                string srValueLine = vrLine.Split('\t').Last();

                int irKey = Convert.ToInt32(srKeyLine);

                int irVal = Convert.ToInt32(srValueLine);

                ReaderDict.Add(irKey, irVal);
            }


        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            if(dicNumbers==null)
            {
                MessageBox.Show("please init dictionary first");
                return;
            }

            int irTextVal = 0;

            Int32.TryParse(txtNumber.Text, out irTextVal);

            if(dicNumbers.ContainsKey(irTextVal))
            {
                lblResult.Content = $"the number {irTextVal} has value of: {dicNumbers[irTextVal]}";
            }

            //dicNumbers.Add(32, 12);//throws key exists error
            //'An item with the same key has already been added.'

            dicNumbers[32] = 999992;

            int a = 65;
            a += 65; // a = a + 65;
            a--; // a = a - 1;
            dicNumbers[33] = a;
        }
    }
}
