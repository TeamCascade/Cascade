﻿using Cascade.Core.BaseEntities.ModNPCs;

namespace Cascade.Content.NPCs.CosmostoneShowers.Planetoids
{
    public class GalileoPlanetoid : BasePlanetoid, ILocalizedModType
    {
        public new string LocalizationCategory => "NPCs.Misc";

        public override float MaximumAttractionRadius => 184f;

        public override float WalkableRadius => 128f;

        public override void SafeSetDefaults()
        {
            NPC.width = 128;
            NPC.height = 128;
            NPC.lifeMax = 2;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.dontTakeDamage = true;
        }

        public override void SafeAI()
        {
            float totalAttractionRadius = MaximumAttractionRadius + WalkableRadius;
            Vector2 dustPosition = NPC.Center + Main.rand.NextVector2CircularEdge(totalAttractionRadius, totalAttractionRadius);
            if (Main.rand.NextBool(2))
                CascadeUtilities.CreateDustLoop(15, dustPosition, Vector2.UnitX, DustID.Electric);

            NPC.rotation += Tau / 600f;
            NPC.ShowNameOnHover = false;
        }
    }
}
