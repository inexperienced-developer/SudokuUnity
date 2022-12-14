using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    public bool IsLocal { get; private set; }
    public byte Level { get; private set; }
    public Lobby Lobby { get; private set; }

    public void Init(ushort id, string username, byte level)
    {
        Id = id;
        Username = username;
        Level = level;
        IsLocal = NetworkManager.Instance.Client.Id == id;
        if (IsLocal)
            DontDestroyOnLoad(gameObject);
    }

    public void Init(ushort id)
    {
        Id = id;
    }

    #region Messages

    public void JoinLobby(Lobby lobby)
    {
        Lobby = lobby;
    }

    public void QuitLobby()
    {
        Lobby = null;
    }

    /////////////////////////////////////////
    ///////     Message Sending     /////////
    /////////////////////////////////////////

    public void SendAccountInfo(string email)
    {
        Message msg = Message.Create(MessageSendMode.Reliable, ClientToServerId.AccountInformation);
        msg.AddString(email);
        NetworkManager.Instance.Client.Send(msg);
    }

    public void RequestToJoinRandomLobby()
    {
        Message msg = Message.Create(MessageSendMode.Reliable, ClientToServerId.RequestToJoinRandomLobby);
        NetworkManager.Instance.Client.Send(msg);
    }

    #endregion
}
