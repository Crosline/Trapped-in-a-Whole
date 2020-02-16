using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rgbd2D;

    [SerializeField] private float _movingSpeed;
    [SerializeField] private int _jetPackCapacity = 5;
    [SerializeField] private int _thrustAmmount = 5;

    private bool _isGrounded;


    private void Awake()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void Jump()
    {

    }

    public void Move()
    {

    }

    public void UseThrust()
    {
        if (_thrustAmmount == 0)
        {
            // TODO: Logic for no trust situation
            return;
        }
        
        _thrustAmmount--;

    }
}
