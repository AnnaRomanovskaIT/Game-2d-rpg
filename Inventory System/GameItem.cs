using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameItem : MonoBehaviour
{
    [SerializeField] private ItemStack _stack;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _colliderEnablesAfterTime = 1f;
    [Header("ThrowSettings")]
    [SerializeField] private float _throwGravity = 2f;
    [SerializeField] private float _throwMinXForce = 3f;
    [SerializeField] private float _throwMaxXForce = 5f;
    [SerializeField] private float _throwYForce = 5f;

    private Collider2D _collider;
    private Rigidbody2D _rb;
    public ItemStack Stack => _stack;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rb = GetComponent<Rigidbody2D>();
        _collider.enabled = false;
    }

    private void Start()
    {
        SetupGameObject();
        StartCoroutine(EnableCollider(_colliderEnablesAfterTime));
    }

    private void OnValidate()
    {
        SetupGameObject();
    }

    private void SetupGameObject()
    {
        if (_stack.Item == null) return;
        SetGameSprite();
        AdjustNumberOfItem();
        UpdateGameObjectName();
    }

    private void SetGameSprite()
    {
        _spriteRenderer.sprite = _stack.Item.inGameSprite;
    }

    private void UpdateGameObjectName()
    {
        var name = _stack.Item.name;
        var number = _stack.isStackable ? _stack.NumberOfItems.ToString() : "ns";
        gameObject.name = $"{name} ({number})";
    }

    private void AdjustNumberOfItem()
    {
        _stack.NumberOfItems = _stack.NumberOfItems;
    }

    public ItemStack Pick()
    {
        Destroy(gameObject);
        return _stack;
    }

    public void Throw(float xDir)
    {
        _rb.gravityScale = _throwGravity;
        var throwXForce = Random.Range(_throwMinXForce, _throwMaxXForce);
        _rb.velocity = new Vector2(Mathf.Sign(xDir) * throwXForce, _throwYForce);
        StartCoroutine(DisableGravity(_throwYForce));
    }

    private IEnumerator DisableGravity(float atYVelocity)
    {
        yield return new WaitUntil(() => _rb.velocity.y < -atYVelocity);
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 0;
    }

    private IEnumerator EnableCollider(float afterTime)
    {
        yield return new WaitForSeconds(afterTime);
        _collider.enabled = true;
    }

    public void SetStack(ItemStack itemStack)
    {
        _stack = itemStack;
    }
}
