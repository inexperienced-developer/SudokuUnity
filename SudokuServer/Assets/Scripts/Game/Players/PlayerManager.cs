using InexperiencedDeveloper.Core;
using InexperiencedDeveloper.Utils.Log;
using Riptide;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private static Dictionary<ushort, Player> m_Players = new Dictionary<ushort, Player>();

    public static Player GetPlayerById(ushort id) => m_Players.TryGetValue(id, out Player player) ? player : null;
    public static bool RemovePlayerFromList(Player player)
    {
        if(m_Players.TryGetValue(player.Id, out Player p))
        {
            m_Players.Remove(player.Id);
            return true;
        }
        return false;
    } 

    private static void CreatePlayer(ushort id, string email)
    {
        //Create Player Obj
        Player player = Instantiate(GameManager.Instance.PlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        //Get player data from DB - Username, Stats, Profile Info
        string username = email;   //For now this is placeholder
        player.gameObject.name = $"{username}({id})";
        byte level = 1; // placeholder will receive from database
        player.Init(id, username, level);
        //Add to list
        m_Players.Add(id, player);
        player.SendAccountData(player.Id);  //Send the data back -- for now it's just a username (Future would be all achievements, level, see who's online, etc.)
    }

    #region Messages


    /////////////////////////////////////////
    ///////     Message Handling    /////////
    /////////////////////////////////////////

    [MessageHandler((ushort)ClientToServerId.AccountInformation)]
    private static void ReceiveAccountInformation(ushort fromId, Message msg)
    {
        string email = msg.GetString();
        CreatePlayer(fromId, email);
    }

    #endregion


}