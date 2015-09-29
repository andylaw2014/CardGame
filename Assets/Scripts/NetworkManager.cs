using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                GameObject room = Instantiate(RoomListItem) as GameObject;
                room.GetComponentInChildren<Text>().text = game.name+" "+game.playerCount+"/2";
                room.transform.SetParent(ScrollViewContent.transform);
                room.GetComponent<Button>().onClick.AddListener(() => { PhotonNetwork.JoinRoom(game.name); });
            }
        }
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(RoomNameInputField.text)&& RoomNameInputField.text.Length<=20)
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
        if(!PhotonNetwork.isMasterClient)
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
}
