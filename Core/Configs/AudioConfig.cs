﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace Cascade.Core.Configs
{
    public class AudioConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        public static AudioConfig Instance;

        [Header("Audio")]
        [BackgroundColor(192, 54, 64, 192)]
        [DefaultValue(true)]
        public bool OverrideCalamityMusic { get; set; }
    }
}
