using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.Core.Plugins;
using Steamworks;
using UnityEngine;
using Rocket.Unturned.Player;
using Rocket.Unturned.Chat;
using Rocket.API.Collections;
using System.Timers;

namespace DriverKick
{
    public class Plugin : RocketPlugin<PluginConfiguration>
    {
        public const string Discord = "discord.gg/4Fq2Spe";
        public static Plugin Instance;
        protected override void Load()
        {
            Instance = this;
            Rocket.Core.Logging.Logger.LogWarning("DriverKick by Anomoly!");
            Rocket.Core.Logging.Logger.LogWarning("Have an issue? Join my discord: " + Discord);
        }

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.LogWarning("DriverKick has unloaded!");
        }

        #region translations
        public override TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList() {
                    {"driverkick","You kicked {0} from their vehicle"},
                    {"not_driving", "{0} is not driving/flying!" },
                    {"not_in_vehicle", "{0} is not in a vehicle!" },
                    {"player_not_found", "Player not found!" },
                    {"driverkick_self","You kicked yourself from the vehicle!" },
                    {"syntax", "Please do /driverkick {0}" },
                    {"kicked", "You've been kicked from your vehicle by {0}!" },
                    {"not_driving_self","You're not driving!" },
                    {"not_in_vehicle_self", "You're not in a vehicle!" }
                };
            }
        }
        #endregion
    }
}
