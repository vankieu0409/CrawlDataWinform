using Crawl_Data;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace CrawlDataWinform.Services;

public class CrawlLapPCService
{
    string HomePage = "https://gearvn.com";
    HttpClient httpClient;
    HttpClientHandler handler;
    CookieContainer cookie = new CookieContainer();

    public CrawlLapPCService()
    {
        IniHttpClient();
    }
    void IniHttpClient()
    {
        handler = new HttpClientHandler
        {
            CookieContainer = cookie,
            ClientCertificateOptions = ClientCertificateOption.Automatic,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            AllowAutoRedirect = true,
            UseDefaultCredentials = false
        };

        httpClient = new HttpClient(handler);

        //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/63.4.154 Chrome/57.4.2987.154 Safari/537.36");
        /*
         * Header:
         * - Origin
         * - Host
         * - Referer
         * - :scheme
         * - accept
         * - Accept-Encoding
         * - Accept-Language
         * - User-Argent
         */


        httpClient.BaseAddress = new Uri(HomePage);
    }

    public string CrawlDataFromURL(string url)
    {
        string html = "";

        html = WebUtility.HtmlDecode(httpClient.GetStringAsync(url).Result);

        //html = httpClient.PostAsync(url,new StringContent("")).Result.Content.ReadAsStringAsync().Result;

        return html;
    }

    public List<ProductVariant> Crawl(string url)
    {
        List<ProductVariant> listProduct = new List<ProductVariant>();
        string htmlLearn = CrawlDataFromURL(url);
        var CourseList = Regex.Matches(htmlLearn, @"<div class=""product-row"">(.*?)</span></div>", RegexOptions.Singleline);
        foreach (var course in CourseList)
        {
            #region Comment
            //List<MenuTreeItem> listProduct = new List<MenuTreeItem>();
            //var a = Regex.Match(course.ToString(), @"(?=<h3 class=""p-name "">).*?(?=</h3>)").Value;
            //var removeHref = Regex.Match(a.ToString(), @"<a href=""/(.*?)"">").Value;
            //var test = a.Replace(removeHref, "");
            ////string courseName = Regex.Match(a.Replace(removeHref, "").ToString(), @"<h3 class=""p-name "">(.*?)</a>").Value.Replace("<h3 class=\"p-name \">", "").Replace("</a>", ""); 
            #endregion
            string linkCourse = Regex.Match(course.ToString(), @"<a href=""(.*?)"">", RegexOptions.Singleline).Value.Replace("<a href=\"", "").Replace("\">", "");

            // AddItemIntoTreeViewItem(TreeItems, item);

            string htmlCourse = CrawlDataFromURL(linkCourse);
            string sideBar = Regex.Match(htmlCourse, @"<div id=""mainframe"">(.*?)<div role=""tabpanel"" class=""tab-pane"" id=""hrvproducttabs"">", RegexOptions.Singleline).Value;//.Replace(" ","");
            string name = Regex.Match(sideBar, @"<h1 class=""product_name"">(.*?)</h1>", RegexOptions.Singleline).Value.Replace("<h1 class=\"product_name\">", "").Replace("</h1>", "");
            var testsss = Regex.Match(sideBar, @"<span class=""sku"">(.*?)</span>", RegexOptions.Singleline).Value;
            string skuId = Regex.Match(sideBar, @"<span class=""sku"">(.*?)</span>", RegexOptions.Singleline).Value.Replace("<span class=\"sku\">", "").Replace("</span>", "");
            string price = Regex.Match(sideBar, @"<span class=""product_sale_price"">(.*?)</span>", RegexOptions.Singleline).Value.Replace("<span class=\"product_sale_price\">", "").Replace("</span>", "").Replace("₫", "").Replace(",", "");
            if (!string.IsNullOrEmpty(price))
            {
                ProductVariant productVariant = new ProductVariant();
                productVariant.Name = name;
                productVariant.SkuId = skuId;
                productVariant.Price = Convert.ToInt64(price);


                var scopedInfomation = Regex.Match(sideBar, @"<tbody style=""(.*?)></table>",
                    RegexOptions.Singleline).Value;
                var listOPtionValueProduct =
                    Regex.Matches(scopedInfomation, @"<tr(.*?)</tr>", RegexOptions.Singleline);

                foreach (var lecture in listOPtionValueProduct)
                {
                    #region lấy option

                    string option = "";
                    var temp = Regex.Match(lecture.ToString(), @"<strong>(.*?)</strong>", RegexOptions.Singleline).Value.Replace("<strong>", "").Replace("</strong>", "").Replace("<span style=\"font-size:16px\">", "").Replace("</span>", "").Replace("</a>", "").Replace($@"<span style=""color:#000000"">", "");//.Replace("/", "");
                    var removeHref = Regex.Match(temp.ToString(), @"<a(.*?)"">", RegexOptions.Singleline).Value;
                    if (!string.IsNullOrEmpty(removeHref)) option = temp.Replace(removeHref, "");
                    else option = temp.ToString();

                    #endregion
                    #region Lấy value

                    string value = "";
                    var tempValue = Regex.Match(lecture.ToString(), @"""><span style=""font-size:16px"">(.*?)</td></tr>", RegexOptions.Singleline).Value.Replace("\"><span style=\"font-size:16px\">", "").Replace("</td></tr>", "");//.Replace("<span style=\"font-size:16px\">", "").Replace("</span>", "").Replace("</a>", "").Replace($@"<span style=""color:#000000"">", "");//.Replace("/", "");
                    string removeHrefValue = Regex.Match(tempValue.ToString(), @"<a(.*?)"">", RegexOptions.Singleline).Value;
                    string removeHrefValue2 = Regex.Match(tempValue.ToString(), @"<strong>(.*?)px<span", RegexOptions.Singleline).Value.Replace("<span","");
                    if (!string.IsNullOrEmpty(removeHrefValue)&& !string.IsNullOrEmpty(removeHrefValue2)) value = tempValue.Replace(removeHrefValue, "").Replace(removeHrefValue2, "");
                    else if (!string.IsNullOrEmpty(removeHrefValue)) value = tempValue.Replace(removeHrefValue, "");
                    else value = tempValue.ToString(); 

                    #endregion
                    if (!string.IsNullOrEmpty(option) && !string.IsNullOrEmpty(value))
                    {
                        Option_Value Subitem = new Option_Value();
                        Subitem.Option = option;
                        Subitem.Value = value.Replace("<span style=\"font-size:16px\">", "").Replace("</span>", "").Replace("</a>", "").Replace($@"<span style=""color:#000000"">", "");//.Replace("/", "");;
                        productVariant.OptionValueColection.Add(Subitem);
                    }
                }

                var scopedImage = Regex.Match(sideBar, @"<div id=""mainframe"">(.*?)<h1 class=""product_name"">", RegexOptions.Singleline).Value;
                var listImage = Regex.Matches(scopedImage, @"<img src=""(.*?)/>", RegexOptions.Singleline);
                foreach (var lecture in listImage)
                {
                    string image = "";
                    string temp= Regex.Match(lecture.ToString(), @"<img src=""(.*?)/>", RegexOptions.Singleline).Value.Replace("<img src=\"", "").Replace("/>", "");
                    string removeHrefValue = Regex.Match(temp.ToString(), @"alt=""(.*?)""", RegexOptions.Singleline).Value;
                    if (!string.IsNullOrEmpty(removeHrefValue)) image= temp.Replace(removeHrefValue, "").Replace(@"\","").Replace('"', ' ');
                    else image = temp.ToString().Replace("alt=","").Replace("\"","");
                    if (!string.IsNullOrEmpty(image))
                        productVariant.ImageCollection.Add(image);
                }

                listProduct.Add(productVariant);
            }
        }
        return listProduct;
    }


    [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IOleServiceProvider
    {
        [PreserveSig]
        int QueryService([In] ref Guid guidService, [In] ref Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out object ppvObject);
    }

}