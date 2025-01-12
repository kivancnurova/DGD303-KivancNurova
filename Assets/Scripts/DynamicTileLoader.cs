using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DynamicTileLoader : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] tiles;
    public int viewWidth;
    public int viewHeight;

    private Vector3Int lastPlayerTilePosition;


    void Start()
    {
        lastPlayerTilePosition = Vector3Int.zero;
        LoadVisibleTiles();
    }

    void Update()
    {
        Vector3Int currentPlayerTilePosition = GetPlayerTilePosition();

        if(currentPlayerTilePosition != lastPlayerTilePosition)
        {
            lastPlayerTilePosition = currentPlayerTilePosition;
            LoadVisibleTiles();
        }
    }


    Vector3Int GetPlayerTilePosition()
    {
        Vector3 playerPosition = Camera.main.transform.position;
        return tilemap.WorldToCell(transform.position);
    }

    void LoadVisibleTiles()
    {
        Vector3Int playerTilePos = GetPlayerTilePosition();

        for(int x = -viewWidth / 2; x < viewWidth / 2; x++)
        {
            for(int y = -viewHeight / 2; y < viewHeight / 2; y++)
            {
                Vector3Int tilePos = new Vector3Int(playerTilePos.x + x, playerTilePos.y + y, 0);
                if(!tilemap.HasTile(tilePos))
                {
                    tilemap.SetTile(tilePos, tiles[Random.Range(0, tiles.Length)]);
                }
            }
        }

        CleanOldTiles(playerTilePos);
    }

    void CleanOldTiles(Vector3Int playerTilePos)
    {
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            float distance = Vector3Int.Distance(pos, playerTilePos);
            if(distance > viewWidth / 2 || distance > viewHeight / 2)
            {
                tilemap.SetTile(pos, null);
            }
        }
    }
}
