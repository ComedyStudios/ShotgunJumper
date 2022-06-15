using System;
using System.Collections.Generic;
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
            //InvokeRepeating(nameof(SetTiles), 0f, 1f);
            GenerateDungeon();
        }

        private void SetTiles()
        {
            var map = Random.Range(0, tilemaps.Count-1);
            mainTilemap.ClearAllTiles();
            for (int i = 0; i < rooms; i++)
            {
                var location = new Vector3Int(0,0);
                tilemaps[map].CompressBounds();
                var tilesArray = tilemaps[map].GetTilesBlock(tilemaps[map].cellBounds);
                mainTilemap.SetTilesBlock(new BoundsInt(location, tilemaps[map].size), tilesArray);
            }
        }

        private void GenerateDungeon()
        {
            int[,] dungeon = new int[4, 4];

            var startTile = new Vector2(Random.Range(1, dungeon.GetLength(0)), Random.Range(1, dungeon.GetLength(0)));
            /*for (int x = 0;x<dungeon.GetLength(0);x++)
            {
                for (int y = 0; y<dungeon.GetLength(1);y++ )
                {
                    dungeon[x, y] = Random.Range(0, 2);
                }
            }*/
            PrintArray(dungeon);
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