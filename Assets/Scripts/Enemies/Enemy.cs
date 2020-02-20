using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected Rigidbody2D _rb2d;
    [SerializeField] protected Collider2D collider;
    
    public abstract void Move();

    protected void Awake()
    {
        collider = GetComponent<Collider2D>();
        _rb2d = GetComponent<Rigidbody2D>();
    }
}
