using UnityEngine;
using System.Collections;

public class GameLogic : Photon.MonoBehaviour {

	public static int playerWhoIsIt;
	public static int player1=-1;
	public static int player2=-1;

	private static PhotonView ScenePhotonView;
	
	void Start()
	{
		ScenePhotonView = this.GetComponent<PhotonView>();
	}
	
	void OnPhotonPlayerConnected(PhotonPlayer player)
	{

		Debug.Log("OnPhotonPlayerConnected: " + player);
		Debug.Log ("player 1" + player1);
		Debug.Log ("player 2" + player2);

		// when new players join, we send "who's it" to let them know
		// only one player will do this: the "master"
		
		if (PhotonNetwork.isMasterClient)
		{
			TagPlayer(playerWhoIsIt);
		}
	}
	
	public static void TagPlayer(int playerID)
	{
		Debug.Log("TagPlayer: " + playerID);
		ScenePhotonView.RPC("TaggedPlayer", PhotonTargets.All, playerID);
	}

	void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerDisconnected: " + player);
		
		if (PhotonNetwork.isMasterClient)
		{
			if (player.ID == playerWhoIsIt)
			{
				// if the player who left was "it", the "master" is the new "it"
				TagPlayer(PhotonNetwork.player.ID);
			}
		}
	}

	void OnJoinedRoom()
	{
		// game logic: if this is the only player, we're "it"
		if (PhotonNetwork.playerList.Length == 1)
		{
			playerWhoIsIt = PhotonNetwork.player.ID;
		}
		
		Debug.Log("onjoinedroom playerWhoIsIt: " + playerWhoIsIt);
	}


	[RPC]
	void TaggedPlayer(int playerID)
	{
		playerWhoIsIt = playerID;
		Debug.Log("RPC TaggedPlayer: " + playerID);
	}
}
