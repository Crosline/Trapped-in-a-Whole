using UnityEngine;

public class CrystalShard : MonoBehaviour, IPickUp {

    private Collider2D _collider2D;
    private Animator _animator;
    private bool isPickedUp = false;
    public LayerMask layerMask;

    private void Awake()
    {
        _collider2D = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    public void PickUp()
    {
        isPickedUp = true;
        _animator.SetTrigger("Collect");
        _collider2D.enabled = false;
        PickUpEffect();
    }

    public void PickUpEffect() => Player.CurrentLevel.ShardsCollected++;


    void Update() {

        if (isPickedUp) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.4f, layerMask);
            Debug.DrawRay(transform.position, Vector2.down * (.4f), Color.red);
            if (hit.collider.tag != "DefaultTile")
                transform.position = new Vector2(transform.position.x, transform.position.y - 1 * Time.deltaTime);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isPickedUp)
            PickUp();

    }
}