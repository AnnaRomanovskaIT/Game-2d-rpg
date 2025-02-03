using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public class PlantInformation
    {
        public Vector3Int Position { get; private set; }
        public int DayCount { get; private set; }
        public GameObject Plant { get; private set; }

        public PlantInformation(Vector3Int position, int dayCount, GameObject plant)
        {
            Position = position;
            DayCount = dayCount;
            Plant = plant;
        }
    }

    [SerializeField] private Land land;
    [SerializeField] private GameItemSpawner spawn;
    
    public event Action<GameObject, Vector3Int> OnPlantPlanted;
    private List<PlantInformation> plantedPlants = new List<PlantInformation>(); // Stores the position of planted plants, the day count when they were planted, gameobject

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        spawn.OnPlantPlanted += HandlePlantPlanted;
        GameManager.instance.worldTime.DayCountChanged += DayChangedEventHandler;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Unsubscribe()
    {
        spawn.OnPlantPlanted -= HandlePlantPlanted;
        GameManager.instance.worldTime.DayCountChanged -= DayChangedEventHandler;
    }

    private void HandlePlantPlanted(GameObject plant, Vector3Int position)
    {
        int dayCount = GameManager.instance.worldTime.DayCount;
        PlantInformation plantInfo = new PlantInformation(position, dayCount, plant);
        plantedPlants.Add(plantInfo);
    }

    private void DayChangedEventHandler(object sender, int newDayCount)
    {
        List<Vector3Int> plantsToRemove = new List<Vector3Int>();

        foreach (var plant in plantedPlants)
        {
            if(plant.Plant!=null)
            {
            Vector3Int position = plant.Position;
            int plantedDay = plant.DayCount;

            // Calculate the days since planted using the new day count directly
            int daysSincePlanted = newDayCount - plantedDay;

            // If the plant hasn't been watered within 1 day, add it to the list of plants to remove
            if (daysSincePlanted >= 1 && !land.CheckLandStatus(Land.LandStatus.Watered, position) &&
                plant!=null)
            {
                plantsToRemove.Add(position);
            }
            //count days after harvesting
            CropBehavior cropBehavior = plant.Plant.GetComponent<CropBehavior>();
            if(cropBehavior!=null)
            {
                if(cropBehavior.cropState == CropState.NotHarvestable)
                {
                    cropBehavior.cropState = CropState.Harvestable;
                }
                if (cropBehavior.cropState == CropState.Harvestable)
                {
                    if(cropBehavior.isManyTimesHarvestable())
                    {
                        int daysSinceHarvest = newDayCount - cropBehavior.DaysToGrow;
                        if (daysSinceHarvest==cropBehavior.TimesHarvestable)
                        {
                            plantsToRemove.Add(position);
                        }
                    }
                    else
                    {
                        plantsToRemove.Add(position);
                    }
                }
            }
            }
            else
            {
                plantsToRemove.Add(plant.Position);
            }
        }

        // Destroy the plants in the separate list and remove them from the main list
        foreach (var position in plantsToRemove)
        {
            DestroyPlant(position);
        }
    }

private void DestroyPlant(Vector3Int position)
{
    int index = FindIndexByPosition(position);
    if (index >= 0)
    {
        if (plantedPlants[index].Plant != null)
        {
        GameObject plantObject = plantedPlants[index].Plant;
        Destroy(plantObject);
        }
        plantedPlants.RemoveAt(index);
    }
}

    public int FindIndexByPosition(Vector3Int position)
    {
        for (int i = 0; i < plantedPlants.Count; i++)
        {
            if (Vector3Int.Equals(position, plantedPlants[i].Position))
            {
                return i;
            }
        }
        return -1;
    }

    public bool isThereIsAnotherPlant(Vector3Int position)
    {
        int index = FindIndexByPosition(position);
        for (int i = 0; i < plantedPlants.Count; i++)
        {
            if (Vector3Int.Equals(position, plantedPlants[i].Position))
            {
                return true;
            }
        }
        return false;
    }
}
