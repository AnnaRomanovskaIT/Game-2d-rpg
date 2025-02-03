using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Behavior : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private UI_Inventory _inventory;
    void Awake()
    {
        _menuPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        if(!_menuPanel.activeSelf)
        {
            _menuPanel.SetActive(true);
        }
        else
        {
            _inventory.DeleteAllChildren();
            _inventory.InitializeInventoryUI();
            _menuPanel.SetActive(false);
        }
    }
}
