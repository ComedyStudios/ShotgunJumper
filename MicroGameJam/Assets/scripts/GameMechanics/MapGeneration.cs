using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


namespace GameMechanics
{
    public class MapGeneration : MonoBehaviour
    {
        public Tilemap mainTilemap ;
        public List<Tilemap> tilemaps;
        public List<Tilemap> conectors;
        public int rooms;

        private void Start()
        {
            CreateMap();
        }

        private void CreateMap()
        {
            mainTilemap.ClearAllTiles();
            var dungeon = GenerateDungeon();
            Vector3Int start;
            for (int i = 0; i < dungeon.GetLength(0); i++)
            {
                for (int j = 0; j < dungeon.GetLength(1); j++)
                {
                    if (dungeon[i, j] != 0)
                    {
                        SetTiles(new Vector3Int(j * 30, -i * 30));
                        if (dungeon[i, j] == 1)
                        {
                            start = new Vector3Int(j * 30, -i * 30);
                            var position = mainTilemap.GetComponentInParent<Grid>().GetCellCenterWorld(start);
                            AttackScript.Instance.transform.position = position + new Vector3(-3, 3);
                        }
                    }
                }
            }
        }

        private void SetTiles(Vector3Int location)
        {
            var map = Random.Range(0, tilemaps.Count-1);
            for (int i = 0; i < rooms; i++)
            {
                tilemaps[map].CompressBounds();
                var tilesArray = tilemaps[map].GetTilesBlock(tilemaps[map].cellBounds);
                mainTilemap.SetTilesBlock(new BoundsInt(location, tilemaps[map].size), tilesArray);
            }
        }

        private int[,] GenerateDungeon()
        {
            int[,] dungeon = new int[8, 8];
            List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up,Vector2Int.down,Vector2Int.left, Vector2Int.right};
            var newPosition = new Vector2Int(Random.Range(1, dungeon.GetLength(1)), Random.Range(1, dungeon.GetLength(0)));
            dungeon[newPosition.y, newPosition.x] = 1;
            var possibleDirections = new List<Vector2Int>(directions);
            Vector2Int direction = Vector2Int.zero;
            try
            {
                for (int i = 1; i < rooms; i++)
                {
                
                    foreach (var dir in possibleDirections.ToList())
                    {
                        
                        if (newPosition.x + dir.x< 0 || newPosition.x + dir.x ==dungeon.GetLength(1) ||newPosition.y + dir.y < 0 || newPosition.y + dir.y == dungeon.GetLength(0))
                        {
                            possibleDirections.Remove(dir);
                            continue;
                        }
                    
                        if (dungeon[newPosition.y + dir.y, newPosition.x + dir.x] != 0)
                        {
                            possibleDirections.Remove(dir);
                        }
                    }
                    if (possibleDirections.Count == 0)
                    {
                        break;
                    }
                    direction = possibleDirections[Random.Range(0, possibleDirections.Count)];
                    newPosition += direction;
                    dungeon[newPosition.y, newPosition.x] = i + 1;
                    possibleDirections = new List<Vector2Int>(directions);
                    possibleDirections.Remove(-1*direction);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"{newPosition} {direction} {e}");
                var text = "";
                foreach (var dir in possibleDirections)
                {
                    text += $" {dir}";
                }

                Debug.Log(text);
            }
            PrintArray(dungeon);
            return dungeon;

        }

        private void PrintArray(int[,] floorMapArray)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0; i< floorMapArray.GetLength(0); i++)
            {
                for(int j=0; j<floorMapArray.GetLength(1); j++)
                {
                    sb.Append(floorMapArray [i,j]);
                    sb.Append(' ');				   
                }
                sb.AppendLine();
            }
            Debug.Log(sb.ToString());
        }
    }
}