using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;

namespace ClickZan
{
    class Program
    {

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }

        static void Main(string[] args)
        {
            float count = 0;            
            for (int j = 0; j < 1000; j++)
            {
                string time = GetTimeStamp();
                Random ran = new Random();
                Double randomNumber = ran.NextDouble()*10000000000000000;
                for (int i = 0; i < 10; i++)
                {

                    string headersString = time + "_" + randomNumber.ToString("f0") + "_h5";
                    string respons;
                    respons = Post("http://my-api.app.xinhuanet.com//vote/vote", "voteUuid=8d3a88ba9120401d9bcec085c30fd3f9&optionId=230", headersString);  //地址-ID-随机号
                    Console.WriteLine(respons);
                    if (respons.Contains("1"))
                    {
                        Console.WriteLine("点赞成功");
                        count++;
                        Console.WriteLine("本次点赞次数累计:" + count);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("点赞失败！！！！！请注意！！！！");
                        goto aa;
                    }
                    Thread.Sleep(50);

                }
            }
            aa:
            Console.ReadLine();
        }

        public static string Post(string url, string content, string headersString)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            req.Accept = "application/json, text/javascript, */*; q=0.01";
            req.Headers.Add("Accept-Encoding", "gzip, deflate");
            req.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
            //req.Connection = "keep-alive";
            //req.Headers["Content-Length"] = "54";
            req.Headers["device-token"] = headersString;
            //req.Headers["device-token"] = "1627891131279_7006831777805327_5" + Convert.ToString(randomNumber);
            Console.WriteLine("Header:"+ headersString);
            //req.Headers["Host"] = "my-api.app.xinhuanet.com";
            req.Headers["Origin"] = "http://my-h5news.app.xinhuanet.com";
            req.Headers["platform"] = "h5";
            //req.Headers["Referer"] = "http://my-h5news.app.xinhuanet.com/h5activity/huihuangbainian/vote.html";
            //req.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";
            req.Headers["version"] = "8.7.2";



            #region 添加Post 参数
            byte[] data = Encoding.UTF8.GetBytes(content);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }



        public string Login(string RequestString, HttpContext context)
        {
            string url = "http://my-api.app.xinhuanet.com//vote/vote";
            string json = "";
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "post";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");
            request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Content-Length", "54");
            request.Headers.Add("device-token", "162278123186005_18024247777818425_12564");
            request.Headers.Add("Host", "my-api.app.xinhuanet.com");
            request.Headers.Add("Origin", "http://my-h5news.app.xinhuanet.com");
            request.Headers.Add("platform", "h5");
            request.Headers.Add("Referer", "http://my-h5news.app.xinhuanet.com/h5activity/huihuangbainian/vote.html");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36");
            request.Headers.Add("version", " 8.7.2");



            request.Accept = "application/json, text/javascript, */*; q=0.01";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            byte[] buffer = encoding.GetBytes(RequestString.ToString());
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }
    }
}
