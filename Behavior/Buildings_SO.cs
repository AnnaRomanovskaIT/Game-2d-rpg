using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Building", fileName = "New Building")]
public class Buildings_SO : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _inGameSprite;
    [SerializeField] private SceneReference _buildingScene;
    public string Name => _name;
    public Sprite BuildingSprite => _inGameSprite;
    public SceneReference BuildingScene => _buildingScene;
}
