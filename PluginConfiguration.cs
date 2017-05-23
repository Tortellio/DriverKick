using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rocket.API;
using System.Xml.Serialization;
using UnityEngine;
using Steamworks;

namespace DriverKick
{
    public class PluginConfiguration : IRocketPluginConfiguration
    {
        public string MessageColor;
        public void LoadDefaults()
        {
            MessageColor = "red";
        }
    }
}
