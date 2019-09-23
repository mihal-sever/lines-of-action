using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class GameNetworking : PunBehaviour, IPunTurnManagerCallbacks
{
    [SerializeField]
    private Text TurnText;

    private PunTurnManager turnManager;


    void Start()
    {
        turnManager = this.gameObject.AddComponent<PunTurnManager>();
        turnManager.TurnManagerListener = this;
        turnManager.TurnDuration = 600f;
    }
    
    void Update()
    {
        if (!PhotonNetwork.inRoom)
            return;

        if (PhotonNetwork.room.PlayerCount > 1)
        {
            if (turnManager.IsOver)
            {
                return;
            }

            /*
			// check if we ran out of time, in which case we loose
			if (turnEnd<0f && !IsShowingResults)
			{
					Debug.Log("Calling OnTurnCompleted with turnEnd ="+turnEnd);
					OnTurnCompleted(-1);
					return;
			}
		*/

            if (this.TurnText != null)
            {
                this.TurnText.text = this.turnManager.Turn.ToString();
            }
        }
    }

    #region TurnManager Callbacks

    /// <summary>Called when a turn begins (Master Client set a new Turn number).</summary>
    public void OnTurnBegins(int turn)
    {
    }
    
    public void OnTurnCompleted(int obj)
    {
    }
    
    // when a player moved (but did not finish the turn)
    public void OnPlayerMove(PhotonPlayer photonPlayer, int turn, object move)
    {
    }
    
    // when a player made the last/final move in a turn
    public void OnPlayerFinished(PhotonPlayer photonPlayer, int turn, object move)
    {
    }
    
    public void OnTurnTimeEnds(int obj)
    {
    }

    private void UpdateScores()
    {
    }

    #endregion
}
