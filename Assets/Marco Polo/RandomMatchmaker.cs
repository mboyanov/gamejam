using UnityEngine;

public class RandomMatchmaker : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom("main");

	}

	void OnJoinedRoom()
	{
		GameObject monster;
		// game logic: if this is the only player, we're "it"
		if (PhotonNetwork.playerList.Length == 1) {
					 GameLogic.player1 = PhotonNetwork.player.ID;
					 monster = PhotonNetwork.Instantiate ("monsterprefab", Vector3.zero, Quaternion.identity, 0);
			Camera.current.cullingMask ^= 1<<10;
				} else {
					GameLogic.player2 = PhotonNetwork.player.ID;
					monster = PhotonNetwork.Instantiate("monsterprefab2", Vector3.zero, Quaternion.identity, 0);
			Camera.current.cullingMask ^= 1<<9;
				}


		monster.GetComponent<myThirdPersonController>().isControllable = true;
	}




}