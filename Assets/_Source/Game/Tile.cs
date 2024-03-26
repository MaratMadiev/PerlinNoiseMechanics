using Assets._Source.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    TileEnum tile;
    public TileEnum TileEnum
    {
        get
        {
            return tile;
        }
        set
        {
            tile = value;
        }
    }
}
