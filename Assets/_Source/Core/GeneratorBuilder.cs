using Assets._Source.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBuilder
{
    int _width;
    int _height;
    
    

    float[,] _data;


    public int Width { get => _width; }
    public int Height { get => _height; }
    public GeneratorBuilder(int width, int height)
    {
        _height = height;
        _width = width;
        _data = new float[width, height];
        
    }

    public GeneratorBuilder() : this(128, 128) { }

    public float this[int i, int j]
    {
        get
        {
            i = Math.Clamp(i, 0, _width - 1);
            j = Math.Clamp(j, 0, _height - 1);
            return _data[i, j];
        }

        set
        {
            i = Math.Clamp(i, 0, _width - 1);
            j = Math.Clamp(j, 0, _height - 1);
            _data[i, j] = value;
        }
    }


    public void AddPerlinNoise(int seed1, int seed2, float coef1 = 1.0F, float coef2 = 1.0F, Func<float, float> fun = null)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var perlin = PerlinClamped(seed1 + i * coef1 / _width, seed2 + j * coef2 / _height);
                if (fun != null)
                {
                    _data[i, j] += fun(perlin);
                }
                else
                {
                    _data[i, j] += perlin;
                    var list = new List<float>();
                }
            }
        }
    }
    public void MultiplyPerlinNoise(int seed1, int seed2, float coef1 = 1.0F, float coef2 = 1.0F, Func<float, float> fun = null)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var perlin = PerlinClamped(seed1 + i * coef1 / _width, seed2 + j * coef2 / _height);
                if (fun != null)
                {
                    _data[i, j] *= fun(perlin);
                }
                else
                {
                    _data[i, j] *= perlin;
                    var list = new List<float>();
                }
            }
        }
    }

    public void MultiplyByCoordFunc(Func<int, int, float> fun)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _data[i, j] *= fun(i, j);
            }
        }
    }
    public void AddByCoordFunc(Func<int, int, float> fun)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _data[i, j] += fun(i, j);
            }
        }
    }

    public void ApplyFunc(Func<float, float> fun)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _data[i, j] = fun(_data[i, j]);
            }
        }
    }



    private float PerlinClamped(float v1, float v2) => Mathf.Clamp(Mathf.PerlinNoise(v1, v2), 0, 1);

}
