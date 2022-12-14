using InexperiencedDeveloper.Utils.Log;
using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ushort Id { get; private set; }
    public string Username { get; private set; }
    public byte Level { get; private set; }
    public Lobby Lobby { get; private set; }

    public void Init(ushort id, string username, byte level)
    {
        Id = id;
        Username = username;
        Level = level;
    }

    private void OnDestroy()
    {
        PlayerManager.RemovePlayerFromList(this);
        if(Lobby != null)
        {
            Lobby.QuitLobby(this);
        }
    }

    public void JoinLobby(Lobby lobby)
    {
        Lobby = lobby;
    }

    public void QuitLobby()
    {
        Lobby = null;
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

    public void SendJoinLobby(ushort toId)
    {
        if(Lobby == null)
        {
            IDLogger.LogError($"Lobby for player {Id} is null.");
            return;
        }
        ushort lobbyId = Lobby.LobbyId;
        ushort[] playerIds = Lobby.PlayersInLobby.ToArray();
        Message msg = Message.Create(MessageSendMode.Reliable, ServerToClientId.JoinedLobby);
        msg.AddUShort(lobbyId);
        msg.AddByte((byte)playerIds.Length);
        msg.AddUShorts(playerIds, false);

        NetworkManager.Instance.Server.Send(msg, toId);
    }

    #endregion
}
