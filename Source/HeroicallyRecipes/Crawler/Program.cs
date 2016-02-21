using AngleSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Crawler
{
    public class Program
    {
        static void Main()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var browsingContext = BrowsingContext.New(config);
            // http://cooking.sakam.net/?m=201510 date
            string localFilename = @"c:\localpath\tofile.jpg";
            using (WebClient client = new WebClient())
            {
                var data = client.DownloadData("http://cooking.sakam.net/wp-content/uploads/2016/02/3-1.jpg");

                //Read image data into a memory stream
                using (MemoryStream ms = new MemoryStream(data, 0, data.Length))
                {
                    ms.Write(data, 0, data.Length);
                    
                    //Set image variable value using memory stream.
                    System.Drawing.Image newImage = System.Drawing.Image.FromStream(ms, true);
                    newImage.Save("image.jpg");
                }


            }
            for (int i = 10; i < 7000; i++)
            {
                var url = string.Format("http://www.gotvetesmen.com/recipes/categories/salati/salati-ot-zelenchutsi/recipe_{0}.php", i);
                var document = browsingContext.OpenAsync(url).Result;

                var recipeContent = document.QuerySelector(".description").TextContent.Trim();
                var products = document.QuerySelector(".products ul").ChildNodes.ToList();

                if (!string.IsNullOrWhiteSpace(recipeContent))
                {
                    var preparation = string.Join("", recipeContent.Split(new char[] { '\u2022', '\n' })).Replace("Приготвяне:", "");

                    if (products != null)
                    {
                        foreach (var item in products)
                        {
                            string r = item.TextContent;
                        }
                    }
                }

            }
        }
    }
}
