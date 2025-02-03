using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Inventory/Tool Definition", fileName = "New Tool Definition")]
public class ToolsDefinition : ItemDefinition
{
    public enum ToolType
    {
        Hoe, WateringCan, Axe, Pickaxe
    }
    public ToolType type;
}
