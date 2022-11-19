using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Rocket.API;
using Rocket.Core;
using Rocket.Unturned;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Rocket.Core.Plugins;
using UP = Rocket.Unturned.Player.UnturnedPlayer;
using System.Text.RegularExpressions;

namespace ReportDiscord
{
    public static class AdditionalMethods
    {
        public static void sendDiscordWebhook(string url, string escapedJson)
        {
            var WebReq = WebRequest.Create(url);
            WebReq.ContentType = "application/json";
            WebReq.Method = "POST";
            using (var StrWrt = new StreamWriter(WebReq.GetRequestStream())) StrWrt.Write(escapedJson);
            WebResponse Res = WebReq.GetResponse();
            StreamReader Reader = new StreamReader(Res.GetResponseStream());
            string str = Reader.ReadLine();
            while (str != null)
            {
                Console.WriteLine(str);
                str = Reader.ReadLine();
            }
            Reader.Close();
            Res.Close();
        }

        public static string discordDate()
        {
            return DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
