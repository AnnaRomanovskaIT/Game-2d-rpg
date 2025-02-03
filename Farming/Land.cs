using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Land : MonoBehaviour
{
    public enum LandStatus
    {
        Soil, Watered, Standart
    }

    public class TileStatus
    {
        private Vector3 _position;
        private LandStatus _landStatus;
        public Vector3 Position { get => _position; set => _position = value; }
        public LandStatus Status { get => _landStatus; set => _landStatus = value; }
        public TileStatus() { }
        public TileStatus(Vector3 position, LandStatus status)
        {
            _position = position;
            _landStatus = status;
        }
    }

    [SerializeField] private RuleTile soilTile;
    [SerializeField] private RuleTile wateredTile;
    [SerializeField] private TileBase interactive;
    [SerializeField] private Tilemap _intercatibleMap;
    [SerializeField] private Tilemap overlapMap;
    private List<TileStatus> _tilesStatus = new List<TileStatus>();
    public TileStatus TilesStatus(int i) => _tilesStatus[i];
    public Tilemap IntercatibleMap => _intercatibleMap;
    private void Start()
    {
        foreach (var position in _intercatibleMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = _intercatibleMap.GetTile(position);
            if (tile != null && tile.name == "Interactable")
            {
                TileStatus tileStatus = new TileStatus(position, LandStatus.Standart);
                _tilesStatus.Add(tileStatus);
                _intercatibleMap.SetTile(position, interactive);
            }
        }
    }

    private void Awake()
    {
        GameManager.instance.worldTime.DayCountChanged += OnDayCountChanged;
    }
    private void OnDestroy()
    {
        GameManager.instance.worldTime.DayCountChanged -= OnDayCountChanged;
    }

    private void OnDayCountChanged(object sender, int newDayCount)
    {
        StartCoroutine(OnDayCountChangedCoroutine(newDayCount));
    }

    private IEnumerator OnDayCountChangedCoroutine(int newDayCount)
    {
        yield return new WaitForSeconds(0.01f);
        DestroyOverlapMap();
    }

    private void DestroyOverlapMap()
    {
        BoundsInt bounds = overlapMap.cellBounds;
        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            TileBase tile = overlapMap.GetTile(position);
            if (tile != null)
            {
                overlapMap.SetTile(position, null);
                int index = FindIndexByPosition(position);
                _tilesStatus[index].Status = LandStatus.Soil;
            }
        }
    }

    public bool CheckLandStatus(LandStatus targetlandStatus, Vector3 position)
    {
        var cell = _intercatibleMap.WorldToCell(position);
        int index = FindIndexByPosition(cell);
        if (index >= 0)
        {
            return _tilesStatus[index].Status==targetlandStatus;
        }
        return false;
    }

    public bool CheckLandStatus(LandStatus targetlandStatus, Vector3Int position)
    {
        int index = FindIndexByPosition(position);
        if (index >= 0)
        {
            return _tilesStatus[index].Status == targetlandStatus;
        }
        return false;
    }

    public int FindIndexByPosition(Vector3 position)
    {
        for (int i = 0; i < _tilesStatus.Count; i++)
        {
            if (Vector3.Equals(position, _tilesStatus[i].Position))
            {
                return i;
            }
        }
        return -1;
    }

    public void SetInteractedLand(string toolType, Vector3 position)
    {
        var cell = _intercatibleMap.WorldToCell(position);
        int index = FindIndexByPosition(cell);
        if (index >= 0)
        {
            if (toolType == "Hoe" && _tilesStatus[index].Status == LandStatus.Standart)
            {
                _tilesStatus[index].Status = LandStatus.Soil;
                SetInteracted(cell, _intercatibleMap, soilTile);
            }
            if (toolType == "WateringCan" && _tilesStatus[index].Status == LandStatus.Soil)
            {
                _tilesStatus[index].Status = LandStatus.Watered;
                overlapMap.SetTile(cell, wateredTile);
            }
        }
    }

    private void SetInteracted(Vector3Int cell, Tilemap map, TileBase tile)
    {
        map.SetTile(cell, tile);
        Color color = new Color(255f, 255f, 255f, 255f);
        map.SetColor(cell, color);
    }
}
