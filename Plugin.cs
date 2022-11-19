using System;
using System.Timers;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using Rocket.API;
using Rocket.Core;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Items;
using Rocket.Unturned.Player;
using Rocket.Unturned.Enumerations;
using Rocket.Unturned.Plugins;
using Rocket.Core.Logging;
using Rocket.API.Collections;
using Rocket.Core.Plugins;
using SDG.Unturned;
using Steamworks;
using SDG;
using UnityEngine;
using UnityEngine.Events;
using UP = Rocket.Unturned.Player.UnturnedPlayer;
using Rocket.API.Serialisation;
using Rocket.Unturned.Chat;
using SDG.Provider;
using Logger = Rocket.Core.Logging.Logger;

namespace ReportDiscord
{
    public class ReportDiscord : RocketPlugin<Configuration>
    {
        public static ReportDiscord Instance;
        public string Creator = "XXFOGS";
        public string Version = "1.0.0";

        protected override void Load()
        {
            Instance = this;

            Logger.Log($"ReportDiscord by {Creator} has been loaded! Version: {Version}");
            
        }

        protected override void Unload()
        {
            Logger.Log("ReportDiscord has been unloaded");
        }
    }
}
