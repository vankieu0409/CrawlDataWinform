using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

using Crawl_Data;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace suaThuocTinh
{
    public partial class Form1 : Form
    {
        private List<ProductVariant> listD;
        string path = @"E:\DATN\Crawl_Data\lap.json";
        public Form1()
        {
            listD = new List<ProductVariant>();
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamWriter file = File.CreateText(@"E:\DATN\Crawl_Data\lapfinal.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, SuaListOPtion(listD));
                MessageBox.Show("chuyển xong", "oke", MessageBoxButtons.OK);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string json = File.ReadAllText(path);
            listD = JsonConvert.DeserializeObject<List<ProductVariant>>(json);

            MessageBox.Show("oke", "oke", MessageBoxButtons.OK);

        }

        List<ProductVariant> SuaListOPtion(List<ProductVariant> a)
        {
            var result = new List<ProductVariant>();

            foreach (var item in a)
            {
                var temp = new ProductVariant();

                temp.Name = item.Name.Replace("\n", "").Replace("\t", "").Replace("\"", "");
                foreach (var VARIABLE in item.OptionValueColection)
                {

                    var optionNew = new Option_Value();
                    string reOption = Regex.Match(VARIABLE.Option, "<(.*?)>", RegexOptions.Singleline).Value;
                    if (reOption.Length != 0)
                        optionNew.Option = VARIABLE.Option.Replace(reOption, "");
                    else
                        optionNew.Option = VARIABLE.Option;


                    string newOOP = "";
                    string remove1 = Regex.Match(VARIABLE.Value, "<strong>(.*?)1668px", RegexOptions.Singleline).Value;
                    string remove2 = Regex.Match(VARIABLE.Value, "<strong>(.*?)630px", RegexOptions.Singleline).Value;
                    string remove4 = Regex.Match(VARIABLE.Value, "<strong>(.*?)511px", RegexOptions.Singleline).Value;
                    string remove5 = Regex.Match(VARIABLE.Value, "<strong>(.*?)78.3197%", RegexOptions.Singleline).Value;
                    string remove6 = Regex.Match(VARIABLE.Value, "<strong>(.*?)661px", RegexOptions.Singleline).Value;
                    string remove7 = Regex.Match(VARIABLE.Value, "<strong>(.*?)649px", RegexOptions.Singleline).Value;
                    string remove8 = Regex.Match(VARIABLE.Value, "<strong>(.*?)619px", RegexOptions.Singleline).Value;
                    string remove9 = Regex.Match(VARIABLE.Value, "<strong>(.*?)637px", RegexOptions.Singleline).Value;
                    string remove10 = Regex.Match(VARIABLE.Value, "<strong>(.*?)668px", RegexOptions.Singleline).Value;
                    string remove11 = Regex.Match(VARIABLE.Value, "<strong>(.*?)634px", RegexOptions.Singleline).Value;
                    string remove12 = Regex.Match(VARIABLE.Value, "<strong>(.*?)658px", RegexOptions.Singleline).Value;
                    string remove13 = Regex.Match(VARIABLE.Value, "<strong>(.*?)664px", RegexOptions.Singleline).Value;
                    string remove14 = Regex.Match(VARIABLE.Value, "<strong>(.*?)516px", RegexOptions.Singleline).Value;
                    string remove15 = Regex.Match(VARIABLE.Value, "<strong>(.*?)618px", RegexOptions.Singleline).Value;
                    string remove16 = Regex.Match(VARIABLE.Value, "<strong>(.*?)535px", RegexOptions.Singleline).Value;
                    string remove17 = Regex.Match(VARIABLE.Value, "<strong>(.*?)679px", RegexOptions.Singleline).Value;
                    string remove18 = Regex.Match(VARIABLE.Value, "<strong>(.*?)640px", RegexOptions.Singleline).Value;
                    string remove19 = Regex.Match(VARIABLE.Value, "<strong>(.*?)645px", RegexOptions.Singleline).Value;
                    string remove20 = Regex.Match(VARIABLE.Value, "<strong>(.*?)429px", RegexOptions.Singleline).Value;
                    string remove21 = Regex.Match(VARIABLE.Value, "<strong>(.*?)532px", RegexOptions.Singleline).Value;
                    string remove22 = Regex.Match(VARIABLE.Value, "<strong>(.*?)636px", RegexOptions.Singleline).Value;
                    string remove23 = Regex.Match(VARIABLE.Value, "<strong>(.*?)614px", RegexOptions.Singleline).Value;
                    string remove24 = Regex.Match(VARIABLE.Value, "<strong>(.*?)584px", RegexOptions.Singleline).Value;
                    string remove25 = Regex.Match(VARIABLE.Value, "<strong>(.*?)515px", RegexOptions.Singleline).Value;
                    string remove26 = Regex.Match(VARIABLE.Value, "<strong>(.*?)650px", RegexOptions.Singleline).Value;
                    string remove3 = Regex.Match(VARIABLE.Value, "<strong>(.*?)style=\"color:#000000", RegexOptions.Singleline).Value;
                    if (remove1.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove1, "");
                    else if (remove2.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove2, "");
                    else if (remove3.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove3, "");
                    else if (remove4.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove4, "");
                    else if (remove5.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove5, "");
                    else if (remove6.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove6, "");
                    else if (remove7.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove7, "");
                    else if (remove8.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove8, "");
                    else if (remove9.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove9, "");
                    else if (remove10.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove10, "");
                    else if (remove11.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove11, "");
                    else if (remove12.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove12, "");
                    else if (remove13.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove13, "");
                    else if (remove14.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove14, "");
                    else if (remove15.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove15, "");
                    else if (remove16.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove16, "");
                    else if (remove17.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove17, "");
                    else if (remove18.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove18, "");
                    else if (remove19.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove19, "");
                    else if (remove20.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove20, "");
                    else if (remove21.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove21, "");
                    else if (remove22.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove22, "");
                    else if (remove23.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove23, "");
                    else if (remove24.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove24, "");
                    else if (remove25.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove25, ""); 
                    else if (remove26.Length != 0)
                        newOOP = VARIABLE.Value.Replace(remove26, "");
                    else
                        newOOP = VARIABLE.Value;

                    string removeLink = Regex.Match(VARIABLE.Value, "<a(.*?)>", RegexOptions.Singleline).Value;
                    string dem = "";
                    if (removeLink.Length != 0)
                        dem = newOOP.Replace(removeLink, "").Replace("<br>", ", ").Replace("</p>", "").Replace("<span style=\"font-family:\"Arial\",\"sans-serif\"\">", "").Replace("<span style=\"line-height:107%\">", "").Replace("<ul>","").Replace("</ul>", "").Replace("<li>", ";").Replace("/li", " ");
                    else
                        dem = newOOP.Replace("<br>", ", ").Replace("</p>", "").Replace("</p>", "").Replace("<span style=\"font-family:\"Arial\",\"sans-serif\"\">", "").Replace("<span style=\"line-height:107%\">", "").Replace("<ul>", "").Replace("</ul>", "").Replace("<li>", "; ").Replace("/li", "");

                    optionNew.Value = dem.Replace("<", "").Replace(">", "").Replace("\"","").Replace("<span style=\"color:#000000", "");
                    
                    if ((optionNew.Option.Length != 0) && (optionNew.Value.Length != 0))
                    {
                        temp.OptionValueColection.Add(optionNew);
                    }

                    

                }

                foreach (var VARIABLE in item.ImageCollection)
                {
                    temp.ImageCollection.Add(VARIABLE);
                }


                result.Add(temp);

            }
            return result;
        }
    }

}