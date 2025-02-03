using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private GameItemSpawner spawn;
    [SerializeField] private ToolsDefinition toolToInteract;
    [SerializeField] private ItemDefinition itemToCollect;
    [SerializeField] private int amoutOfItem;
    [SerializeField] private int amoutOfHit;
    public ToolsDefinition tool=>toolToInteract;
    private int hitCount=0;
    public void Interact(GameObject gameObject)
    {
        hitCount++;
        if(hitCount==amoutOfHit)
        {
            ItemStack stack = new ItemStack(itemToCollect, amoutOfItem);
            spawn.SpawnItem(stack);
            Destroy(gameObject);
            hitCount=0;
        }
    }
}
