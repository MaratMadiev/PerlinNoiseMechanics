using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Source.Core
{
    public class BiomeGenerator
    {
        float _stoneMinHeight;
        float _iceMinHeight;
        float _waterHeight;

        GeneratorBuilder _generatorBuilder;

        TileEnum[,] _biomes;


        public TileEnum this[int i, int j]
        {
            get
            {
                return _biomes[i, j];
            }

            set
            {
                _biomes[i, j] = value;
            }
        }

        public BiomeGenerator(GeneratorBuilder generatorBuilder, float stoneMinHeight, float iceMinHeight, float waterHeight)
        {
            _generatorBuilder = generatorBuilder;
            _biomes = new TileEnum[_generatorBuilder.Width, _generatorBuilder.Width];

            this._stoneMinHeight = stoneMinHeight;
            this._iceMinHeight = iceMinHeight;
            this._waterHeight = waterHeight;
        }

        public void GenerateBiomes()
        {
            CalculateWater();
            CalculateBeaches();
            CalculateStonesAndPeaks();
        }

        private void CalculateStonesAndPeaks()
        {
            for (int i = 0; i < _generatorBuilder.Height; i++)
            {
                for (int j = 0; j < _generatorBuilder.Width; j++)
                {
                    if (_biomes[i, j] != TileEnum.WATER && _generatorBuilder[i, j] >= _iceMinHeight)
                    {
                        _biomes[i, j] = TileEnum.ICE;
                        continue;
                    }
                    if (_biomes[i, j] != TileEnum.WATER && _generatorBuilder[i, j] >= _stoneMinHeight)
                    {
                        _biomes[i, j] = TileEnum.STONE;
                    }
                }
            }
        }

        private void CalculateBeaches()
        {
            for (int i = 0; i < _generatorBuilder.Height; i++)
            {
                for (int j = 0; j < _generatorBuilder.Width; j++)
                {
                    if (_biomes[i, j] != TileEnum.WATER && HasWaterNerby(i, j))
                    {
                        _biomes[i, j] = TileEnum.SAND;
                    }
                }
            }
        }

        private bool HasWaterNerby(int i, int j)
        {
            for (int ii = i - 1; ii <= i + 1; ii++)
            {
                for (int jj = j - 1; jj <= j + 1; jj++)
                {
                    if (0 <= ii && ii < _generatorBuilder.Height && 0 <= jj && jj < _generatorBuilder.Width)
                    {
                        if (_biomes[ii, jj] == TileEnum.WATER)
                        {
                            return true;
                        }
                    } else return true;
                }
            }
            return false;
        }

        private void CalculateWater()
        {
            for (int i = 0; i < _generatorBuilder.Height; i++)
            {
                for (int j = 0; j < _generatorBuilder.Width; j++)
                {
                    if (_generatorBuilder[i, j] <= _waterHeight)
                    {
                        _biomes[i, j] = TileEnum.WATER;
                    }
                }
            }
        }
    }
}
