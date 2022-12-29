using Crawl_Data;
using CrawlDataWinform.Services;
using Microsoft.VisualBasic.Devices;

using Newtonsoft.Json;

namespace CrawlDataWinform
{
    public partial class MainUI : Form
    {
        private CrawlLapPCService _service;
        public MainUI()
        {
            InitializeComponent();
            _service = new CrawlLapPCService();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string page = textBox_CrawlPc.Text;
            //Task t = new Task(() => { Crawl("/laptop-acer"); });
            //t.Start();
            var listProducts= _service.Crawl(page);
            foreach (var item in listProducts.ToList())
            {
                if (item.OptionValueColection.Count==0)
                {
                    var temp = listProducts.FindIndex(c => c.Name.Contains(item.Name));
                    listProducts.RemoveAt(temp);
                }
            }

            ExportToFileWithJson(listProducts);

            MessageBox.Show($@"cào thành công {listProducts.Count} sản Phẩm", "Chán lắm!",MessageBoxButtons.OK);
        }

        void ExportToFileWithJson(List<ProductVariant> values)
        { var list=new List<ProductVariant>();
            if (File.Exists(@"D:\DATN\Crawl_Data\Main.json"))
            {
                using (StreamReader r = new StreamReader(@"D:\DATN\Crawl_Data\Main.json"))
                {
                    string json = r.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<ProductVariant>>(json);
                }
            }

            list.AddRange(values);
            //var json = JsonConvert.SerializeObject(values,Formatting.Indented);
            //json = json.Replace(@"\", "");
            //var bearks = 0;
            // tuần tự hóa JSON thành một chuỗi và sau đó ghi chuỗi vào tệp 
            // File.WriteAllText( @"E:\movie.json", json);

            // tuần tự hóa JSON trực tiếp thành một tệp 

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(@"D:\DATN\Crawl_Data\Main.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, list);
            }
        }
    }
}