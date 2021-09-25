using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    public void DrawDisplayMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        Texture2D textureMap = new Texture2D(width, height);
        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.white, Color.black, noiseMap[x, y]);
            }
        }
        textureMap.SetPixels(colorMap);
        textureMap.Apply();
        textureRender.sharedMaterial.mainTexture = textureMap;
        textureRender.transform.localScale = new Vector3(width, 1, height);
    }
}
