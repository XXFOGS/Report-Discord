using Newtonsoft.Json;
using SDG.Unturned;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReportDiscord
{
    class ImgBB
    {
        public static class IMGBB
        {
            private const string Endpoint = "https://api.imgbb.com/1/upload";

            public static async Task<string> UploadAsync(byte[] data)
            {
                var req = WebRequest.CreateHttp(Endpoint);
                req.Method = "POST";
                var payload = Encoding.UTF8.GetBytes($"key={ReportDiscord.Instance.Configuration.Instance.ImgBBKey}&image={WebUtility.UrlEncode(Convert.ToBase64String(data))}&name=spy&expiration=86400");
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = payload.Length;

                using (var network = await req.GetRequestStreamAsync())
                {
                    await network.WriteAsync(payload, 0, payload.Length);
                    await network.FlushAsync();
                }
                var resp = (HttpWebResponse)await req.GetResponseAsync();

                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    var json = await sr.ReadToEndAsync();

                    var d = JsonConvert.DeserializeObject<ImgBBResult>(json);

                    return d.data.Display_Url;
                }
            }
        }

        public class ImgBBResult
        {
            public ImgBBData data;
        }

        public class ImgBBData
        {
            public string ID;
            public string Title;
            public string url_Viewer;
            public string Url;
            public string Display_Url;
            public int size;
            public int expiration;
            public long time;
        }
    }
}
