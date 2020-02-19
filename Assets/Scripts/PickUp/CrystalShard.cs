using UnityEngine;

public class CrystalShard : MonoBehaviour, IPickUp
{
    private Collider2D _collider;
    private Animator _animator;
    private bool isPickedUp = false;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    public void PickUp()
    {
        isPickedUp = true;
        _animator.Play("Shard Die Animation Name");
        PickUpEffect();
    }

    public void PickUpEffect() => Player.CurrentLevel.ShardsCollected++;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isPickedUp)
            PickUp();
    }
}