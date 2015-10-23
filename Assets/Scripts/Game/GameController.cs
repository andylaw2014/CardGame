using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public bool IsFirstPlayer { get; private set; }
    public PlayerController Player { get; private set; }
    public PlayerController Opponent { get; private set; }
    public GameState Phase { get; private set; }
    public GameGuiController GuiController { get; private set; }
    public ResourcePanelController ResourcePanel;
    public CombatHandler Combat;
    
    void Awake()
    {
        // UNDONE: Random instead
        IsFirstPlayer = PhotonNetwork.isMasterClient;
        GuiController = GetComponent<GameGuiController>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Opponent = GameObject.FindGameObjectWithTag("Opponent").GetComponent<PlayerController>();

        if (Instance == null)
            Instance = this;
        else
            DestroyImmediate(this);
    }

    void Start()
    {
        // UNDONE: Draw from deck
        Phase = new ResetState(true);
        DrawCard("TestCard");
        DrawCard("TestCard");
        DrawCard("TestCard");
        // Start GameController
        if (IsFirstPlayer) // Remove condition will make SetState called twice (Player 1 and 2 and their RPC)
            SetPhase(GameState.Type.Player1Reset);
    }

    public void DrawCard(string cardName)
    {
        var obj = Instantiate(Resources.Load(cardName)) as GameObject;
        obj.AddComponent<Draggable>();
        var id = Player.DrawCard(obj);
        GetComponent<PhotonView>().RPC("RpcDrawCard", PhotonTargets.Others, cardName, id);
    }

    [PunRPC]
    private void RpcDrawCard(string cardName, int id)
    {
        Opponent.DrawCard(Instantiate(Resources.Load(cardName)) as GameObject, id);
    }

    public void AddResource(EnumType.Resource resource, bool reset)
    {
        Player.AddResource(resource);
        if (reset)
            Player.ResetResource();
        GetComponent<PhotonView>().RPC("RpcAddResource", PhotonTargets.Others, (byte)resource, reset);
    }


    [PunRPC]
    public void RpcAddResource(byte data, bool reset)
    {
        var resource = (EnumType.Resource)data;
        Opponent.AddResource(resource);
        if (reset)
            Opponent.ResetResource();
    }

    public bool IsCardPlayable(int id)
    {
        return Phase.AllowPlayCard() && Player.IsPlayable(id);
    }

    public void PlayCard(int id)
    {
        Player.Play(id);
        GetComponent<PhotonView>().RPC("RpcPlayCard", PhotonTargets.Others, id);
    }

    [PunRPC]
    private void RpcPlayCard(int id)
    {
        Opponent.Play(id);
    }

    public bool IsCardAttackable(int id)
    {
        //UNDONE: Check other card
        return true && Player.IsAttackable(id);
    }

    public bool IsCardDefencable(int id)
    {
        //UNDONE: Check other card
        return true && Player.IsDefencable(id);
    }

    #region Phase
    public void NextPhase()
    {
        SetPhase(Phase.NextState());
    }

    public void SetPhase(GameState.Type gameState)
    {
        RpcSetPhase((byte) gameState);
        GetComponent<PhotonView>().RPC("RpcSetPhase", PhotonTargets.Others, (byte)gameState);
    }

    [PunRPC]
    private void RpcSetPhase(byte data)
    {
        Phase.EndStateCall();
        var gameState = (GameState.Type)data;
        switch (gameState)
        {
            case GameState.Type.Player1Reset:
                Phase = new ResetState(true); break;
            case GameState.Type.Player1Draw:
                Phase = new DrawState(true); break;
            case GameState.Type.Player1Main:
                Phase = new MainState(true); break;
            case GameState.Type.Player1Atk:
                Phase = new AttackState(true); break;
            case GameState.Type.Player1Def:
                Phase = new DefenceState(true); break;
            case GameState.Type.Player1AtkCot:
                Phase = new AttackCounterState(true); break;
            case GameState.Type.Player1DefCot:
                Phase = new DefenceCounterState(true); break;
            case GameState.Type.Player1SecMain:
                Phase = new SecondMainState(true); break;
            case GameState.Type.Player2Reset:
                Phase = new ResetState(false); break;
            case GameState.Type.Player2Draw:
                Phase = new DrawState(false); break;
            case GameState.Type.Player2Main:
                Phase = new MainState(false); break;
            case GameState.Type.Player2Atk:
                Phase = new AttackState(false); break;
            case GameState.Type.Player2Def:
                Phase = new DefenceState(false); break;
            case GameState.Type.Player2AtkCot:
                Phase = new AttackCounterState(false); break;
            case GameState.Type.Player2DefCot:
                Phase = new DefenceCounterState(false); break;
            case GameState.Type.Player2SecMain:
                Phase = new SecondMainState(false); break;
        }
        
        GuiController.NextPhaseButton.interactable = Phase.NextPhaseClickable();
        Phase.StateCall();
    }
    #endregion

    #region Select
    public void InitialCombatHandler()
    {
        Combat=new CombatHandler();
    }

    [PunRPC]
    public void AddAttackor(int id)
    {
        Debug.Log("AddAttackor"+id);
        Combat.AddAttackor(id);
    }

    public void RpcAddAttackor(int id)
    {
        GetComponent<PhotonView>().RPC("AddAttackor", PhotonTargets.Others, id);
    }
    
    #endregion
}
