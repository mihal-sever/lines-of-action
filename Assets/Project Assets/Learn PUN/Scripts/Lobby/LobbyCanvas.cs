using UnityEngine;

public class LobbyCanvas : MonoBehaviour
{
    [SerializeField]
    private RoomLayoutGroup _roomLayoutGroup;
    private RoomLayoutGroup RoomLayoutGroup { get { return _roomLayoutGroup; } }

    public void OnClick_JoinRoom(string roomName)
    {
        if (!PhotonNetwork.JoinRoom(roomName))
            Debug.Log("Join rom failed");
    }
}
