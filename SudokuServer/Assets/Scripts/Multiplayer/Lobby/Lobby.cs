using InexperiencedDeveloper.Utils.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby
{
    public ushort LobbyId { get; private set; }
    private byte m_MaxPlayers = 5;
    private List<Player> m_Players = new List<Player>();

    public bool Full => m_Players.Count >= m_MaxPlayers;

    public Lobby(List<Player> players)
    {
        m_Players = players;
        LobbyId = LobbyManager.GetNextAvailableLobbyId();
    }

    public Lobby(Player player)
    {
        if(m_Players.Count <= 0)
        {
            m_Players = new List<Player> { player };
            LobbyId = LobbyManager.GetNextAvailableLobbyId();
        }
        else
        {
            IDLogger.LogError($"Lobby {LobbyId} already exists. Try to join it.");
        }
    }

    public void JoinLobby(Player player)
    {
        if(Full)
        {
            IDLogger.LogWarning($"Lobby {LobbyId} is full.");
            return;
        }
        m_Players.Add(player);
    }

    public void QuitLobby(Player player)
    {
        m_Players.Remove(player);
    }
}
