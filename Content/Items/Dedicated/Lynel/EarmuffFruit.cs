﻿using Cascade.Content.Buffs.Pets;

namespace Cascade.Content.Items.Dedicated.Lynel
{
    public class EarmuffFruit : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.DefaultToVanitypet(ModContent.ProjectileType<EarPiercingBellbird>(), ModContent.BuffType<BellbirdBuff>());
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(Item.buffType, 2);
            return base.UseItem(player);
        }
    }
}
