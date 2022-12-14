using InexperiencedDeveloper.Core;
using Riptide;
using Riptide.Utils;
using System;
using UnityEngine;

public enum ServerToClientId : ushort
{
    SyncTicks = 1,
    AccountData, //Contains Currencies, Username, Profile Info
    JoinedLobby,
}

public enum ClientToServerId : ushort
{
    AccountInformation = 1, //Contains email/username -> Waits for AccountData
    RequestToJoinRandomLobby, //RandomLobby
}

public class NetworkManager : Singleton<NetworkManager>
{
    public Client Client { get; private set; }

    [SerializeField] private string ip;
    [SerializeField] private ushort port;

    void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        Client = new Client();
        Client.Connected += OnClientConnected;
    }

    private void OnDisable()
    {
        Client.Connected -= OnClientConnected;
    }

    private void OnClientConnected(object sender, EventArgs e)
    {
        //TODO: Get Email from Authentication
        string email = "a@a.com"; //TEMP EMAIL
        LevelManager.Instance.LoadSceneAsync(Constants.MAIN_MENU_SCREEN, delegate { PlayerManager.Instance.CreatePlayer(NetworkManager.Instance.Client.Id, email);});
    }

    public void Connect()
    {
        Client.Connect($"{ip}:{port}");
    }

    private void FixedUpdate()
    {
        Client.Update();
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }
}
