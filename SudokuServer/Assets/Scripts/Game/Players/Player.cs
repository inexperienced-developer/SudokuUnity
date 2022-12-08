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

    private void OnDestroy()
    {
        PlayerManager.RemovePlayerFromList(this);
    }

    #region Messages


    /////////////////////////////////////////
    ///////     Message Sending     /////////
    /////////////////////////////////////////

    public void SendAccountData(ushort toId)
    {
        Message msg = Message.Create(MessageSendMode.Reliable, ServerToClientId.AccountData);
        msg.AddUShort(Id);
        msg.AddString(Username);
        msg.AddByte(Level);
        NetworkManager.Instance.Server.Send(msg, toId);
    }

    #endregion
}
