using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private Rigidbody2D _rgbd2D;
    private BoxCollider2D _collider;

    public static GameLevel CurrentLevel;

    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _airMovingSpeed;

    public float JetpackThrust
    {
        get => _jetpackThrust;
        set
        {
            _jetpackThrust = (value >= _jetpackCapacity) ? _jetpackCapacity : value;
            GameplayUI.Instance.ChangeThrustSliderValue(value);
        }
    }

    public float JetpackCapacity => _jetpackCapacity;

    [SerializeField] private float _jetpackThrust;
    [SerializeField] private float _jetpackCapacity;
    [SerializeField] private float _jetPackRestoringSpeed;
    [SerializeField] private float _jetpackPower;
    [SerializeField] private float _jumpPower;

    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashTimeout;

    [SerializeField] private bool _isGrounded = true;
    [SerializeField] private bool _isJumped = false;
    [SerializeField] private bool _isDashing = false;
    [SerializeField] private LayerMask layer;

    [SerializeField] private ParticleSystem _particleSystem;

    private void Awake()
    {
        Instance = this;
        CurrentLevel.FindShards();
        _rgbd2D = GetComponent<Rigidbody2D>();
        _particleSystem = GetComponent<ParticleSystem>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Move();
        GroundCheck();
    }

    private void Jump()
    {
        _isJumped = true;
        _isGrounded = false;

        _rgbd2D.AddForce(Vector3.up * _jumpPower / _rgbd2D.gravityScale * 100); 
        Debug.Log("JUMP");
    }

    public void Move()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
            Jump();

        else if (Input.GetButton("Jump") && !_isGrounded)
            UseThrust();

        if (Input.GetKeyDown(KeyCode.V) && !_isDashing)
            StartCoroutine(Dash(Input.GetAxisRaw("Horizontal")));

        if (Input.GetAxis("Horizontal") != 0)
            _rgbd2D.position = (_rgbd2D.position + new Vector2(_movingSpeed * Time.deltaTime * Input.GetAxis("Horizontal"),0));
    }

    private void UseThrust()
    {
        _isGrounded = false;
        if (_jetpackThrust <= 0)
        {
            // TODO: Logic for no trt situation
            _jetpackThrust = 0;
            return;
        }
        JetpackThrust -= Time.deltaTime;
        _rgbd2D.AddForce(Vector3.up * Time.deltaTime * _jetpackPower / _rgbd2D.gravityScale * 1000);
        _particleSystem.Play();
    }

    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - _collider.size.x / 2 + 0.05f, transform.position.y - _collider.size.y / 2 - 0.01f), 
            new Vector2(transform.position.x + _collider.size.x / 2 - 0.05f, transform.position.y - _collider.size.y / 2), layer);

        if (!_isGrounded)
            return;

        _isJumped = false;
        JetpackThrust += _jetPackRestoringSpeed;
    }

    private IEnumerator Dash(float direction)
    {
        if (direction == 0)
            yield break;

        _isDashing = true;

        for (float i = 0; i<_dashDuration; i++)
        {
            _rgbd2D.position += new Vector2(direction, 0) * _dashSpeed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSecondsRealtime(_dashTimeout);
        _isDashing = false;
    }
}
