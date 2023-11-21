using System;
using System.Collections.Generic;
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

namespace Kcots.Controls.Tabs
{
    /// <summary>
    /// Interaction logic for HomeTab.xaml
    /// </summary>
    public partial class HomeTab : UserControl
    {
        public HomeTab()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            //Get a list of all symbols that can be used in UI
           var QUERY_URL = "https://www.alphavantage.co/query?function=LISTING_STATUS&apikey=demo";

            Uri queryUri = new Uri(QUERY_URL);
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US"); ;
            using (WebClient client = new WebClient())
            {
                using (MemoryStream stream = new MemoryStream(client.DownloadDataTaskAsync(queryUri).Result))
                {
                    stream.Position = 0;

                    using (StreamReader reader = new StreamReader(stream))
                    {
                        using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            csv.Read();
                            csv.ReadHeader();
                            Console.WriteLine(string.Join("\t", csv.HeaderRecord));
                            while (csv.Read())
                            {
                                Console.WriteLine(string.Join("\t", csv.Parser.Record));
                            }
                        }
                    }
                }
            }
        }
    }
}
