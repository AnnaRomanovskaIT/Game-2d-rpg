using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap _intercatibleMap;
    [SerializeField] private Tile _hiddenTile;
    [SerializeField] private TileBase _interactedTile;

    public TileBase InteractedTile
    {
        get => _interactedTile;
        set { _interactedTile = value; }
    }

    public void SetInteracted(Vector3 mouseWorldPos)
    {
        var cell = _intercatibleMap.WorldToCell(mouseWorldPos);
        _intercatibleMap.SetTile(cell, _interactedTile);
        Color color = new Color(255f, 255f, 255f, 255f);
        _intercatibleMap.SetColor(cell, color);
    }
}
