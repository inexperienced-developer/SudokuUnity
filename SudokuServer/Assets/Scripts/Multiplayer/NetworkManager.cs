using InexperiencedDeveloper.Core;
using Riptide;
using Riptide.Utils;
using System;
using UnityEngine;

public enum ServerToClientId : ushort
{
    SyncTicks = 1,
    AccountData, //Contains Currencies, Username, Profile Info
}

public enum ClientToServerId : ushort
{
    AccountInformation = 1, //Contains email/username -> Waits for AccountData
    RequestToJoinLobby, //Contains friend's username
}

public class NetworkManager : Singleton<NetworkManager>
{
    public Server Server { get; private set; }

    [SerializeField] private ushort port = 7777;
    [SerializeField] private ushort maxClientCount = 10;

    void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientDisconnected += OnClientDisconnect;
    }

    private void OnClientDisconnect(object sender, ServerDisconnectedEventArgs e)
    {
        Destroy(PlayerManager.GetPlayerById(e.Client.Id).gameObject);
    }

    private void FixedUpdate()
    {
        Server.Update();
    }

    private void OnApplicationQuit()
    {
        Server.Stop();
    }
}