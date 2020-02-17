using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rgbd2D;
    private BoxCollider2D _collider;

    [SerializeField] private float _movingSpeed;
    [SerializeField] private float _airMovingSpeed;
    [SerializeField] private float _jetPackCapacity;
    [SerializeField] private float _thrustAmmount;

    [SerializeField] private float _jetPackRestoringSpeed;
    [SerializeField] private float _dashTimeout;

    private bool _isGrounded = true;
    private bool _isJumped = false;

    private void Awake()
    {
        _rgbd2D = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
    }

    private void Jump()
    {
        _isGrounded = false;
        _isJumped = true;
        _rgbd2D.AddForce(Vector3.up * Time.deltaTime / _rgbd2D.gravityScale * 10000); // / _rgbd2D.gravityScale 
        Debug.Log("JUMP");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided!");
    }


    public void Move()
    {
        if (Input.GetButtonDown("Jump") && !_isJumped)
            Jump();

        else if (Input.GetButton("Jump") && _isJumped)
            UseThrust();

        if (Input.GetAxis("Horizontal") != 0)
            transform.position = (transform.position + new Vector3(_movingSpeed * Time.deltaTime * Input.GetAxis("Horizontal"),0));
    }

    private void UseThrust()
    {
        if (_thrustAmmount <= 0)
        {
            // TODO: Logic for no trust situation
            _thrustAmmount = 0;
            return;
        }
        _thrustAmmount -= Time.deltaTime;
        _rgbd2D.AddForce(Vector3.up * Time.deltaTime / _rgbd2D.gravityScale * 1000);
    }

    private void Dash()
    {

    }
}
