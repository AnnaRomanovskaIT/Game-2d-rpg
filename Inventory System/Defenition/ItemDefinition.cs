using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item Definition", fileName = "New Item Definition")]
public class ItemDefinition : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private bool _isStackable;
    [SerializeField] private Sprite _inGameSprite;
    [SerializeField] private Sprite _iconSprite;
    [SerializeField] private string _description;

    public string Name => _name;
    public bool IsStackable => _isStackable;
    public Sprite inGameSprite => _inGameSprite;
    public Sprite iconSprite => _iconSprite;
    public string Description => _description;
}
