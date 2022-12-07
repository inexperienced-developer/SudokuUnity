using InexperiencedDeveloper.Core;
using Riptide;
using Riptide.Utils;
using System;
using UnityEngine;

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

    private void OnClientConnected(object sender, EventArgs e)
    {
        Debug.Log(Client.Id);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Client.Connect($"{ip}:{port}");
        }
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
