namespace Assets.Scripts.Outdate.Core.Zone
{
    public class HandZone : OrderedZone
    {
        public HandZone(Game game, Player owner) : base(game, owner)
        {
        }

        protected override void AfterAdd(Card card)
        {
        }

        protected override void AfterRemove(Card card)
        {
        }
    }
}