using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapDisplay _mapDisplay;
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public bool isAutoUpdate;
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale);
        _mapDisplay.DrawDisplayMap(noiseMap);
    }
}
