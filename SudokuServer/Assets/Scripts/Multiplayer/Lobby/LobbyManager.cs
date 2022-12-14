using InexperiencedDeveloper.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LobbyManager : Singleton<LobbyManager>
{
    private static Dictionary<ushort, Lobby> m_CurrentLobbies = new();

    public static ushort GetNextAvailableLobbyId()
    {
        ushort id = (ushort)(m_CurrentLobbies.Keys.ToArray()[m_CurrentLobbies.Count - 1] + 1);
        return id;
    }
}
