using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CropState
{
    Seed, Seedling, Harvestable, NotHarvestable
}

public class CropBehavior : MonoBehaviour
{
    [Header("Stages of Life")]
    public Sprite[] seedSprite;
    public Sprite[] seedlingSprites;
    public Sprite harvestableSprite;
    public Sprite notHarvestableSprite;
    [SerializeField] public PlantDefinition seedToGrow;
    [SerializeField] private GameObject gameObject;
    public CropState cropState;
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    private int seedlingIndex = 0;
    public int DaysToGrow => seedToGrow.DaysToGrow;
    public int TimesHarvestable => seedToGrow.TimesHarvestable;
    private void Awake()
    {
        GameManager.instance.worldTime.DayCountChanged += OnDayCountChanged;
    }
    private void OnDestroy()
    {
        GameManager.instance.worldTime.DayCountChanged -= OnDayCountChanged;
    }
    private void OnValidate()
    {
        if (seedToGrow == null) return;
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        SetupGameObject(cropState);
    }
    public void Plant(PlantDefinition seed)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        seedToGrow = seed;
        SetupGameObject();
    }

    private void OnDayCountChanged(object sender, int newDayCount)
    {
        Grow(newDayCount);
    }

    private void Grow(int dayCount)
    {
        float DaysToGrow = seedToGrow.DaysToGrow;
        if(dayCount == (int)(DaysToGrow * 0.25))
        {
            seedlingIndex = 0;
            SwithStates(CropState.Seedling);
            return;
        }
        if (dayCount == (int)(DaysToGrow * 0.50))
        {
            seedlingIndex = 1;
            SwithStates(CropState.Seedling);
            return;
        }
        if (dayCount == (int)(DaysToGrow * 0.75))
        {
            seedlingIndex = 2;
            SwithStates(CropState.Seedling);
            return;
        }
        if (dayCount == DaysToGrow)
        {
            SwithStates(CropState.Harvestable);
            return;
        }
    }

    public ItemStack Harvest()
    {
        if(cropState == CropState.Harvestable)
        {
            ItemStack harvest = new ItemStack(seedToGrow.ItemToCollect, seedToGrow.NumberOfItemsToCollect);
            cropState = CropState.NotHarvestable;
            return harvest;
        }
        return null;
    }

    public bool isManyTimesHarvestable()
    {
        return seedToGrow.TimesHarvestable>1;
    }

    private void SetupGameObject()
    {
        if (seedToGrow == null) return;
        UpdateGameObjectName();
        InitializePlantSprites();
        SwithStates();
        AdjustCollider();
    }
private void SetupGameObject(CropState target)
    {
        if (seedToGrow == null) return;
        UpdateGameObjectName();
        InitializePlantSprites();
        SwithStates(target);
        AdjustCollider();
    }
    private void UpdateGameObjectName()
    {
        var name = seedToGrow.Name;
        gameObject.name = $"{name}";
    }

    private void AdjustCollider()
    {
        Vector2 colliderSize = collider.bounds.size;
        Vector2 spriteSize = GetComponent<SpriteRenderer>().sprite.bounds.size;
        float yOffset = (-spriteSize.y + spriteSize.y*0.8f);
        collider.offset = new Vector2(0f, yOffset);
    }

    private void InitializePlantSprites()
    {
        if (seedToGrow == null) return;
        if (seedToGrow.GrowingPlantSprites == null) return;  
        seedSprite = new Sprite[2];
        seedSprite[0] = seedToGrow.GrowingPlantSprites[0];
        seedSprite[1] = seedToGrow.GrowingPlantSprites[1];
        seedlingSprites = new Sprite[3];
        seedlingSprites[0] = seedToGrow.GrowingPlantSprites[2];
        seedlingSprites[1] = seedToGrow.GrowingPlantSprites[3];
        seedlingSprites[2] = seedToGrow.GrowingPlantSprites[4];
        harvestableSprite = seedToGrow.GrowingPlantSprites[5];
        if (seedToGrow.TimesHarvestable>1)
        {
            notHarvestableSprite = seedToGrow.GrowingPlantSprites[6];
        }
    }

    public void SwithStates(CropState stateToSwitch=CropState.Seed)
    {
        cropState = stateToSwitch;
        switch (stateToSwitch)
        {
            case CropState.Seed:
                spriteRenderer.sprite = seedSprite[Random.Range(0, seedSprite.Length)];
                break;
            case CropState.Seedling:
                spriteRenderer.sprite = seedlingSprites[seedlingIndex];
                break;
            case CropState.Harvestable:
                spriteRenderer.sprite = harvestableSprite;
                break;
            case CropState.NotHarvestable:
                spriteRenderer.sprite = notHarvestableSprite;
                break;
        }
    }
}
