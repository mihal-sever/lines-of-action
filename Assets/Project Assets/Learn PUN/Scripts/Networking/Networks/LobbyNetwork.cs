using Photon;

public class LobbyNetwork : PunBehaviour
{
    private void Start()
    {
        print("connecting to server...");
        PhotonNetwork.ConnectUsingSettings("0.0.0");        
    }

    public override void OnConnectedToMaster()
    {
        print("connected to Master");
        PhotonNetwork.automaticallySyncScene = false;
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        print("joined lobby");

        if (!PhotonNetwork.inRoom)
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
    }
}
