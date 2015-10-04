using UnityEngine;
using UnityEngine.UI;

// GameController Controller
public class GameController : MonoBehaviour
{
    // Different GameController phase
    public enum State
    {
        GameStart = 0,
        Player1_1Reset, Player1_2Draw, Player1_3Main, Player1_4Atk, Player1_5Def, Player1_6AtkCot, Player1_7DefCot, Player1_8Main,
        Player2_1Reset, Player2_2Draw, Player2_3Main, Player2_4Atk, Player2_5Def, Player2_6AtkCot, Player2_7DefCot, Player2_8Main
    }

    public Text StateText; // GUI text to show current phase.
    public Button NextPhaseButton;  // Button to end current phase
    public AddManaPanelController AddManaPanel;
    public Sprite CardBack;  // Card back
    [HideInInspector]
    public bool IsMaster; // True: Player 1; False: Player 2
    [HideInInspector]
    public State GameState; // Current phase of the GameController

    private bool _allowPlayCard; // Is Player allow to play any card
    private PlayerController _player;
    private PlayerController _opponent;

    void Start()
    {
        SetState(State.GameStart);
        _allowPlayCard = false;
        // UNDONE: Random instead
        IsMaster = PhotonNetwork.isMasterClient;

        // Set up player controller
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _opponent = GameObject.FindGameObjectWithTag("Opponent").GetComponent<PlayerController>();

        // UNDONE: Draw from deck
        DrawCard("TestCard");
        DrawCard("TestCard");
        DrawCard("TestCard");

        // Start GameController
        if (IsMaster) // Remove condition will make SetState called twice (Player 1 and 2 and their RPC)
            SetState(State.Player1_1Reset);
    }

    void OnGUI()
    {
        UpdateStateText();
    }

    #region Draw Card
    // Player draw a card
    public void DrawCard(string cardName)
    {
        int id = _player.DrawCard(cardName);
        GetComponent<PhotonView>().RPC("OppYouDraw", PhotonTargets.Others, cardName, id);
    }

    // Opponent see you draw a card
    [PunRPC]
    private void OppYouDraw(string cardName, int ID)
    {
        _opponent.DrawCard(cardName, ID, false);
    }
    #endregion

    #region Play Card
    public void PlayCard(int id)
    {
        _player.Play(id);
        GetComponent<PhotonView>().RPC("OppPlayCard", PhotonTargets.Others, id);
    }

    public bool IsCardPlayable(int id)
    {
        return _allowPlayCard && _player.IsPlayable(id);
    }

    // Opponent see play card
    [PunRPC]
    private void OppPlayCard(int id)
    {
        _opponent.Play(id);
    }
    #endregion

    #region Mana Methods
    public void ResetMana(bool isPlayer = true)
    {
        if (isPlayer)
            _player.ResetMana();
        else
            _opponent.ResetMana();
    }

    public bool IsMana1Full
    {
        get { return _player.IsMana1Full; }
    }


    public bool IsMana2Full
    {
        get { return _player.IsMana2Full; }
    }


    public bool IsMana3Full
    {
        get { return _player.IsMana3Full; }
    }

    public void AddMana1()
    {
        _player.AddMana1();
        GetComponent<PhotonView>().RPC("OppAddMana", PhotonTargets.Others, 1);
        ResetMana();
    }

    public void AddMana2()
    {
        _player.AddMana2();
        GetComponent<PhotonView>().RPC("OppAddMana", PhotonTargets.Others, 2);
        ResetMana();
    }

    public void AddMana3()
    {
        _player.AddMana3();
        GetComponent<PhotonView>().RPC("OppAddMana", PhotonTargets.Others, 3);
        ResetMana();
    }

    // Opponent see you add Mana
    [PunRPC]
    private void OppAddMana(int type)
    {
        switch (type)
        {
            case 1:
                _opponent.AddMana1();
                break;
            case 2:
                _opponent.AddMana2();
                break;
            default:
                _opponent.AddMana3();
                break;
        }
        ResetMana(false);
    }

    #endregion

    #region Phase Methods
    // Progress to next phase
    public void NextPhase()
    {
        State state;
        switch (GameState)
        {
            case State.Player1_1Reset:
                state = State.Player1_2Draw;
                break;
            case State.Player1_2Draw:
                state = State.Player1_3Main;
                break;
            case State.Player1_3Main:
                state = State.Player1_4Atk;
                break;
            case State.Player1_4Atk:
                state = State.Player1_5Def;
                break;
            case State.Player1_5Def:
                state = State.Player1_6AtkCot;
                break;
            case State.Player1_6AtkCot:
                state = State.Player1_7DefCot;
                break;
            case State.Player1_7DefCot:
                state = State.Player1_8Main;
                break;
            case State.Player1_8Main:
                state = State.Player2_1Reset;
                break;
            case State.Player2_1Reset:
                state = State.Player2_2Draw;
                break;
            case State.Player2_2Draw:
                state = State.Player2_3Main;
                break;
            case State.Player2_3Main:
                state = State.Player2_4Atk;
                break;
            case State.Player2_4Atk:
                state = State.Player2_5Def;
                break;
            case State.Player2_5Def:
                state = State.Player2_6AtkCot;
                break;
            case State.Player2_6AtkCot:
                state = State.Player2_7DefCot;
                break;
            case State.Player2_7DefCot:
                state = State.Player2_8Main;
                break;
            case State.Player2_8Main:
                state = State.Player1_1Reset;
                break;
            default:
                state = GameState;
                break;
        }
        SetState(state);
    }

    // Set Phase directly
    public void SetState(State state)
    {
        GetComponent<PhotonView>().RPC("RPCSetState", PhotonTargets.AllViaServer, (byte)state);
    }

    // Remote set Phase directly
    [PunRPC]
    private void RPCSetState(byte state)
    {
        GameState = (State)state;

        // Call Phase method
        // It should excute all events when a phase start
        #region CallPhase
        if (IsMaster)
            switch (GameState)
            {
                case State.Player1_1Reset:
                    ResetPhase();
                    break;
                case State.Player1_2Draw:
                case State.Player1_3Main:
                case State.Player1_4Atk:
                case State.Player2_5Def:
                case State.Player1_6AtkCot:
                case State.Player2_7DefCot:
                case State.Player1_8Main:
                    break;
                default:
                    break;
            }
        else
            switch (GameState)
            {
                case State.Player2_1Reset:
                    ResetPhase();
                    break;
                case State.Player2_2Draw:
                case State.Player2_3Main:
                case State.Player2_4Atk:
                case State.Player1_5Def:
                case State.Player2_6AtkCot:
                case State.Player1_7DefCot:
                case State.Player2_8Main:
                    break;
                default:
                    break;
            }
        #endregion

        // Enable NextPhaseButton
        #region Next Phase Button
        switch (GameState)
        {
            case State.Player1_1Reset:
            case State.Player1_2Draw:
            case State.Player1_3Main:
            case State.Player1_4Atk:
            case State.Player2_5Def:
            case State.Player1_6AtkCot:
            case State.Player2_7DefCot:
            case State.Player1_8Main:
                NextPhaseButton.interactable = IsMaster;
                break;
            case State.Player2_1Reset:
            case State.Player2_2Draw:
            case State.Player2_3Main:
            case State.Player2_4Atk:
            case State.Player1_5Def:
            case State.Player2_6AtkCot:
            case State.Player1_7DefCot:
            case State.Player2_8Main:
                NextPhaseButton.interactable = !IsMaster;
                break;
            default:
                NextPhaseButton.interactable = false;
                break;
        }
        #endregion
        // Enable NextPhaseButton

        #region Allow Play Card
        switch (GameState)
        {
            case State.Player1_3Main:
            case State.Player1_8Main:
                _allowPlayCard = IsMaster;
                break;
            case State.Player2_3Main:
            case State.Player2_8Main:
                _allowPlayCard = !IsMaster;
                break;
            default:
                _allowPlayCard = false;
                break;
        }

        #endregion
    }

    private void UpdateStateText()
    {

        var text = "";

        switch (GameState)
        {
            case State.Player1_1Reset:
            case State.Player1_2Draw:
            case State.Player1_3Main:
            case State.Player1_4Atk:
            case State.Player1_5Def:
            case State.Player1_6AtkCot:
            case State.Player1_7DefCot:
            case State.Player1_8Main:
                text = IsMaster ? "Your " : "Opponent ";
                break;
            case State.Player2_1Reset:
            case State.Player2_2Draw:
            case State.Player2_3Main:
            case State.Player2_4Atk:
            case State.Player2_5Def:
            case State.Player2_6AtkCot:
            case State.Player2_7DefCot:
            case State.Player2_8Main:
                text = IsMaster ? "Opponent " : "Your ";
                break;
            case State.GameStart:
                break;
            default:
                break;
        }

        switch (GameState)
        {
            case State.Player1_1Reset:
            case State.Player2_1Reset:
                text += "Reset Phase";
                break;
            case State.Player1_2Draw:
            case State.Player2_2Draw:
                text += "Draw Phase";
                break;
            case State.Player1_3Main:
            case State.Player2_3Main:
                text += "Main Phase";
                break;
            case State.Player1_4Atk:
            case State.Player2_4Atk:
                text += "Attack Phase";
                break;
            case State.Player1_5Def:
            case State.Player2_5Def:
                text += "Defence Phase";
                break;
            case State.Player1_6AtkCot:
            case State.Player2_6AtkCot:
                text += "Attack Counter Phase";
                break;
            case State.Player1_7DefCot:
            case State.Player2_7DefCot:
                text += "Defence Counter Phase";
                break;
            case State.Player1_8Main:
            case State.Player2_8Main:
                text += "2nd Main Phase";
                break;
            case State.GameStart:
                break;
            default:
                break;
        }
        StateText.text = text;
    }

    private void ResetPhase()
    {
        if (!_player.IsManaFull)
            AddManaPanel.Activate();
    }
    #endregion
}
