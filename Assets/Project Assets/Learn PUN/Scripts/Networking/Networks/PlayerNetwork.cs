using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }

    private PhotonView photonView;
    private int playersInGame = 0;

    private void Awake()
    {
        Instance = this;
        photonView = GetComponent<PhotonView>();

        PlayerName = "Name#" + Random.Range(1000, 9999);

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {
        photonView.RPC(nameof(RPC_LoadedGameScene), PhotonTargets.MasterClient);
        photonView.RPC(nameof(RPC_LoadGameOthers), PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        photonView.RPC(nameof(RPC_LoadedGameScene), PhotonTargets.MasterClient);
    }
    
    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(1);
    }

    [PunRPC]
    private void RPC_LoadedGameScene()
    {
        playersInGame++;

        if (playersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players in the game");
        }
    }
}
