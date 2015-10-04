using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : Photon.MonoBehaviour
{
    public GameObject ScrollViewContent;
    public GameObject RoomListItem;
    public Button NewButton;
    public Button CreateButton;
    public Button CancelButton;
    public Text RoomNameInputField;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnJoinedLobby()
    {
        NewButton.interactable = true;
        CreateButton.interactable = true;
    }

    void OnReceivedRoomListUpdate()
    {
        if (ScrollViewContent != null)
        {
            foreach (Transform child in ScrollViewContent.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var room in PhotonNetwork.GetRoomList())
            {
                var roomListItem = Instantiate(RoomListItem);
                roomListItem.GetComponentInChildren<Text>().text = room.name + " " + room.playerCount + "/2";
                roomListItem.transform.SetParent(ScrollViewContent.transform);
                roomListItem.GetComponent<Button>().onClick.AddListener(() => { PhotonNetwork.JoinRoom(room.name); });
                roomListItem.transform.localScale = Vector3.one;
            }
        }
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    public void CreateRoom()
    {
        var roomName = RoomNameInputField.text;
        if (string.IsNullOrEmpty(roomName) || IsRoomExists(roomName) || roomName.Length > 20) return;

        if (PhotonNetwork.CreateRoom(RoomNameInputField.text, new RoomOptions() { maxPlayers = 2, isVisible = true }, null))
        {
            CreateButton.interactable = false;
            CancelButton.interactable = true;
            CreateButton.GetComponent<ChangeMenu>().Change();
        }
        else
        {
            CreateButton.interactable = true;
        }
    }

    void OnLeftRoom()
    {
        if (CancelButton != null)
        {
            CancelButton.interactable = false;
            CancelButton.GetComponent<ChangeMenu>().Change();
        }
        else
        {
            DestroyImmediate(gameObject);
            Application.LoadLevel("Menu");
        }
    }

    void OnJoinedRoom()
    {
        if (!PhotonNetwork.isMasterClient)
            Application.LoadLevel("Main");
    }

    void OnPhotonPlayerConnected()
    {
        Application.LoadLevel("Main");
    }

    void OnPhotonPlayerDisconnected()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    private bool IsRoomExists(string name)
    { 
        foreach (var room in PhotonNetwork.GetRoomList())
        {
            if (room.name.Equals(name))
                return true;
        }
        return false;
    }
}
