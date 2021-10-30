using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using MUtility;

public class TurretPlacing : MonoBehaviour
{
    [SerializeField] private Tilemap towerGrid;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 offset = towerGrid.transform.position.XY();
            print(offset);
            Vector3Int position = Vector3Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).XY() - offset) ;
            towerGrid.SetTileFlags(position, TileFlags.None);
            towerGrid.SetColor(position, Color.green);
        }
    }
}
