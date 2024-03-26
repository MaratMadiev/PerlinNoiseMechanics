using Assets._Source.Core;
using Codice.Client.BaseCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private GameObject grassTilePrefab;
    [SerializeField]
    private GameObject sandTilePrefab;
    [SerializeField]
    private GameObject iceTilePrefab;
    [SerializeField]
    private GameObject stoneTilePrefab;

    const float sizeOfTile = 3;



    internal void Create(GeneratorBuilder builder, BiomeGenerator biomes)
    {
        SetOffset(builder);
        Fill(builder, biomes);
    }

    private void SetOffset(GeneratorBuilder builder)
    {
        gameObject.transform.position = new Vector3(-builder.Width * sizeOfTile / 2, 0, -builder.Height * sizeOfTile / 2);
    }

    void Fill(GeneratorBuilder builder, BiomeGenerator biomes)
    {
        for (int i = 0; i < builder.Width; i++)
        {
            for (int j = 0; j < builder.Height; j++)
            {
                
                if (biomes[i, j] == TileEnum.WATER) continue;
                InstantiateTile(biomes[i, j], builder[i, j], i * sizeOfTile, j * sizeOfTile);
            }
        }
    }

    private void InstantiateTile(TileEnum tileEnum, float height, float x, float z)
    {
        switch (tileEnum)
        {
            case TileEnum.GRASS:
                {
                    Instantiate(grassTilePrefab, transform.position + new Vector3(x, height, z), Quaternion.identity, transform);
                    break;
                }
            case TileEnum.ICE:
                {
                    Instantiate(iceTilePrefab, transform.position + new Vector3(x, height, z), Quaternion.identity, transform);
                    break;
                }
            case TileEnum.STONE:
                {
                    Instantiate(stoneTilePrefab, transform.position + new Vector3(x, height, z), Quaternion.identity, transform);
                    break;
                }
            case TileEnum.SAND:
                {
                    Instantiate(sandTilePrefab, transform.position + new Vector3(x, height, z), Quaternion.identity, transform);
                    break;
                }
        }
    }
}
