using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Seed Definition", fileName = "New Seed Definition")]
public class PlantDefinition : ItemDefinition
{
    [SerializeField] private int _daysToGrow;
    [SerializeField] private HarvestDefinition _itemToCollect;
    [SerializeField] private Sprite[] _growingPlantSprites;
    [SerializeField] private int _howManyTimesHarvestable;
    [SerializeField] private int _NumberOfItemToCollect;

    public int DaysToGrow => _daysToGrow;
    public ItemDefinition ItemToCollect => _itemToCollect;
    public Sprite[] GrowingPlantSprites => _growingPlantSprites;
    public int TimesHarvestable => _howManyTimesHarvestable;
    public int NumberOfItemsToCollect => _NumberOfItemToCollect;
}
