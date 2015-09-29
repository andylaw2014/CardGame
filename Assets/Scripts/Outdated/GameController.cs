using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public enum GameState { PlayerAddMana, PlayerMain }
    public enum Mana { Mana1, Mana2, Mana3 }
    public Player player;
    public Player opponent;
    public GameObject AddManaPanel;
    public GameObject TestCard;
    [HideInInspector]
    public GameState state;
    // Use this for initialization
    void Start()
    {
        state = GameState.PlayerAddMana;
        GameObject card = Instantiate(TestCard) as GameObject;
        if (card != null)
            player.AddHand(card.GetComponent<Card>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextStage()
    {

    }
    public void PlayerAddMana(int type)
    {
        if (type == 1)
            PlayerAddMana(Mana.Mana1);
        else if (type == 2)
            PlayerAddMana(Mana.Mana2);

        else if (type == 3)
            PlayerAddMana(Mana.Mana3);
    }
    public void PlayerAddMana(Mana mana)
    {
        switch (mana)
        {
            case Mana.Mana1:
                player.MaxMana1++;
                break;
            case Mana.Mana2:
                player.MaxMana2++;
                break;
            case Mana.Mana3:
                player.MaxMana3++;
                break;
        }
        AddManaPanel.SetActive(false);
        player.RestoreMana();
        state = GameState.PlayerMain;
    }
}
