using UnityEngine;
using Unity;

public class LRWalker : Enemy
{
    [SerializeField] private bool isMovingRight = true;
    [SerializeField] private Transform rayPosition;

    public override void Move()
    {
        RaycastHit2D raycast = Physics2D.Raycast(rayPosition.position, Vector3.down, 0.1f);

        if (raycast.collider != null)
        {
            if (isMovingRight)
            {
                transform.position += new Vector3(_speed*Time.fixedDeltaTime, 0);
            }
            else
            {
                transform.position -= new Vector3(_speed * Time.fixedDeltaTime, 0);
            }
        }    
        else
        {
            isMovingRight = !isMovingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
}