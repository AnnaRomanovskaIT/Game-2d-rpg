using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInputHandler : MonoBehaviour
{
    private Inventory _inventory;

    private void Awake()
    {
        _inventory = GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        InputActions.Instance.Inventory.ThrowItem.performed += OnThrowItem;
        InputActions.Instance.Inventory.NextItem.performed += OnNextItem;
        InputActions.Instance.Inventory.PreviousItem.performed += OnPreviousItem;
    }

    private void OnDisable()
    {
        InputActions.Instance.Inventory.ThrowItem.performed -= OnThrowItem;
        InputActions.Instance.Inventory.NextItem.performed -= OnNextItem;
        InputActions.Instance.Inventory.PreviousItem.performed -= OnPreviousItem;
    }

    private void OnThrowItem(InputAction.CallbackContext ctx)
    {
        if (_inventory.GetActiveSlot().HasItem())
        {
            _inventory.RemoveItem(_inventory.ActiveSlotIndex, true);
        }
    }

    private void OnPreviousItem(InputAction.CallbackContext ctx)
    {
        _inventory.ActivateSlot(_inventory.ActiveSlotIndex + 1);
    }

    private void OnNextItem(InputAction.CallbackContext ctx)
    {
        _inventory.ActivateSlot(_inventory.ActiveSlotIndex - 1);
    }
}
