using Assets.Scripts.Outdate.Menu;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MonoBehaviour = Photon.MonoBehaviour;

namespace Assets.Scripts
{
    public class NetworkManager : MonoBehaviour
    {
        public Button CancelButton;
        public Button CreateButton;
        public Button NewButton;
        public GameObject RoomListItem;
        public Text RoomNameInputField;
        public GameObject ScrollViewContent;

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings("0.1");
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnJoinedLobby()
        {
            if (NewButton != null)
                NewButton.interactable = true;
            if (CreateButton != null)
                CreateButton.interactable = true;
        }

        private void OnReceivedRoomListUpdate()
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
                    roomListItem.GetComponent<Button>()
                        .onClick.AddListener(() => { PhotonNetwork.JoinRoom(room.name); });
                    roomListItem.transform.localScale = Vector3.one;
                }
            }
        }

        private void OnGUI()
        {
            GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        }

        public void CreateRoom()
        {
            var roomName = RoomNameInputField.text;
            if (string.IsNullOrEmpty(roomName) || IsRoomExists(roomName) || roomName.Length > 20) return;

            if (PhotonNetwork.CreateRoom(RoomNameInputField.text, new RoomOptions {maxPlayers = 2, isVisible = true},
                null))
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

        private void OnEnterEdit()
        {
            DestroyImmediate(gameObject);
            SceneManager.LoadScene("Edit");
        }

        private void OnLeftRoom()
        {
            if (CancelButton != null)
            {
                CancelButton.interactable = false;
                CancelButton.GetComponent<ChangeMenu>().Change();
            }
            else
            {
                DestroyImmediate(gameObject);
                SceneManager.LoadScene("Menu");
            }
        }

        private void OnJoinedRoom()
        {
            if (!PhotonNetwork.isMasterClient)
                SceneManager.LoadScene("Main");
        }

        private void OnPhotonPlayerConnected()
        {
            SceneManager.LoadScene("Main");
        }

        private void OnPhotonPlayerDisconnected()
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
}