using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected bool _isMovingRight = true;
    [SerializeField] protected float _speed;

    [SerializeField] protected float _spottedWaitTime;
    [SerializeField] protected float _spotDistance;

    protected Rigidbody2D _rb2d;
    protected Collider2D collider;
    protected Animator _animator;

    protected virtual void Move()
    {
        if (_isMovingRight)
            transform.position += new Vector3(_speed * Time.fixedDeltaTime, 0);
        else
            transform.position -= new Vector3(_speed * Time.fixedDeltaTime, 0);
    }

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    protected bool CheckForPlayer()
    {
        RaycastHit2D raycast = Physics2D.Raycast(new Vector2(transform.position.x+((_isMovingRight)?0.75f:-0.75f), transform.position.y), (float)((_isMovingRight) ? -1 : 1) * Vector3.left, _spotDistance);
        if (raycast.collider != null && raycast.collider.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER DETECTED!!!");
            return true;
        }
        return false;
    }

    protected bool CheckForWall()
    {
        RaycastHit2D raycast = Physics2D.Raycast(new Vector2(transform.position.x + ((_isMovingRight) ? 0.75f : -0.75f), transform.position.y), (float)((_isMovingRight) ? -1 : 1) * Vector3.left, _spotDistance);
        if (raycast.collider != null && raycast.collider.gameObject.layer == LayerMask.NameToLayer("DefaultTile"))
        {
            Debug.Log("Tile DETECTED!!!");
            return true;
        }
        return false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("COLLIDED WITH PLAYER");
            StartCoroutine(Player.Instance.Die());
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            StartCoroutine(Player.Instance.Die());
            Debug.Log("OMAE WA MOU SHINDEIRU!");
        }
    }


}
