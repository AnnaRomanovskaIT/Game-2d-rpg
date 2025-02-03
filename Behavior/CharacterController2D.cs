using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] GameObject _itemHolder;
    [SerializeField] Land land;
    private SpriteRenderer _spriteRendererItem;
    private Inventory _inventory;
    private Animator animator;
    private Vector2 motionVector;
    private Vector2 lastMotionVector;
    private InteractionArea interactionArea;
    private bool isMoving;
    private bool isHolding;
    new private Rigidbody2D rigidbody2D;
    public Vector2 MotionVector => motionVector;
    public Vector2 LastMotionVector => lastMotionVector;


    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _inventory = GetComponent<Inventory>();
        _spriteRendererItem = _itemHolder.GetComponent<SpriteRenderer>();
        interactionArea = GetComponentInChildren<InteractionArea>();
        interactionArea.InteractWithObject +=InteractWithObjectEventHandler;
    }
    private void OnDestroy()
    {
        interactionArea.InteractWithObject -=InteractWithObjectEventHandler;
    }
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        motionVector =
            new Vector2(horizontal, vertical);

        var motionVectorNormalized = motionVector.normalized;

        rigidbody2D.velocity = motionVectorNormalized * speed;

        animator.SetFloat("horizontal", horizontal);
        animator.SetFloat("vertical", vertical);

        isMoving = (horizontal != 0 || vertical != 0);
        animator.SetBool("isMoving", isMoving);

        if (horizontal != 0 || vertical != 0)
        {
            lastMotionVector =
            new Vector2(horizontal, vertical).normalized;

            animator.SetFloat("lastHorizontal", horizontal);
            animator.SetFloat("lastVertical", vertical);
        }

        if(!_inventory.HasToolEquipped())
        {
            isHolding = _inventory.HasItem();
        }
        else
        {
            isHolding = false;
        }
        animator.SetBool("isHolding", isHolding);
        if (isHolding)
        {
            _spriteRendererItem.enabled = true;
            _spriteRendererItem.sprite = _inventory.GetSpriteOfActiveItem();
        }
        else
        {
            _spriteRendererItem.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_inventory.HasToolEquipped("Hoe"))
            {
                StartCoroutine(PerformDigAnimation());
            }
            if (_inventory.HasToolEquipped("WateringCan"))
            {
                StartCoroutine(PerformWateringAnimation());
            }
            if (_inventory.HasPlantSeedEquipped() && 
                (land.CheckLandStatus(Land.LandStatus.Soil, interactionArea.HighlightPosition)||
                land.CheckLandStatus(Land.LandStatus.Watered, interactionArea.HighlightPosition)))
            {
                _inventory.PlantCrop(_inventory.ActiveSlotIndex, true);
            }
            else
            {}
        }
    }
private void InteractWithObjectEventHandler(object sender, GameObject Interactable)
{
    if (Interactable.TryGetComponent(out Seller sellerComponent))
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(_inventory.HasHarvestEquipped(_inventory.Slots[_inventory.ActiveSlotIndex]))
            {
                sellerComponent.Add(_inventory.Slots[_inventory.ActiveSlotIndex].Item,
                _inventory.Slots[_inventory.ActiveSlotIndex].NumberOfItems);
                _inventory.RemoveItem(_inventory.Slots[_inventory.ActiveSlotIndex].State);
            }
        }
    }
    if(Interactable.TryGetComponent(out CropBehavior plant))
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            bool isHarvestable = plant.cropState == CropState.Harvestable;
            if (isHarvestable && Input.GetKeyDown(KeyCode.Space))
                {
                    GameItemSpawner spawn = GetComponent<GameItemSpawner>();
                    //spawn item and change state to not harvastable
                    ItemStack stack = plant.Harvest();
                    spawn.SpawnItem(stack);
                }
        }
    }
}
IEnumerator PerformDigAnimation()
{
    // Stop movement
    rigidbody2D.velocity = Vector2.zero;
    // Trigger the animation
    animator.SetBool("isDig", true);
    // Wait for the animation to finish
    yield return new WaitForSeconds(0.28f);
    // After the animation, perform actions related to the tool
    land.SetInteractedLand("Hoe", interactionArea.HighlightPosition);
    // Reset the animation bool
    animator.SetBool("isDig", false);
    yield break;
}

IEnumerator PerformWateringAnimation()
{
    // Stop movement
    rigidbody2D.velocity = Vector2.zero;
    // Trigger the animation
    animator.SetBool("isWatered", true);
    // Wait for the animation to finish
    yield return new WaitForSeconds(0.28f);
    // After the animation, perform actions related to the tool
    land.SetInteractedLand("WateringCan", interactionArea.HighlightPosition);
    // Reset the animation bool
    animator.SetBool("isWatered", false);
    yield break;
}
}
