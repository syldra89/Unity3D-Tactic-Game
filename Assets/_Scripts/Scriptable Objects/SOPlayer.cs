using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "Create Player/Player")]
public class SOPlayer : ScriptableObject
{
    //Clases disponibles
    public enum PlayerJob
    {
        Soldier,
        WhiteMage,
        BlackMage,
        Thief,
        Archer
    }

    //Variables del player
    public int characterID;
    public string playerName;
    public PlayerJob playerJobs;
    
    public int playerLevel;
    public float playerHP;
    public float playerMP;
    public float playerAttack;
    public float playerDefense;
    public float playerMagic;
    public float playerSpirit;
    public float playerSpeed;
    public float playerMovement;
}
