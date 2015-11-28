namespace Assets.Scripts.Core.Zone
{
    public class BattlefieldZone : OrderedZone
    {
        public BattlefieldZone(Game game, Player owner) : base(game, owner)
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