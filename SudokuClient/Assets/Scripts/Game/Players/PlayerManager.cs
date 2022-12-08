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
        //Create Player Obj
        Player player = Instantiate(GameManager.Instance.PlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.gameObject.name = $"Player {id} (Waiting for server)";
        //Add to list
        m_Players.Add(id, player);
        IDLogger.Log($"Player Added: {id}");
        IDLogger.Log($"Count: {m_Players.Count}");
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

    #endregion
}