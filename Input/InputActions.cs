//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/WorldInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""WorldInput"",
    ""maps"": [
        {
            ""name"": ""Inventory"",
            ""id"": ""5e585dfa-1659-4259-94cd-6931a8a311bc"",
            ""actions"": [
                {
                    ""name"": ""PreviousItem"",
                    ""type"": ""Button"",
                    ""id"": ""0191aeac-ab37-4368-ba20-aab57e02c472"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextItem"",
                    ""type"": ""Button"",
                    ""id"": ""3d9d0988-e777-4aca-b96f-6bd6a8ba9728"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThrowItem"",
                    ""type"": ""Button"",
                    ""id"": ""a9aa1191-a653-496a-8c70-537096bf57ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9f1cc88a-7ae6-4a3e-94d4-13305c410264"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6834332a-737f-4416-a592-ddc73be528d1"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65d0554a-0f99-4b1b-81e7-1309ef820cea"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_PreviousItem = m_Inventory.FindAction("PreviousItem", throwIfNotFound: true);
        m_Inventory_NextItem = m_Inventory.FindAction("NextItem", throwIfNotFound: true);
        m_Inventory_ThrowItem = m_Inventory.FindAction("ThrowItem", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Inventory
    private readonly InputActionMap m_Inventory;
    private List<IInventoryActions> m_InventoryActionsCallbackInterfaces = new List<IInventoryActions>();
    private readonly InputAction m_Inventory_PreviousItem;
    private readonly InputAction m_Inventory_NextItem;
    private readonly InputAction m_Inventory_ThrowItem;
    public struct InventoryActions
    {
        private @InputActions m_Wrapper;
        public InventoryActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @PreviousItem => m_Wrapper.m_Inventory_PreviousItem;
        public InputAction @NextItem => m_Wrapper.m_Inventory_NextItem;
        public InputAction @ThrowItem => m_Wrapper.m_Inventory_ThrowItem;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void AddCallbacks(IInventoryActions instance)
        {
            if (instance == null || m_Wrapper.m_InventoryActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_InventoryActionsCallbackInterfaces.Add(instance);
            @PreviousItem.started += instance.OnPreviousItem;
            @PreviousItem.performed += instance.OnPreviousItem;
            @PreviousItem.canceled += instance.OnPreviousItem;
            @NextItem.started += instance.OnNextItem;
            @NextItem.performed += instance.OnNextItem;
            @NextItem.canceled += instance.OnNextItem;
            @ThrowItem.started += instance.OnThrowItem;
            @ThrowItem.performed += instance.OnThrowItem;
            @ThrowItem.canceled += instance.OnThrowItem;
        }

        private void UnregisterCallbacks(IInventoryActions instance)
        {
            @PreviousItem.started -= instance.OnPreviousItem;
            @PreviousItem.performed -= instance.OnPreviousItem;
            @PreviousItem.canceled -= instance.OnPreviousItem;
            @NextItem.started -= instance.OnNextItem;
            @NextItem.performed -= instance.OnNextItem;
            @NextItem.canceled -= instance.OnNextItem;
            @ThrowItem.started -= instance.OnThrowItem;
            @ThrowItem.performed -= instance.OnThrowItem;
            @ThrowItem.canceled -= instance.OnThrowItem;
        }

        public void RemoveCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IInventoryActions instance)
        {
            foreach (var item in m_Wrapper.m_InventoryActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_InventoryActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    public interface IInventoryActions
    {
        void OnPreviousItem(InputAction.CallbackContext context);
        void OnNextItem(InputAction.CallbackContext context);
        void OnThrowItem(InputAction.CallbackContext context);
    }
}
