using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Section
{
    I,
    II,
    III,
    IV,
    V,
    VI,
    VII,
    VIII,
    IX,
    None
}

[System.Serializable]
public class GameTile
{
    public int row;
    public int column;
    public Section section;
    public int value = 0;
    public bool sorted = false;
    public SpriteRenderer sprite;
}
