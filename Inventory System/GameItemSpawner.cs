using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class GameItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _itemBasePrefab;
    [SerializeField] private GameObject _plantBasePrefab;
    CharacterController2D _c2d;
    public event Action<GameObject, Vector3Int> OnPlantPlanted;

    private void Awake()
    {
        _c2d = GetComponent<CharacterController2D>();
    }

    //function to spawn ItemStack on tha field
    public void SpawnItem(ItemStack itemStack)
    {
        //check if prefab is initialized
        if (_itemBasePrefab == null) return;
        //create a new game object
        var item = PrefabUtility.InstantiatePrefab(_itemBasePrefab) as GameObject;
        item.transform.position = transform.position;
        var gameItemScript = item.GetComponent<GameItem>();
        //set up game object
        gameItemScript.SetStack(new ItemStack(itemStack.Item, itemStack.NumberOfItems));
        gameItemScript.Throw(_c2d.LastMotionVector.x);
    }

    //function to spawn plant object on the field
    public void SpawnPlant(PlantDefinition seed)
    {
        //check if prefab is initialized
        if (_plantBasePrefab == null) return;
        InteractionArea interactionArea = GetComponentInChildren<InteractionArea>();
        //check if player has InteractionArea 
        if (interactionArea == null) return;

        //check if there is anouther plant on that tile
        if (GameManager.instance.plantManager.isThereIsAnotherPlant(interactionArea.CellPosition())) return;

        GameObject plant = Instantiate(_plantBasePrefab);
        Vector3 cellPosition = interactionArea.HighlightPosition;
        plant.transform.position = new Vector3(cellPosition.x, cellPosition.y + 0.19f, 0);

        CropBehavior cropBehavior = plant.GetComponent<CropBehavior>();
        //check if prefab has Component CropBehavior
        if (cropBehavior == null) return;

        cropBehavior.Plant(seed);
        OnPlantPlanted?.Invoke(plant, interactionArea.CellPosition());
    }
}
