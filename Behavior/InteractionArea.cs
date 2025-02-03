using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
public class InteractionArea : MonoBehaviour
{
    CharacterController2D character;
    [SerializeField] private RectTransform position;
    [SerializeField] private float adjustmentDistance=0.1f;
    private GameItemSpawner spawn;
    public Tilemap groundTilemap;
    public GameObject highlightPrefab;
    private GameObject currentHighlight;
    private Vector3 highlightPosition;
    public Vector3 HighlightPosition => highlightPosition;
    Vector3Int cellPosition;
public event EventHandler<GameObject> InteractWithObject;
    private void Awake()
    {
        character = GetComponentInParent<CharacterController2D>();
        spawn = GetComponentInParent<GameItemSpawner>();
    }

    void Update()
    {
        Vector2 motionVector = character.LastMotionVector;
        Vector3 areaPosition = position.position;
        Vector2 adjustmentVector = motionVector.normalized * adjustmentDistance;
        areaPosition += new Vector3(adjustmentVector.x, adjustmentVector.y, 0f);
        RaycastHit2D hit = Physics2D.Raycast(areaPosition, Vector2.zero);
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            InteractWithObject?.Invoke(this, hitObject);
        }
        cellPosition = groundTilemap.WorldToCell(areaPosition);
        Vector3 highlightWorldPosition = groundTilemap.GetCellCenterWorld(cellPosition);
        if (currentHighlight != null)
        {
            Destroy(currentHighlight);
        }
        currentHighlight = Instantiate(highlightPrefab, highlightWorldPosition, Quaternion.identity);
        highlightPosition = currentHighlight.transform.position;
    }
    public Vector3Int CellPosition() {  return cellPosition; }
}
