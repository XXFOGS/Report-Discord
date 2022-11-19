using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Rocket.API;
using Rocket.Core;
using Rocket.Unturned;
using Rocket.Core.Logging;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Rocket.Core.Plugins;
using UP = Rocket.Unturned.Player.UnturnedPlayer;
using Rocket.API.Serialisation;
using Rocket.Unturned.Chat;
using SDG.Provider;
using Steamworks;
using UnityEngine;
using ReportDiscord;
using System.Globalization;

namespace ReportDiscord.Commands
{
    class ReportCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "report";

        public string Help => "alers staff members about a violation";

        public string Syntax => "<player> <reason>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions
        {
            get { return new List<string>() { "reportdiscord.report" }; }
        }

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = UnturnedPlayer.FromName(command[0]);
            var reason = string.Join(" ", command.Where(s => !string.IsNullOrEmpty(s) && s != command[0]));

            if (player != null) 
            {
                if (command.Length > 1)
                {
                    foreach (var steamPlayer in Provider.clients)
                    {
                        UnturnedPlayer staffMember = UnturnedPlayer.FromSteamPlayer(steamPlayer);

                        if (staffMember.HasPermission("reportdiscord.notify"))
                        {
                            staffMember.sendMessage($"[<color=red> Report </color>] <color=green>{caller.DisplayName}</color> reports <color=red>{player.DisplayName}</color> for: '{reason}'");
                        }
                    }
                    caller.sendMessage($"[<color=red> Report </color>] You have successfully reported <color=red>{player.DisplayName}</color> for '{reason}'");
                    player.Player.sendScreenshot(CSteamID.Nil, OnReceivedScreenshot);
                    void OnReceivedScreenshot(CSteamID player_0, byte[] jpg)
                    {
                        Task.Run(async () =>
                        {
                            string imageLink = await ImgBB.IMGBB.UploadAsync(jpg);
                            AdditionalMethods.sendDiscordWebhook(ReportDiscord.Instance.Configuration.Instance.WebhookLink, "{\"username\": \"" + ReportDiscord.Instance.Configuration.Instance.WebhookName + "\", \"avatar_url\": \"" + ReportDiscord.Instance.Configuration.Instance.WebhookAvatar + "\", \"content\": \"" + ReportDiscord.Instance.Configuration.Instance.WebhookMessage + "\", \"embeds\":[    { \"title\":\"" + ReportDiscord.Instance.Configuration.Instance.WebhookTitle + "\", \"color\":"+ int.Parse(ReportDiscord.Instance.Configuration.Instance.WebhookColor.Replace("#", string.Empty), System.Globalization.NumberStyles.HexNumber) +", \"thumbnail\": { \"url\": \"" + ReportDiscord.Instance.Configuration.Instance.WebhookThumbnail + "\"}, \"image\": { \"url\": \"" + imageLink + "\"}, \"footer\": { \"text\": \"" + ReportDiscord.Instance.Configuration.Instance.WebhookFooter + "\", \"icon_url\": \"" + ReportDiscord.Instance.Configuration.Instance.WebhookFooterIcon + "\"},  \"timestamp\": \"" + AdditionalMethods.discordDate() + "\", \"fields\": [ {\"name\": \"Reporter\", \"value\": \"" + caller.DisplayName + "\", \"inline\": \"true\"}, {\"name\": \"Reportee\", \"value\": \"" + player.DisplayName + "\", \"inline\": \"true\"}, {\"name\": \"Reportees SteamID\", \"value\": \"" + player.CSteamID + "\", \"inline\": \"true\"}, { \"name\": \"Reason\", \"value\": \"" + reason + "\" }  ]}]  }");
                        });
                    }  
                } else
                {
                    caller.sendMessage($"[<color=red> Report </color>] You have not specified report reason");
                }
            } else
            {
                caller.sendMessage($"[<color=red> Report </color>] Player not found");
            }
        }
    }
}
