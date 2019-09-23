using UnityEngine;
using UnityEngine.UI;
using Photon;

public class CreateRoom : PunBehaviour
{
    [SerializeField]
    private Text _roomName;
    private Text RoomName { get { return _roomName; } }

    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 2, PlayerTtl = 20000 };

        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
        {
            print("create room successfully sent");
        }
        else
        {
            print("create room failed to send");
        }
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        print("create room failed: " + codeAndMsg[1]);
    }

    public override void OnCreatedRoom()
    {
        print("room created successfully");
    }
}
