using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehavior : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] private Buildings_SO building;
    private void Start()
    {
        SetupGameObject();
    }
    private void OnValidate()
    {
        SetupGameObject();
    }
    private void SetupGameObject()
    {
        if(building==null) return;
        ChangeName();
        SetGameSprite();
        AdjustCollider();
    }

    private void ChangeName()
    {
        if(gameObject.name == $"{building.Name}") return;
        gameObject.name = $"{building.Name}";
    }
    private void SetGameSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = building.BuildingSprite;
    }
    private void AdjustCollider()
    {
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        Vector2 colliderSize = collider.bounds.size;
        Vector2 spriteSize = GetComponent<SpriteRenderer>().sprite.bounds.size;
        float yOffset = (-spriteSize.y + spriteSize.y*0.8f);
        collider.offset = new Vector2(0f, yOffset);
        collider.size = new Vector2(spriteSize.x, (spriteSize.y/1.5f));
    }

}
