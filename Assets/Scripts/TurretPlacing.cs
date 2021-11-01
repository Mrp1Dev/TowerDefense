using UnityEngine;
using UnityEngine.Tilemaps;
using MUtility;
using System.Collections.Generic;
namespace TowerDefense
{
    //TODO: Refactor.
    public class TurretPlacing : MonoBehaviour
    {
        [SerializeField] private Tilemap towerGrid;
        [SerializeField] private GameObject towerPrefab;
        [SerializeField] private List<Transform> path;
        private HashSet<Vector3Int> takenPositions = new HashSet<Vector3Int>();
        private Vector3Int? previousSelected;
        private Color normalColor;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3Int position = Vector3Int.RoundToInt(towerGrid.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition).XY()));
                if (towerGrid.GetTile(position) != null)
                {
                    towerGrid.SetTileFlags(position, TileFlags.None);
                    if (towerGrid.GetColor(position) == Color.green)
                    {
                        if (takenPositions.Contains(position) == false)
                        {
                            var spawned = 
                                Instantiate(towerPrefab, towerGrid.transform.TransformPoint(position), Quaternion.identity, towerGrid.transform);
                            spawned.GetComponent<Turret>().Init(path);
                            takenPositions.Add(position);
                        }
                    }
                    else
                    {
                        if (previousSelected != null)
                        {
                            towerGrid.SetColor(previousSelected.Value, normalColor);
                        }
                        normalColor = towerGrid.GetColor(position);
                        towerGrid.SetColor(position, Color.green);
                        previousSelected = position;
                    }
                }
            }
        }
    }
}
