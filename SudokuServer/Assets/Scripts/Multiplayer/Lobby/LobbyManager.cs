using InexperiencedDeveloper.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{
    public static Dictionary<ushort, Lobby> m_CurrentLobbies = new();

    public static bool GetLobbyById(ushort id, out Lobby lobby)
    {
        if (m_CurrentLobbies.TryGetValue(id, out Lobby l))
        {
            lobby = l;
            return true;
        }
        lobby = null;
        return false;
    }

    public static void RemoveLobbyFromList(Lobby lobby)
    {
        m_CurrentLobbies.Remove(lobby.LobbyId);
    }

    public static Lobby GetNextAvailableOpenLobby()
    {
        foreach(ushort id in m_CurrentLobbies.Keys.ToArray())
        {
            if (!m_CurrentLobbies[id].Full)
                return m_CurrentLobbies[id];
        }
        Lobby newLobby = new Lobby(GetNextAvailableLobbyId());
        m_CurrentLobbies.Add(newLobby.LobbyId, newLobby);
        return newLobby;
    }

    private static ushort GetNextAvailableLobbyId()
    {
        if (m_CurrentLobbies.Count <= 0) return 1;
        ushort id = (ushort)(m_CurrentLobbies.Keys.ToArray()[m_CurrentLobbies.Count - 1] + 1);
        return id;
    }
}
