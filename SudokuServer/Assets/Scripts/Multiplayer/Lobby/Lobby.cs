using InexperiencedDeveloper.Utils.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby
{
    public ushort LobbyId { get; private set; }
    private byte m_MaxPlayers = 5;
    private List<Player> m_Players = new List<Player>();
    public List<ushort> PlayersInLobby
    {
        get
        {
            List<ushort> playerIds = new List<ushort>();
            foreach (var player in m_Players)
            {
                playerIds.Add(player.Id);
            }
            return playerIds;
        }
    }
    public bool Full => m_Players.Count >= m_MaxPlayers;

    //public Lobby(List<Player> players)
    //{
    //    m_Players = players;
    //    LobbyId = LobbyManager.GetNextAvailableLobbyId();
    //}

    //public Lobby(Player player)
    //{
    //    if(m_Players.Count <= 0)
    //    {
    //        m_Players = new List<Player> { player };
    //        LobbyId = LobbyManager.GetNextAvailableLobbyId();
    //    }
    //    else
    //    {
    //        IDLogger.LogError($"Lobby {LobbyId} already exists. Try to join it.");
    //    }
    //}

    public Lobby(ushort id)
    {
        LobbyId = id;
    }

    public void JoinLobby(Player player)
    {
        if(Full)
        {
            IDLogger.LogWarning($"Lobby {LobbyId} is full.");
            return;
        }
        m_Players.Add(player);
        player.JoinLobby(this);
        foreach(var p in m_Players)
        {
            p.SendJoinLobby(p.Id);
        }
    }

    public void QuitLobby(Player player)
    {
        m_Players.Remove(player);
        if (m_Players.Count <= 0)
            LobbyManager.RemoveLobbyFromList(this);
    }
}
