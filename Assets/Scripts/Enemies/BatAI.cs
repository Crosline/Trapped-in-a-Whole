using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum BatMoveType
{
    Horizontal,
    Vertical,
    Diagonal,
    Random
}

public enum BatMoveLenght
{
    ByDistance,
    UntilCloseToCollider
}


public class BatAI : Enemy
{
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashRestoreTime;
    [SerializeField] private float _timeToWait;
    [SerializeField] private bool isDashingAvaible;
    [SerializeField] private BatMoveType _moveType;
    [SerializeField] private BatMoveLenght _moveLenght;
    [SerializeField] private float _moveDistance;

    private bool _isDistance;
    [SerializeField] private bool _isWaiting = false;
    [SerializeField] private bool _isDashing = false;
    [SerializeField] private bool isDashingAvailable = false;
    [SerializeField] private bool _isMoving = true;

    protected override void Move()
    {

        switch(_moveType)
        {
            case BatMoveType.Horizontal:
                StartCoroutine(MoveHorizontal());
                break;

            case BatMoveType.Diagonal:
                StartCoroutine(MoveDiagonal());
                break;

            case BatMoveType.Vertical:
                StartCoroutine(MoveVertical());
                break;

            case BatMoveType.Random:
                break;
        }
    }

    private void Start()
    {
        _isDistance = (_moveLenght == BatMoveLenght.ByDistance) ? true : false;
    }

    private void FixedUpdate()
    {
        if (!_isWaiting && !_isDashing && !_isMoving)
            Move();

        if (!_isDashing && CheckForPlayer() && isDashingAvaible)
            StartCoroutine(BatDash());
    }

    private IEnumerator MoveHorizontal()
    {
        Debug.Log("MovedHorizontal!");

        _isMoving = true;
        var k = -1;
        if (_isMovingRight)
            k = 1;

        if (_isDistance)
        {
            var distance = _moveDistance;

            for (float i = 0; i<distance; i+=_speed/100)
            {
                Debug.Log(CheckForPlayer());
                if (CheckForPlayer() && !isDash() && isDashingAvaible)
                {
                    StartCoroutine(BatDash());
                    distance += _speed * Time.fixedDeltaTime;
                    yield return new WaitWhile(isDash);
                }

                transform.position += new Vector3(k *_speed * Time.fixedDeltaTime, 0);
                yield return new WaitForFixedUpdate();
            }

            StartCoroutine(Wait());
            _isMovingRight = !_isMovingRight;
            yield return new WaitWhile(isWait);

            //Back To Center 
            for (float i = 0; i < distance; i += _speed / 100)
            {
                if (CheckForPlayer() && !isDash() && isDashingAvaible)
                {
                    StartCoroutine(BatDash());
                    distance += _speed * Time.fixedDeltaTime;
                    yield return new WaitWhile(isDash);
                }

                transform.position -= new Vector3(k * _speed * Time.fixedDeltaTime, 0);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            var startPos = transform.position;

            while(true)
            {
                if (CheckForPlayer() && !isDash() && isDashingAvaible)
                {
                    StartCoroutine(BatDash());
                    yield return new WaitWhile(isDash);
                }

               
                if (CheckForWall())
                    break;

                transform.position += new Vector3(k * _speed * Time.fixedDeltaTime, 0);
                yield return new WaitForFixedUpdate();
            }

            _isMovingRight = !_isMovingRight;
            StartCoroutine(Wait());
            yield return new WaitWhile(isWait);
            var distance = (transform.position - startPos).magnitude;

            for (float i = 0; i<distance; i += _speed/100)
            {
                if (CheckForPlayer() && !isDash() && isDashingAvaible)
                {
                    StartCoroutine(BatDash());
                    distance += _speed * Time.fixedDeltaTime;
                    yield return new WaitWhile(isDash);
                }
                transform.position -= new Vector3(k * _speed * Time.fixedDeltaTime, 0);
                yield return new WaitForFixedUpdate();
            }
        }

        _isMoving = false;

        bool isDash() => _isDashing;
        bool isWait() => _isWaiting;
    }


    private IEnumerator MoveVertical()
    {
        _isMoving = true;
        var k = 1;
        if (_isMovingRight)
            k = -1;

        if (_isDistance)
        {
            var distance = _moveDistance;

            for (float i = 0; i < distance; i += _speed / 100)
            {
                Debug.Log(CheckForPlayerBat());
                if (CheckForPlayerBat() && !isDash())
                {
                    StartCoroutine(BatDash());
                    distance += _speed * Time.fixedDeltaTime;
                    yield return new WaitWhile(isDash);
                }

                transform.position += new Vector3(0, k * _speed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            StartCoroutine(Wait());
            _isMovingRight = !_isMovingRight;
            yield return new WaitWhile(isWait);

            //Back To Center 
            for (float i = 0; i < distance; i += _speed / 100)
            {
                if (CheckForPlayerBat() && !isDash())
                {
                    StartCoroutine(BatDash());
                    distance += _speed * Time.fixedDeltaTime;
                    yield return new WaitWhile(isDash);
                }

                transform.position -= new Vector3(0, k * _speed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            var startPos = transform.position;

            while (true)
            {
                if (CheckForPlayerBat() && !isDash())
                {
                    StartCoroutine(BatDash());
                    yield return new WaitWhile(isDash);
                }

                if (CheckForWall())
                    break;

                transform.position += new Vector3(0, k * _speed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }

            _isMovingRight = !_isMovingRight;
            StartCoroutine(Wait());
            yield return new WaitWhile(isWait);
            var distance = (transform.position - startPos).magnitude;

            for (float i = 0; i < distance; i += _speed / 100)
            {
                if (CheckForPlayerBat() && !isDash())
                {
                    StartCoroutine(BatDash());
                    distance += _speed * Time.fixedDeltaTime;
                    yield return new WaitWhile(isDash);
                }
                transform.position -= new Vector3(0, k * _speed * Time.fixedDeltaTime);
                yield return new WaitForFixedUpdate();
            }
        }

        _isMoving = false;

        bool isDash() => _isDashing;
        bool isWait() => _isWaiting;
    }

    private IEnumerator MoveDiagonal()
    {
        _isMoving = true;
        var k = -1;
        if (_isMovingRight)
            k = 1;

        if (_isDistance)
        {
            var distance = _moveDistance;

            for (float i = 0; i < distance; i += _speed / 100)
            {
                Debug.Log(CheckForPlayerBat());
                if (CheckForPlayerBat() && !isDash())
                {
                    StartCoroutine(BatDash());
                    distance += _speed * Time.fixedDeltaTime;
                    yield return new WaitWhile(isDash);
                }

                transform.position += new Vector3(k * _speed * Time.fixedDeltaTime, 0);
                yield return new WaitForFixedUpdate();
            }

            StartCoroutine(Wait());
            _isMovingRight = !_isMovingRight;
            yield return new WaitWhile(isWait);

            //Back To Center 
            for (float i = 0; i < distance; i += _speed / 100)
            {
                if (CheckForPlayerBat() && !isDash())
                {
                    StartCoroutine(BatDash());
                    distance += _speed * Time.fixedDeltaTime;
                    yield return new WaitWhile(isDash);
                }

                transform.position -= new Vector3(k * _speed * Time.fixedDeltaTime, 0);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            var startPos = transform.position;

            while (true)
            {
                if (CheckForPlayerBat() && !isDash())
                {
                    StartCoroutine(BatDash());
                    yield return new WaitWhile(isDash);
                }


                if (CheckForWallBat())
                    break;

                transform.position += new Vector3(k * _speed * Time.fixedDeltaTime, 0);
                yield return new WaitForFixedUpdate();
            }

            _isMovingRight = !_isMovingRight;
            StartCoroutine(Wait());
            yield return new WaitWhile(isWait);
            var distance = (transform.position - startPos).magnitude;

            for (float i = 0; i < distance; i += _speed / 100)
            {
                if (CheckForPlayerBat() && !isDash() && isDashingAvaible)
                {
                    StartCoroutine(BatDash());
                    distance += _speed * Time.fixedDeltaTime;
                    yield return new WaitWhile(isDash);
                }
                transform.position -= new Vector3(k * _speed * Time.fixedDeltaTime, 0);
                yield return new WaitForFixedUpdate();
            }
        }

        _isMoving = false;

        bool isDash() => _isDashing;
        bool isWait() => _isWaiting;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(transform.position.x + ((_isMovingRight) ? 0.75f : -0.75f), transform.position.y), transform.position + (float)((_isMovingRight) ? -1 : 1) * Vector3.left * _spotDistance);
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y + ((_isMovingRight) ? -0.75f : 0.75f)), transform.position + (float)((_isMovingRight) ? -1 : 1) * Vector3.up * _spotDistance);
        Gizmos.DrawLine(new Vector2(transform.position.x + ((_isMovingRight) ? 0.75f : -0.75f), transform.position.y + ((_isMovingRight) ? -0.75f : 0.75f)), transform.position + (float)((_isMovingRight) ? -1 : 1) * (Vector3.left+Vector3.up).normalized * _spotDistance);
    }

    private IEnumerator BatDash()
    {
        _isDashing = true;
        yield return new WaitForSecondsRealtime(_spottedWaitTime);

        var k = -1;
        if (_isMovingRight)
            k = 1;

        for (float i = 0; i < _dashTime; i += Time.deltaTime)
        {
            if (_moveType == BatMoveType.Horizontal)
                transform.position += new Vector3(k * _dashSpeed * Time.fixedDeltaTime, 0);
            if (_moveType == BatMoveType.Diagonal)
                transform.position += new Vector3(k * _dashSpeed * Time.fixedDeltaTime, 0);
            if (_moveType == BatMoveType.Vertical)
                transform.position -= new Vector3(0, k * _dashSpeed * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSecondsRealtime(_dashRestoreTime);
        _isDashing = false;
    }

    private IEnumerator Wait()
    {
        _isWaiting = true;
        yield return new WaitForSecondsRealtime(_timeToWait);
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        _isWaiting = false;
    }

    protected bool CheckForPlayerBat()
    {
        RaycastHit2D raycast;
        if (_moveType == BatMoveType.Horizontal)
            raycast = Physics2D.Raycast(new Vector2(transform.position.x + ((_isMovingRight) ? 0.75f : -0.75f), transform.position.y), (float)((_isMovingRight) ? -1 : 1) * Vector3.left, _spotDistance);
        if (_moveType == BatMoveType.Diagonal)
            raycast = Physics2D.Raycast(new Vector2(transform.position.x + ((_isMovingRight) ? 0.75f : -0.75f), transform.position.y + ((_isMovingRight) ? -0.75f : 0.75f)), (float)((_isMovingRight) ? -1 : 1) * (Vector3.left + Vector3.up).normalized, _spotDistance);
        if (_moveType == BatMoveType.Vertical)
            raycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + ((_isMovingRight) ? -0.75f : 0.75f)), (float)((_isMovingRight) ? -1 : 1) * Vector3.up, _spotDistance);
        else
            raycast = Physics2D.Raycast(new Vector2(transform.position.x + ((_isMovingRight) ? 0.75f : -0.75f), transform.position.y), (float)((_isMovingRight) ? -1 : 1) * Vector3.left, _spotDistance);


        if (raycast.collider != null && raycast.collider.gameObject.tag == "Player")
        {
            Debug.Log("PLAYER DETECTED!!!");
            return true;
        }
        return false;
    }

    protected bool CheckForWallBat()
    {
        RaycastHit2D raycast;
        if (_moveType == BatMoveType.Horizontal)
            raycast = Physics2D.Raycast(new Vector2(transform.position.x + ((_isMovingRight) ? 0.75f : -0.75f), transform.position.y), (float)((_isMovingRight) ? -1 : 1) * Vector3.left, _spotDistance);
        if (_moveType == BatMoveType.Diagonal)
            raycast = Physics2D.Raycast(new Vector2(transform.position.x + ((_isMovingRight) ? 0.75f : -0.75f), transform.position.y + ((_isMovingRight) ? -0.75f : 0.75f)), (float)((_isMovingRight) ? -1 : 1) * (Vector3.left + Vector3.up).normalized, _spotDistance);
        if (_moveType == BatMoveType.Vertical)
            raycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + ((_isMovingRight) ? -0.75f : 0.75f)), (float)((_isMovingRight) ? -1 : 1) * Vector3.up, _spotDistance);
        else
            raycast = Physics2D.Raycast(new Vector2(transform.position.x + ((_isMovingRight) ? 0.75f : -0.75f), transform.position.y), (float)((_isMovingRight) ? -1 : 1) * Vector3.left, _spotDistance);


        if (raycast.collider != null && raycast.collider.gameObject.layer == LayerMask.NameToLayer("DefaultTile"))
        {
            Debug.Log("Tile DETECTED!!!");
            return true;
        }
        return false;
    }
}