using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class network : MonoBehaviourPunCallbacks
{
	// instance
	public static network instance;


	private void Awake()
	{
		if (instance != null && instance != this)
		{
			gameObject.SetActive(false);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}



	private void Start()
	{
		PhotonNetwork.ConnectUsingSettings();
	}

	public void createRoom (string roomName)
	{
		PhotonNetwork.CreateRoom(roomName);
	}

	public void joinRoom (string roomName)
	{
		PhotonNetwork.JoinRoom(roomName);
	}

	[PunRPC]
	public void changeScene (string sceneName)
	{
		PhotonNetwork.LoadLevel(sceneName);
	}
	
	


}
