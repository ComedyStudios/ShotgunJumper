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
        public RuleTile tile;
        public int rooms;
        public GameObject doorPrefab;
        public GameObject winTrigger;
        public GameObject hud;
        public GameObject winScreen;
        public GameObject loadingScreen;

        public static MapGeneration instace;

        private bool _scaned;
        private Vector3[,] RoomCenter;
        private int[,] _dungeon;

        private void Awake()
        {
            instace = this;
            _scaned = false;
            CreateMap();
        }

        private void Update()
        {
            if (!_scaned && Time.timeSinceLevelLoad > .1)
            {
                AstarPath.active.Scan();
                _scaned = true;
                loadingScreen.SetActive(false);
                hud.SetActive(true);
            }
        }

        private void CreateMap()
        {
            mainTilemap.ClearAllTiles();
            _dungeon = GenerateDungeon();
            RoomCenter = new Vector3[_dungeon.GetLength(0), _dungeon.GetLength(1)];

            for (int i = 0; i < _dungeon.GetLength(0); i++)
            {
                for (int j = 0; j < _dungeon.GetLength(1); j++)
                {
                    if (_dungeon[i, j] != 0)
                    {
                        SetTiles(j, i);
                        if (_dungeon[i, j] == 1)
                        {
                            var start = new Vector3Int(j * 30, -i * 30);
                            var position = mainTilemap.GetComponentInParent<Grid>().GetCellCenterWorld(start);
                            var playerTransform = AttackScript.Instance.transform;
                            playerTransform.position = RoomCenter[i,j];
                            Camera.main!.transform.position = playerTransform.position + Vector3.back;
                        }

                        if (_dungeon[i,j] == 12)
                        {
                            Instantiate(winTrigger,RoomCenter[i,j], Quaternion.identity);
                        }
                    }
                }
            }
            ConnectRooms();
        }

        private void ConnectRooms()
        {
            for (int t = 1; t< rooms; t++)
            {
                var room1 = FindRoom(t);
                var room2 = FindRoom(t+1);
                
                if (room1 != null && room2 != null)
                {
                    var direction = RoomCenter[room2.Value.y, room2.Value.x] - RoomCenter[room1.Value.y, room1.Value.x];
                    for (int j = -2; j<2; j++)
                    {
                        for (int i = 0; i< Math.Abs(direction.x); i++)
                        {
                            mainTilemap.SetTile(Vector3Int.FloorToInt(RoomCenter[room1.Value.y, room1.Value.x])+ new Vector3Int(i * (int)(direction.x/Math.Abs(direction.x)),j ), tile);
                        }
                    }

                    

                    for (int j = -1; j<2; j++)
                    {
                        for (int i = 0; i<Math.Abs(direction.y);i++)
                        {
                            mainTilemap.SetTile(Vector3Int.FloorToInt(RoomCenter[room2.Value.y, room2.Value.x] - new Vector3(j,i* (int)(direction.y/Math.Abs(direction.y)))),tile);
                        }
                    }
                    
                    if (Math.Abs(direction.y) > Math.Abs(direction.x))
                    {
                        Instantiate(doorPrefab, RoomCenter[room2.Value.y, room2.Value.x] - new Vector3(-0.5f,14.5f* (int)(direction.y/Math.Abs(direction.y))), Quaternion.Euler(0,0,90));
                    }
                    else 
                    if (direction != Vector3.zero)
                    {
                        Instantiate(doorPrefab, RoomCenter[room1.Value.y, room1.Value.x]+ new Vector3(14.5f * (int)(direction.x/Math.Abs(direction.x)),-0.5f ), Quaternion.identity);
                    }
                }
            }
        }

        private void SetTiles( int x, int y )
        {
            var map = Random.Range(0, tilemaps.Count);
            var location = new Vector3Int(x * 30, y * -30);
            for (int i = 0; i < rooms; i++)
            {
                
                tilemaps[map].CompressBounds();
                var tilesArray = tilemaps[map].GetTilesBlock(tilemaps[map].cellBounds);
                mainTilemap.SetTilesBlock(new BoundsInt(location, tilemaps[map].size), tilesArray);
                RoomCenter[y, x] = location + tilemaps[map].size/2 ;
            }
            var tileObject = tilemaps[map].gameObject.transform;
            foreach (Transform child in tileObject)
            {   
                Instantiate(child.gameObject,location + child.localPosition - tilemaps[map].origin, Quaternion.identity);
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
            var roomsCreated = 0;
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
                roomsCreated = i + 1;
                possibleDirections = new List<Vector2Int>(directions);
                possibleDirections.Remove(-1*direction);
            }
            PrintArray(dungeon);
            rooms = roomsCreated;
            return dungeon;
        }

        private Vector2Int? FindRoom(int room)
        {
            for(int i=0; i< _dungeon.GetLength(0); i++)
            {
                for(int j=0; j<_dungeon.GetLength(1); j++)
                {
                    if (_dungeon[i,j] == room)
                    {
                        return new Vector2Int(j, i );
                    }   		   
                }
            }
            return null;
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