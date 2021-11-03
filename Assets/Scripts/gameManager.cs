using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
public class gameManager : MonoBehaviour
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

	}
}
