using UnityEngine;
using Mirror; // Assuming Mirror is installed

public class MultiplayerManager : NetworkManager
{
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
    }

    // Add co-op features, like shared quests or PvP
}