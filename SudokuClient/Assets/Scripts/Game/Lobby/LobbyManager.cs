using InexperiencedDeveloper.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{
    [SerializeField] private GameObject m_LobbyPrefab;
    public GameObject LobbyPrefab => m_LobbyPrefab;
    public static Dictionary<ushort, Lobby> m_CurrentLobbies = new();
    public const byte MAX_PLAYERS = 5;

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

    public Lobby CreateNewLobby(ushort lobbyId)
    {
        Lobby newLobby = Instantiate(LobbyManager.Instance.LobbyPrefab, Vector3.zero, Quaternion.identity).GetComponent<Lobby>();
        newLobby.Init(lobbyId);
        return newLobby;
    }

    private static ushort GetNextAvailableLobbyId()
    {
        ushort id = (ushort)(m_CurrentLobbies.Keys.ToArray()[m_CurrentLobbies.Count - 1] + 1);
        return id;
    }
}