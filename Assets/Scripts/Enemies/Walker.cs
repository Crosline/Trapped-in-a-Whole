using UnityEngine;
using Unity;
using System.Collections.Generic;
using System.Collections;

public class Walker : Enemy
{
    [SerializeField] private float _timeToWaitOnEdge;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashRestoreTime;
 
    [SerializeField] private Transform _rayPosition;
    [SerializeField] private bool isDashingAvaible;

    private bool _isWaiting = false;
    private bool _isDashing = false;

    protected override void Move()
    {
        RaycastHit2D raycast = Physics2D.Raycast(_rayPosition.position, Vector3.down, 0.1f);

        if (raycast.collider != null)
            base.Move();
        else
            StartCoroutine(WaitOnEdge());          
    }

    private void FixedUpdate()
    {
        if (!_isWaiting && !_isDashing)
            Move();

        if (!_isDashing && CheckForPlayer() && isDashingAvaible)
            StartCoroutine(SlimeDash());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(transform.position.x + ((_isMovingRight)?0.75f:-0.75f), transform.position.y), transform.position+ (float)((_isMovingRight) ? -1 : 1) * Vector3.left * _spotDistance);
    }

    private IEnumerator SlimeDash()
    {
        _isDashing = true;
        _animator.SetFloat("Speed", 0f);
        _animator.SetTrigger("isSpotted");
        
        yield return new WaitForSecondsRealtime(_spottedWaitTime);
        
        _animator.SetFloat("Speed", 1f);
        _animator.SetBool("isDashed", true);
        _animator.ResetTrigger("isSpotted");

        yield return new WaitForFixedUpdate();

        var k = -1;
        if (_isMovingRight)
            k = 1;

        for (float i = 0; i < _dashTime; i += Time.deltaTime)
        {
            RaycastHit2D raycast = Physics2D.Raycast(_rayPosition.position, Vector3.down, 0.1f);
            if (raycast.collider == null)
                break;

            transform.position += new Vector3(k * _dashSpeed * Time.fixedDeltaTime, 0);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSecondsRealtime(_dashRestoreTime);
        _animator.SetBool("isDashed", false);
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
