using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private Rigidbody2D _rgbd2D;
    private BoxCollider2D _collider;
    private Animator _animator;

    [SerializeField] private AudioClip ThrustClip;
    [SerializeField] private AudioClip Steps;
    private AudioSource source;

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

    private float _horizontalMove;
    private bool jumpedLastFrame;
    private bool isDying = false;

    private void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _rgbd2D = GetComponent<Rigidbody2D>();
        _particleSystem = GetComponent<ParticleSystem>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        StartCoroutine(GameplayUI.Instance.FadeEffect(false));
        yield return new WaitUntil(Waiting);
        CurrentLevel.FindShards();
        StartCoroutine(LevelAudioManager.Instance.SoundFade(true));

        Debug.Log("SPAWN!");
        bool Waiting() => GameplayUI.Instance.isFading;
    }

    private void FixedUpdate()
    {
        if (isDying)
            return;
        Move();
    }


    private void Update()
    {
        if (isDying)
            return;
        GroundCheck();
        CheckInputs();
        Flip();
        PlayAnimations();
    }
    private void Jump()
    {
        _animator.SetBool("Jump", true);
        _isJumped = true;
        jumpedLastFrame = true;
        _isGrounded = false;
        _rgbd2D.AddForce(Vector3.up * _jumpPower / _rgbd2D.gravityScale * 100);
        //Debug.Log("JUMP");
    }

    private void PlayAnimations()
    {
        _animator.SetBool("Fall", _rgbd2D.velocity.y < -1);

    }

    private void Flip()
    {

        if (_horizontalMove > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_horizontalMove < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void CheckInputs()
    {
        _horizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && _isJumped)
        {
            _animator.SetBool("Jump", false);
            _isJumped = false;
        }

        if (Input.GetButton("Jump") && !_isJumped && !_isGrounded)
            UseThrust();

        if (Input.GetButtonDown("Jump") && _isGrounded && !_isJumped)
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !_isDashing)
            StartCoroutine(Dash(Input.GetAxisRaw("Horizontal")));

        if (!Input.GetButton("Jump"))
            JetpackThrust += _jetPackRestoringSpeed;

    }

    private void Move()
    {
        _rgbd2D.velocity = new Vector2(_movingSpeed * _horizontalMove, _rgbd2D.velocity.y);

        if (Mathf.Abs(_horizontalMove) >= 0.1f)
            _animator.SetFloat("Speed", 1);
        else
            _animator.SetFloat("Speed", 0);
    }

    private void UseThrust()
    {
        _animator.SetBool("Thrust", true);
        _isGrounded = false;
        if (_jetpackThrust <= 0)
        {
            _animator.SetBool("Thrust", false);
            _jetpackThrust = 0;
            return;
        }     
        JetpackThrust -= Time.deltaTime;
        _rgbd2D.AddForce(Vector3.up * Time.deltaTime * _jetpackPower / _rgbd2D.gravityScale * 1000);
        _particleSystem.Play();
    }

    private void GroundCheck()
    {
        if (jumpedLastFrame)
        {
            jumpedLastFrame = false;
            return;
        }

        _isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - _collider.size.x / 2 + 0.05f, transform.position.y - _collider.size.y / 2 + 0.01f),
            new Vector2(transform.position.x + _collider.size.x / 2 - 0.05f, transform.position.y - _collider.size.y / 2 - 0.05f), layer);

        if (!_isGrounded)
        {
            _animator.SetBool("Ground", false);
            return;
        }

        _isJumped = false;
        _animator.SetBool("Ground", true);
        _animator.SetBool("Jump", false);
        _animator.SetBool("Thrust", false);
        _animator.SetBool("Fall", false);
        //JetpackThrust += _jetPackRestoringSpeed;
    }

    public IEnumerator Die()
    {
        if (isDying)
            yield break;

        Debug.Log("Dead method");
        _animator.SetBool("Death", true);
        isDying = true;
        LevelAudioManager.Instance.LastPlayingTime = LevelAudioManager.Instance._musicSource.time;
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(GameplayUI.Instance.FadeEffect(true));
        yield return new WaitUntil(Waiting);
        yield return new WaitForSecondsRealtime(0.3f);
        new GameLevel(CurrentLevel.SceneID);
        //CurrentLevel.LoadLevel();

        bool Waiting() => GameplayUI.Instance.isFading;
    }

    private IEnumerator Dash(float direction)
    {

        if (direction == 0)
            yield break;

        if (JetpackThrust < _jetpackCapacity * 0.6f)
            yield break;

        _isDashing = true;
        _animator.SetTrigger("Dash");

        JetpackThrust -= _jetpackCapacity * 0.6f;

        for (float i = 0; i < _dashDuration; i++)
        {
            _rgbd2D.position += new Vector2(direction, 0) * _dashSpeed * Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSecondsRealtime(_dashTimeout);
        _isDashing = false;
        _animator.ResetTrigger("Dash");
    }
}
