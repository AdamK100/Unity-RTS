using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour
{
    public static void DigMulti(Vector3[] positions, Terrain t, float size, float diminution)
    {
        foreach (Vector3 center in positions)
        {
            float[,] heights = t.terrainData.GetHeights(Mathf.RoundToInt(center.x - (size / 2)), Mathf.RoundToInt(center.z - (size / 2)), Mathf.RoundToInt(size / t.terrainData.size.x), Mathf.RoundToInt(size / t.terrainData.size.x));
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    heights[i, j] -= diminution;
                }
            }
            t.terrainData.SetHeights(Mathf.RoundToInt(center.x - (size / 2)), Mathf.RoundToInt(center.z - (size / 2)), heights);
        }

    }
    public static void Dig(Vector3 center, Terrain t, float size, float diminution, float terrainHeight, float rotation)
    {
        if (rotation == 0 || rotation == 180 || rotation == 90 || rotation == -90)
        {
            if (rotation == 0 || rotation == 180)
            {
                float[,] heights = new float[Mathf.RoundToInt(size), Mathf.RoundToInt(size / 3)];
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < Mathf.RoundToInt(size / 3); j++)
                    {
                        heights[i, j] -= 0.1f * heights[i, j];
                    }
                }
                t.terrainData.SetHeights(Mathf.RoundToInt(center.x), Mathf.RoundToInt(center.z - (size / 2)), heights);
            }
            if (rotation == 90 || rotation == -90)
            {
                float[,] heights = new float[Mathf.RoundToInt(size / 3), Mathf.RoundToInt(size)];
                for (int i = 0; i < Mathf.RoundToInt(size / 3); i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        heights[i, j] -= 0.1f * heights[i, j];
                    }
                }
                t.terrainData.SetHeights(Mathf.RoundToInt(center.x - (size / 2)), Mathf.RoundToInt(center.z), heights);
            }
        }
        else
        {
            float[,] heights = new float[Mathf.RoundToInt(size), Mathf.RoundToInt(size)];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    heights[i, j] -= 0.1f * heights[i, j];
                }
            }
            t.terrainData.SetHeights(Mathf.RoundToInt(center.x - (size / 3)), Mathf.RoundToInt(center.z - (size / 3)), heights);
        }
    }
    public static void reverseDig(Vector3 center, Terrain t, float size, float diminution)
    {
        float[,] heights = t.terrainData.GetHeights(Mathf.RoundToInt(center.x), Mathf.RoundToInt(center.z), Mathf.RoundToInt(size / t.terrainData.size.x), Mathf.RoundToInt(size / t.terrainData.size.x));
        for (int i = 0; i < Mathf.RoundToInt(size / t.terrainData.size.x); i++)
        {
            for (int j = 0; j < Mathf.RoundToInt(size / t.terrainData.size.x); j++)
            {
                heights[i, j] += diminution;
            }
        }
        t.terrainData.SetHeights((int)(center.x), (int)Mathf.RoundToInt(center.z), heights);
    }
}
/*float[,] heights = new float[Mathf.RoundToInt(size), Mathf.RoundToInt(size)];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    heights[i, j] -= 0.1f * heights[i, j];
                }
            }
            t.terrainData.SetHeights(Mathf.RoundToInt(center.x - (size / 3)), Mathf.RoundToInt(center.z - (size / 3)), heights);

    */