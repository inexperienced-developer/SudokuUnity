using InexperiencedDeveloper.Core;
using InexperiencedDeveloper.Utils.Log;
using Riptide;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private static Dictionary<ushort, Player> m_Players = new Dictionary<ushort, Player>();

    public static Player GetPlayerById(ushort id) => m_Players.TryGetValue(id, out Player player) ? player : null;
    public static Player GetLocalPlayer() => m_Players.TryGetValue(NetworkManager.Instance.Client.Id, out Player player) ? player : null;

    public void CreatePlayer(ushort id, string email)
    {
        Player player = SpawnPlayer(id);
        player.SendAccountInfo(email);
    }

    private static void ApplyAccountData(ushort id, string username, byte level)
    {
        IDLogger.Log($"Count: {m_Players.Count}");
        Player player = GetLocalPlayer();
        if (player == null)
        {
            IDLogger.LogError("Player is null");
            return;
        } 
        player.Init(id, username, level);
        player.gameObject.name = $"{username}({id})";
    }

    private static void JoinLobby(ushort lobbyId, ushort[] playerIds)
    {
        List<Player> playerList = new List<Player>();
        playerList.Add(GetLocalPlayer());
        foreach (ushort id in playerIds)
        {
            Player newPlayer = GetPlayerById(id);
            if (newPlayer != null && newPlayer != GetLocalPlayer())
            {
                IDLogger.Log($"Player {id} already exists.");
                continue;
            }
            newPlayer = SpawnPlayer(id);
            playerList.Add(newPlayer);
        }
        if (playerList.Count > LobbyManager.MAX_PLAYERS)
        {
            IDLogger.LogError($"More than {LobbyManager.MAX_PLAYERS} cannot join a lobby at a time.");
            return;
        }
        if (!LobbyManager.GetLobbyById(lobbyId, out Lobby lobby))
        {
            Lobby newLobby = LobbyManager.Instance.CreateNewLobby(lobbyId);
            foreach(var player in playerList)
            {
                newLobby.JoinLobby(player);
            }
        }
        else
        {
            foreach(var player in playerList)
            {
                if (lobby.PlayersInLobby.Contains(player.Id))
                    continue;
                lobby.JoinLobby(player);
            }
        }
    }

    private static Player SpawnPlayer(ushort id)
    {
        if (GetPlayerById(id) != null) return GetPlayerById(id);
        Player newPlayer = Instantiate(GameManager.Instance.PlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        newPlayer.gameObject.name = $"Player {newPlayer.Id}";
        newPlayer.Init(id);
        m_Players.Add(newPlayer.Id, newPlayer);
        return newPlayer;
    }


    #region Messages


    /////////////////////////////////////////
    ///////     Message Handling    /////////
    /////////////////////////////////////////

    [MessageHandler((ushort)ServerToClientId.AccountData)]
    private static void ReceiveAccountData(Message msg)
    {
        ushort id = msg.GetUShort();
        string username = msg.GetString();
        byte level = msg.GetByte();
        ApplyAccountData(id, username, level);
    }

    [MessageHandler((ushort)ServerToClientId.JoinedLobby)]
    private static void ReceiveJoinLobby(Message msg)
    {
        ushort lobbyId = msg.GetUShort();
        byte len = msg.GetByte();
        ushort[] playerIds = msg.GetUShorts(len);
        JoinLobby(lobbyId, playerIds);
    }

    #endregion
}