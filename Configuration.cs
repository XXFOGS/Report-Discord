using Rocket.API;
using Rocket.Core.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Rocket.Unturned.Items;
using SDG.Unturned;

namespace ReportDiscord
{
    public class Configuration : IRocketPluginConfiguration
    {
        public string WebhookLink;
        public string WebhookName;
        public string WebhookAvatar;
        public string WebhookMessage;
        public string WebhookThumbnail;
        public string WebhookTitle;
        public string WebhookColor;
        public string WebhookFooter;
        public string WebhookFooterIcon;
        public string ImgBBKey;

        public void LoadDefaults()
        {
            WebhookLink = "https://discord.com/api/webhooks/xxxxxxxxxxxxxx/xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            WebhookAvatar = "https://i.imgur.com/7tjD5qr.png";
            WebhookMessage = "Report has been submitted <@&RoleId>. Spy of reported user has been attached.";
            WebhookName = "Report Webhook";
            WebhookThumbnail = "https://i.imgur.com/7tjD5qr.png";
            WebhookTitle = "Report";
            WebhookColor = "#FF0000";
            WebhookFooter = "Report Plugin, developed by XXFOGS";
            WebhookFooterIcon = "https://i.imgur.com/7tjD5qr.png";
            ImgBBKey = "GET YOUR API KEY HERE https://imgbb.com";
        }
    }
}
