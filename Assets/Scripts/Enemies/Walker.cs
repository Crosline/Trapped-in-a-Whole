using UnityEngine;
using Unity;
using System.Collections.Generic;
using System.Collections;

public class Walker : Enemy
{
    [SerializeField] private float _timeToWaitOnEdge;
    [SerializeField] private float _spottedWaitTime;
    [SerializeField] private float _spotDistance;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashRestoreTime;
 
    [SerializeField] private bool _isMovingRight = true;
    [SerializeField] private Transform _rayPosition;

    private bool _isWaiting = false;
    private bool _isDashing = false;

    public override void Move()
    {
        RaycastHit2D raycast = Physics2D.Raycast(_rayPosition.position, Vector3.down, 0.1f);

        if (raycast.collider != null)
            if (_isMovingRight)
                transform.position += new Vector3(_speed * Time.fixedDeltaTime, 0);
            else
                transform.position -= new Vector3(_speed * Time.fixedDeltaTime, 0);
        else
            StartCoroutine(WaitOnEdge());          
    }

    private void FixedUpdate()
    {
        if (!_isWaiting)
            Move();

        if (CheckForPlayer())
            StartCoroutine(SlimeDash());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y), transform.position+Vector3.left * _spotDistance);
    }

    private bool CheckForPlayer()
    {
        RaycastHit2D raycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector3.left, _spotDistance);
        if (raycast.collider != null && raycast.collider.gameObject.tag == "Player")
            return true;
        return false;
    }

    private IEnumerator SlimeDash()
    {
        _isDashing = true;
        _animator.SetBool("isSpotted", true);
        yield return new WaitForFixedUpdate();
        _animator.SetBool("isSpotted", false);

        for (float i = 0; i < _dashTime; i += Time.deltaTime)
        {
            RaycastHit2D raycast = Physics2D.Raycast(_rayPosition.position, Vector3.down, 0.1f);
            if (raycast.collider == null)
                break;

            transform.position += new Vector3((_isMovingRight) ? 1 : -1 * _dashSpeed * Time.fixedDeltaTime, 0);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSecondsRealtime(_dashRestoreTime);
        _animator.SetBool("isDashed", false);
        _animator.SetBool("isSpotted", false);
        _isDashing = false;
    }


    private IEnumerator WaitOnEdge()
    {
        _isWaiting = true;
        _animator.SetFloat("Speed", 0);
        yield return new WaitForSecondsRealtime(_timeToWaitOnEdge);      
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isMovingRight = !_isMovingRight;
        _animator.SetFloat("Speed", 1);
        _isWaiting = false;
    }
}
