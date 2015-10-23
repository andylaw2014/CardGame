public class PlayCard : AfterDrag
{
    public override void Execute()
    {
        var game = GameController.Instance;
        var cardController = GetComponent<CardController>();
        if (game.IsCardPlayable(cardController.Id))
            game.PlayCard(cardController.Id);
    }
}
