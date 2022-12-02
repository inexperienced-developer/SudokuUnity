using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompletedBoard
{
    public List<GameTile> completedBoard;

    public CompletedBoard()
    {
        completedBoard = new List<GameTile>();
    }
}
