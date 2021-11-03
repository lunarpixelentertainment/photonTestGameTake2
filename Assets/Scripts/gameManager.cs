using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
public class gameManager : MonoBehaviourPunCallbacks
{
	[Header("Stats")]
	public bool isGameOver = false;
	public float timeToWin;
	public float invincibleDuration;
	private float hatPickupTime;

	[Header("Players")]
	public string playerPrefabLocation;
	public Transform[] spawnPoints;
	public playerController[] players;
	public int playerWithHat;
	private int playersInGame;


	//intstance
	public static gameManager instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		players = new playerController[PhotonNetwork.PlayerList.Length];
		photonView.RPC("ImInGame", RpcTarget.AllBuffered);
	}

	[PunRPC]
	void ImInGame()
	{
		playersInGame++;

		if(playersInGame == PhotonNetwork.PlayerList.Length)
		{
			SpawnPlayer();
		}
	}

	void SpawnPlayer()
	{
		GameObject playerObj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
		playerController playerScript = playerObj.GetComponent<playerController>();
		playerScript.photonView.RPC("Initilise", RpcTarget.All, PhotonNetwork.LocalPlayer);
	}

	public playerController GetPlayer(int playerID)
	{
		return players.First(x => x.id == playerID);
	}

	public playerController GetPlayer(GameObject playerObj)
	{
		return players.First(x => x.gameObject == playerObj);
		return players.First(x => x.gameObject == playerObj);
	}
}
