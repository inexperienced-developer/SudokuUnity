using InexperiencedDeveloper.Utils.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : MonoBehaviour
{
    public ushort LobbyId { get; private set; }
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
    public bool Full => m_Players.Count >= LobbyManager.MAX_PLAYERS;

    public void Init(ushort id)
    {
        LobbyId = id;
        gameObject.name = $"Lobby {LobbyId}";
    }

    public void JoinLobby(Player player)
    {
        if (Full)
        {
            IDLogger.LogWarning($"Lobby {LobbyId} is full.");
            return;
        }
        m_Players.Add(player);
        player.JoinLobby(this);
        player.transform.SetParent(transform);
    }

    public void QuitLobby(Player player)
    {
        m_Players.Remove(player);
        if (m_Players.Count <= 0)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LobbyManager.RemoveLobbyFromList(this);
    }
}
