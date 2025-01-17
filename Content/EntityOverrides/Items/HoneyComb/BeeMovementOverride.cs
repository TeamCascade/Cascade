namespace Cascade.Content.EntityOverrides.Items.HoneyComb
{
    public class BeeMovementOverride : ItemOverride
    {
        public override int TypeToOverride => ItemID.SweetheartNecklace;
        public override int[] AdditionalOverrideTypes => new int[]
        {
            ItemID.BeeCloak,
            ItemID.HoneyBalloon
        };

        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
	    {
            player.Cascade_BeeFlightTimeBoost().BeeFlightBoost = 2; 
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Utilities.EditTooltipByNum(0, item, tooltips, (t) => t.Text += "\nIncreases wing flight time by 12%");
        }
    }
}