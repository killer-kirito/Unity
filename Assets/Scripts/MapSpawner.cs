using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class MapSpawner : MonoBehaviour
{

    public GameObject CubePrefab;
    public float Size = 0f; /*Set in UI*/
    public float Seed = 0f; /*Set in UI*/

    // Use this for initialization
    void Start()
    {
        int TileTypeMax = (int)Enum.GetValues(typeof(TileType)).Cast<TileType>().Last();

        for (float ix = 0; ix < Size; ix++)
        {
            for (float iz = 0; iz < Size; iz++)
            {

                /*Bin genauso weit wie beim Anfang xD*/
                /*Morgen schauen wir mal weiter*/

                float mapX = ValueMap.Map((Seed/10) + ix / Size, Seed/10, Seed/10+1);
                float mapY = ValueMap.Map((Seed / 10) + iz / Size, Seed / 10, Seed / 10 + 1);
                float rawPerlin = Mathf.PerlinNoise(mapX, mapY);
                TileType mappedTile = (TileType)Mathf.RoundToInt(ValueMap.Map(rawPerlin, 0, 1, 1, TileTypeMax));

                GameObject Cube = (GameObject)Instantiate(CubePrefab, new Vector3(ix, 0, iz), Quaternion.identity);
                Cube.name = String.Format("maptile_{0}_{1}", ix, iz);
                Cube.transform.SetParent(gameObject.transform);
                Cube.GetComponent<TileInfo>().tileType = mappedTile;

                Color32 tileColor;
                if (!TileToColor.TryGetValue(mappedTile, out tileColor))
                    tileColor = new Color32(255, 0, 0, 255);

                Cube.GetComponent<Renderer>().material.color = tileColor;
            }

        }

    }


    internal static Dictionary<TileType, Color32> TileToColor = new Dictionary<TileType, Color32> {
        { TileType.Water, new Color32(0, 100, 255, 200) },
        { TileType.Ocean, new Color32(0, 0, 255, 200) },
        { TileType.Swamp, new Color32(10, 150, 10, 255) },
        { TileType.Grassland, new Color32(0, 255, 100, 255) },
        { TileType.Ore, new Color32(150, 150, 150, 255) },
        { TileType.Forest, new Color32(0, 255, 0, 255) },
        { TileType.Oil, new Color32(50, 50, 50, 200) },
        { TileType.Desert, new Color32(236, 199, 147, 255) }
    };

}

public enum TileType
{
    Grassland = 1,
    Desert,
    Ocean,
    Water,
    Swamp,
    Oil,
    Ore,
    Forest
}
