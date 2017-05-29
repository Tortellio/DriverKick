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
    public class CommandDriverKick : IRocketCommand
    {
        public string Help
        {
            get { return "allows caller to kick a driver from their vehicle"; }
        }

        public string Name
        {
            get { return "driverkick"; }
        }

        public string Syntax
        {
            get { return "<player>"; }
        }

        public List<string> Aliases
        {
            get { return new List<string>() { "kickdriver","dkick" }; }
        }

        public AllowedCaller AllowedCaller
        {
            get { return AllowedCaller.Player; }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>() { "driverkick" };
            }
        }

        public void Execute(IRocketPlayer caller, params string[] command)
        {
            if(command.Length == 1)
            {
                UnturnedPlayer player = (UnturnedPlayer)caller;
                UnturnedPlayer otherplayer = UnturnedPlayer.FromName(command[0]);
                if(otherplayer == null)
                {
                    UnturnedChat.Say(caller, Plugin.Instance.Translate("player_not_found"),UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                    return;
                }
                else if(otherplayer.Id == caller.Id)
                {
                    player.CurrentVehicle.kickPlayer(0);
                    UnturnedChat.Say(caller, Plugin.Instance.Translate("driverkick_self"), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                    return;
                }
                else if(player.CurrentVehicle.lockedOwner == player.CSteamID && player.HasPermission("myvehicle.driverkick"))
                {
                    if (otherplayer.IsInVehicle)
                    {
                        if (otherplayer.CurrentVehicle.isDriver)
                        {
                            otherplayer.CurrentVehicle.kickPlayer(0);
                            UnturnedChat.Say(caller, Plugin.Instance.Translate("driverkick", otherplayer.DisplayName), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                            UnturnedChat.Say(otherplayer, Plugin.Instance.Translate("kicked", caller.DisplayName), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                        }
                        else
                        {
                            UnturnedChat.Say(caller, Plugin.Instance.Translate("not_driving", otherplayer.DisplayName), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                        }
                    }
                    else
                    {
                        UnturnedChat.Say(caller, Plugin.Instance.Translate("not_in_vehicle", otherplayer.DisplayName), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                    }
                    return;
                }
                if(caller.HasPermission("driverkick.admin") || caller.IsAdmin)
                {
                    if (otherplayer.IsInVehicle)
                    {
                        if (otherplayer.CurrentVehicle.isDriver)
                        {
                            otherplayer.CurrentVehicle.kickPlayer(0);
                            UnturnedChat.Say(caller, Plugin.Instance.Translate("driverkick", otherplayer.DisplayName), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                            UnturnedChat.Say(otherplayer, Plugin.Instance.Translate("kicked", caller.DisplayName), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                        }
                        else
                        {
                            UnturnedChat.Say(caller, Plugin.Instance.Translate("not_driving", otherplayer.DisplayName), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                        }
                    }
                    else
                    {
                        UnturnedChat.Say(caller, Plugin.Instance.Translate("not_in_vehicle", otherplayer.DisplayName), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                    }
                    return;
                }
            }
            if(command.Length == 0)
            {
                UnturnedPlayer player = (UnturnedPlayer)caller;
                if (player.IsInVehicle)
                {
                    if (player.CurrentVehicle.isDriver)
                    {
                        player.CurrentVehicle.kickPlayer(0);
                        UnturnedChat.Say(caller, Plugin.Instance.Translate("driverkick_self"), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                        return;
                    }
                    else
                    {
                        UnturnedChat.Say(caller, Plugin.Instance.Translate("not_driving_self"), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                    }
                }
                else
                {
                    UnturnedChat.Say(caller, Plugin.Instance.Translate("not_in_vehicle_self"), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
                    return;
                }
            }
            else
            {
                UnturnedChat.Say(caller, Plugin.Instance.Translate("syntax", Syntax), UnturnedChat.GetColorFromName(Plugin.Instance.Configuration.Instance.MessageColor, Color.red));
            }
        }
    }
}
