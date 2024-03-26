using Assets._Source.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    GeneratorBuilder builder;
    BiomeGenerator biomesGenerator;

    [SerializeField]
    Map map;
    [SerializeField]
    int width;
    [SerializeField]
    int length;
    [SerializeField]
    float worldMountainHeight;
    [SerializeField]
    float worldHeight;
    [SerializeField]
    float widthCoeficient;
    [SerializeField]
    int partsCount;

    [SerializeField]
    int seed1;
    [SerializeField]
    int seed2;

    [SerializeField]
    float waterHeight;
    [SerializeField]
    float stoneHeight;
    [SerializeField]
    float iceHeight;


    void Awake()
    {
        builder = new GeneratorBuilder(width, length);
        biomesGenerator = new(builder, stoneHeight, iceHeight, waterHeight);
        Generate();
        map.Create(builder, biomesGenerator);

    }

    private void Generate()
    {
        CreateMainTerrain();
        CreateMountains();
        SmoothEdges();


        DivideToParts();
        biomesGenerator.GenerateBiomes();
    }

    private void SmoothEdges()
    {
        builder.MultiplyByCoordFunc(SmoothByCoordinates);
    }

    private float SmoothByCoordinates(int x, int z)
    {
        int m = length;
        int n = width;
        int distanceToBorderX = Mathf.Min(x, m - x - 1);
        int distanceToBorderY = Mathf.Min(z, n - z - 1);
        int minDistanceToBorder = Mathf.Min(distanceToBorderX, distanceToBorderY);

        float value = minDistanceToBorder * 0.2F;
        return Mathf.Min(value + 0.2F, 1);
    }

    private void DivideToParts()
    {
        builder.ApplyFunc(DivideHeight);
    }

    private float DivideHeight(float x)
    {
        var delta = (worldHeight + worldMountainHeight) / partsCount;
        var deltasCount = x / delta;
        return Mathf.Floor(deltasCount) * delta;
    }

    private void CreateMountains()
    {
        int offset = 3;
        builder.AddPerlinNoise(seed1 + offset, seed2 + offset, 
            width / widthCoeficient * 1, length / widthCoeficient * 1, 
            x => CalcMountainHeight(x) * worldMountainHeight);
    }

    private float CalcMountainHeight(float x)
    {
        float minLimit = 0.45F;
        float maxLimit = 1;

        if (x < minLimit) return 0;
        if (x > maxLimit) return 1;

        return (x - minLimit) / (maxLimit - minLimit);
    }

    private void CreateMainTerrain()
    {
        builder.AddPerlinNoise(seed1, seed2, width / widthCoeficient, length / widthCoeficient, x => worldHeight * x);
    }
}
