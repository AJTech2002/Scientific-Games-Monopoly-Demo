using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    [Header("General Information")]
    public string name;

    [Header("Board Information")]
    public int currentBoardPosition;
    public int lastBoardPosition;

    [HideInInspector]
    public List<int> boardHistory = new List<int>();
    [HideInInspector]
    public List<int> diceRolls = new List<int>();

    [Header("Assets")]
    public float cash;
    public List<Property> properties = new List<Property>();

    public GameObject linkedAvatar;

    public PlayerData (string name, float initialCash, int startingPosition = 1)
    {
        this.name = name;
        this.cash = initialCash;
        this.currentBoardPosition = startingPosition;
    }

    public void Move (int newPosition, int diceRoll)
    {
        lastBoardPosition = this.currentBoardPosition;
        this.currentBoardPosition = newPosition;
        boardHistory.Add(lastBoardPosition);
        diceRolls.Add(diceRoll);
    }

}
