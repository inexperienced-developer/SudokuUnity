using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    public byte Level { get; private set; }

    public void Init(ushort id, string username, byte level)
    {
        Id = id;
        Username = username;
        Level = level;
    }

    #region Messages


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
