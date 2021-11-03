using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class menu : MonoBehaviourPunCallbacks
{
    [Header("Screen")]
    public GameObject joinScreen;
    public GameObject lobbyScreen;

    [Header("Join Screen")]
    public Button createRoomButton;
    public Button joinRoomButton;

    [Header("Lobby Screen")]
    public TextMeshProUGUI playerListText;
    public Button startGameButton;

	private void Start()
	{
        // Have Buttons Deactivated untill we are connected to the server
        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
	}

	public override void OnConnectedToMaster()
	{
        // Activate Buttons when connected to server
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
	}

    void setScreen (GameObject screen)
	{
        // Deactivate all panels
        joinScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        //enable the selected screen
        screen.SetActive(true);
	}

    public void onCreateRoomPressed (TMP_InputField roomNameInput)
	{
        // Create Room When Button is Pressed
        network.instance.createRoom(roomNameInput.text);
	}

    public void onJoinButtonPressed (TMP_InputField roomNameInput)
	{
        network.instance.joinRoom(roomNameInput.text);
	}
	public override void OnPlayerLeftRoom(Player otherPlayer)
	{
        UpdateLobbyUI();
	}
	public void onPlayerNameUpdate(TMP_InputField playerNameInput)
	{
        PhotonNetwork.NickName = playerNameInput.text;
	}

	public override void OnJoinedRoom()
	{
        setScreen(lobbyScreen);

        // When someone joins update lobby so that players can see each other
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
	}

    [PunRPC]
    public void UpdateLobbyUI()
	{
        playerListText.text = "";

        // Display all the players currently in the lobby
        foreach (Player player in PhotonNetwork.PlayerList)
		{
            playerListText.text += player.NickName + "\n";
		}

		// Only Enable start game button if they are the host
		if (PhotonNetwork.IsMasterClient)
		{
            startGameButton.interactable = true;

		}
		else
		{
            startGameButton.interactable = false;
		}
	}

    public void onLeaveLobbyPressed()
	{
        PhotonNetwork.LeaveRoom();
        setScreen(joinScreen);
	}

    public void onStartGamePressed()
	{
        network.instance.photonView.RPC("changeScene", RpcTarget.All, "level");
	}

}
