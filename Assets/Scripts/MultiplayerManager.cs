using UnityEngine;
using Mirror; // Assuming Mirror is installed
using System.Collections.Generic;

public class MultiplayerManager : NetworkManager
{
    public List<string> lobbyPlayers = new List<string>();
    public int maxPlayers = 4;

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Server started");
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log("Client started");
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);
        // Custom player setup
        PlayerController player = conn.identity.GetComponent<PlayerController>();
        if (player != null)
        {
            player.playerName = "Player " + numPlayers;
        }
    }

    public void StartHost()
    {
        StartHost();
    }

    public void StartClient(string address)
    {
        networkAddress = address;
        StartClient();
    }

    public void StopGame()
    {
        StopHost();
        StopClient();
    }

    // Lobby functions
    public void JoinLobby(string playerName)
    {
        if (lobbyPlayers.Count < maxPlayers)
        {
            lobbyPlayers.Add(playerName);
            Debug.Log(playerName + " joined the lobby");
        }
    }

    public void LeaveLobby(string playerName)
    {
        lobbyPlayers.Remove(playerName);
        Debug.Log(playerName + " left the lobby");
    }

    public void StartGame()
    {
        if (lobbyPlayers.Count >= 2)
        {
            // Start the game
            Debug.Log("Starting multiplayer game");
            // Load GameWorld scene
        }
    }

    // Add co-op features, like shared quests or PvP
    [Server]
    public void SyncQuest(Quest quest)
    {
        // Sync quest across clients
        RpcUpdateQuest(quest);
    }

    [ClientRpc]
    void RpcUpdateQuest(Quest quest)
    {
        // Update local quest
        QuestManager.Instance.AddQuest(quest);
    }
}
