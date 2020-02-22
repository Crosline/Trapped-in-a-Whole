using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected Rigidbody2D _rb2d;
    protected Collider2D collider;
    protected Animator _animator;

    public abstract void Move();

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        _rb2d = GetComponent<Rigidbody2D>();
    }
}
