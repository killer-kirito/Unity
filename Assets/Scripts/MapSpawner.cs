using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class MapSpawner : MonoBehaviour
{

    public GameObject CubePrefab;
    public float Size = 0f; /*Set in UI*/
    float Seed = 15f; /*Set in UI*/
    float random;
    private float Partition = 0.4f /*Map part of perlin */;
    // Use this for initialization

    public float PerlinNoise(float x, float y)
    {
        //Generate a value from the given position, position is divided to make the noise more frequent.
        float noise = Mathf.PerlinNoise(x / Seed, y / Seed);

        //Return the noise value
        return noise;

    }
    void Start()
    {
        random = UnityEngine.Random.Range(1, 500);
        // Seed = UnityEngine.Random.Range(1, 100);
        //Debug.Log("" + Seed);
        int TileTypeMax = (int)Enum.GetValues(typeof(TileType)).Cast<TileType>().Last();

        for (float ix = 0; ix < Size; ix++)
        {
            for (float iz = 0; iz < Size; iz++)
            {


                TileType mappedTile = (TileType)Mathf.RoundToInt(Mathf.RoundToInt(ValueMap.Map(PerlinNoise(random + ix, random + iz), 0, 1, 1, TileTypeMax * 10)) / 10);

                GameObject Cube = (GameObject)Instantiate(CubePrefab, new Vector3(Size * -1 / 2 + ix, 0, Size * -1 / 2 + iz), Quaternion.identity);
                Cube.name = String.Format("maptile_{0}_{1}", ix, iz);
                Cube.transform.SetParent(gameObject.transform);
                Cube.GetComponent<TileInfo>().tileType = mappedTile;

                Color32 tileColor;
                if (!TileToColor.TryGetValue(mappedTile, out tileColor))
                {
                    tileColor = new Color32(0, 255, 100, 255);
                    Cube.GetComponent<TileInfo>().tileType = TileType.Grassland;
                }
                Cube.GetComponent<Renderer>().material.color = tileColor;
            }

        }

    }






    internal static Dictionary<TileType, Color32> TileToColor = new Dictionary<TileType, Color32> {
        { TileType.Water, new Color32(0, 100, 255, 200) },
        { TileType.Ocean, new Color32(0, 0, 255, 200) },
        { TileType.Oil, new Color32(50, 50, 50, 255) },
        { TileType.Grassland, new Color32(0, 255, 0, 255) },
        { TileType.Ore, new Color32(150, 150, 150, 255) },
        { TileType.Forest, new Color32(10, 150, 10, 255) },
        { TileType.Diamond, new Color32(0, 255, 255, 200) },
        { TileType.Desert, new Color32(236, 199, 147, 255) }
    };

}

public enum TileType
{
    Oil = 1,
    Ore,
    Desert,
    Ocean,
    Grassland,
    Forest,
    Water,
    Diamond
}
