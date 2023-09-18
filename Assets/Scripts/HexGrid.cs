using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public int SpawnRadius = 5;
    public GameObject[] HexTiles;

    void Start()
    {
        for (int i = -SpawnRadius; i <= SpawnRadius; i++)
        {
            for (int j = -SpawnRadius; j <= SpawnRadius; j++)
            {
                // GameObject tile = GetRandomTile();
                // Get Horizontal Position
                float z = j * 1.73f;
                float x = i * 2.0f;
                if (Mathf.Abs(j % 2) == 1)
                    x -= 1;
                // Get Elevation
                float y = Mathf.PerlinNoise(1 + x * 0.02f, 1 + z * 0.02f) * Mathf.PerlinNoise(x * 0.08f, z * 0.08f) * 5;
                
                int index = 0;
                if (y > 0.9) index = 1;
                else if (y > 0.7) index = 2;
                else if (y > 0.5) index = 3;
                GameObject tile = HexTiles[index];

                Instantiate(tile, new Vector3(x, y, z), new Quaternion(), transform);
            }
        }
    }


    private GameObject GetRandomTile()
    {
        int randomIndex = Random.Range(0, HexTiles.Length);
        return HexTiles[randomIndex];
    }
}
