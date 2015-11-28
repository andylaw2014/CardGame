public class PlayCard : AfterDrag
{
    public override void Execute()
    {
        var game = GameController2.Instance;
        var cardController = GetComponent<CardController>();
        if (game.IsCardPlayable(cardController.Id))
            game.PlayCard(cardController.Id);
    }
}