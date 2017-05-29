using Rocket.API;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Unturned.Commands;
using SDG;
using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;
using Rocket.Core.Steam;
using System;
using UnityEngine;

namespace DriverKick
{
    public class CommandVKickAll : IRocketCommand
    {
        public string Help
        {
            get { return "allows caller to kick all players from the vehicle"; }
        }

        public string Name
        {
            get { return "vkickall"; }
        }

        public string Syntax
        {
            get { return "<player>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>() { "vka","vkall" }; }
        }

        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "vkickall" };
            }
        }

        public void Execute(IRocketPlayer caller, params string[] command)
        {
            if (command.Length == 1)
            {
                UnturnedPlayer player = (UnturnedPlayer)caller;
                UnturnedPlayer otherplayer = UnturnedPlayer.FromName(command[0]);
                if (otherplayer == null)
                {
                    UnturnedChat.Say(caller, Plugin.Instance.Translate("player_not_found"), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                }
                else if (otherplayer.IsInVehicle)
                {
                    KickVehicle(otherplayer.CurrentVehicle);
                    UnturnedChat.Say(caller, Plugin.Instance.Translate("vkick"), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                }
                else
                {
                    UnturnedChat.Say(caller, Plugin.Instance.Translate("not_in_vehicle"), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                }
            }
            else if (command.Length == 0)
            {
                UnturnedPlayer player = (UnturnedPlayer)caller;
                KickVehicle(player.CurrentVehicle);
                UnturnedChat.Say(caller, Plugin.Instance.Translate("vkick_self"), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
            }
            else
            {
                UnturnedChat.Say(caller, Plugin.Instance.Translate("syntax_k"), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
            }
        }
        //Thanks Trojaner
        public static void KickVehicle(InteractableVehicle vehicle)
        {
            byte seat = 0;
            foreach (var passenger in vehicle.passengers)
            {
                vehicle.kickPlayer(seat);
                seat++;
            }
        }
    }
}
