using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace GameMechanics
{
    public class MapGeneration : MonoBehaviour
    {
        public Tilemap tiles ;
        public RuleTile tile ;
        public Vector3Int location ;
        public InputAction MouseDownAction;
        public Tilemap tilemap;
        public BoundsInt area;

        private void OnEnable()
        {
            MouseDownAction.Enable();
        }

        private void OnDisable()
        {
            MouseDownAction.Disable();
        }

        private void Start()
        {
            MouseDownAction.performed += _ => SetTiles();
        }

        private void SetTiles()
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            location = tiles.WorldToCell(mp);
            tiles.SetTile(location, tile);
            Debug.Log(location);

            var tilesArray = tilemap.GetTilesBlock(tilemap.cellBounds);
            tilemap.SetTilesBlock(area, tilesArray);
        }
    }
}