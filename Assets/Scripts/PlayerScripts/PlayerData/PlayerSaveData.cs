using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
    public string PlayerName { get; private set; }
    public int PlayerCoins { get; private set; }
    public int PlayerLevels { get; private set; }

    //Could be changed to int later on
    public float PlayerXP { get; private set; }

    public PlayerSaveData(PlayerData player)
    {
        PlayerName = player.PlayerName;
        PlayerCoins = player.PlayerCoins;
        PlayerLevels = player.PlayerLevels;
        PlayerXP = player.PlayerXP;
    }
}