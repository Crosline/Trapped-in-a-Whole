using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCrosline : MonoBehaviour {


    [Header("Jumping Options")]
    private bool isJumping;
    private bool jump;
    public float jetPackTime;
    public float jetPackForce;
    public float jetPackFuelRate;
    private float jetpackTimeCounter;
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f;
    public float lowKeyMultiplier = 1f;


    public Animator anime;

    [Header("Movement Options")]

    private bool isDashing;
    public float dashForce;

    [Header("Player Options")]
    public float health = 3;

    public float playerSpeed = 8f;
    public Rigidbody2D rb;

    private bool isGrounded = true;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsTile;

    private float moveHorizontal;


    [Header("Friction Options")]
    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D withFriction;

    void Start() {
        jetpackTimeCounter = jetPackTime;
    }

    void Update() {

        InputManagment();
        Ground();
    }

    void FixedUpdate() {

        Jump();
        Move();
        Flip();
        Dash();

    }



    #region Movement
    //private bool allowedToMove = true;
    private void Move() {
        rb.velocity = new Vector2(moveHorizontal, rb.velocity.y);





    }

    #region MovementDetails


    private void Ground() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsTile);
    }

    private void Flip() {

        if (moveHorizontal > 0) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        } else if (moveHorizontal < 0) {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }


    private void Dash() {
        if(isDashing && jetpackTimeCounter > jetPackTime * 2 / 5 && (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))) {
            anime.SetTrigger("Dash");
            rb.velocity *= dashForce; //+= Vector2.right * rb.velocity.x *
            jetpackTimeCounter -= jetPackTime * 2 / 5;
            jetpackTimeCounter = Mathf.Clamp(jetpackTimeCounter, 0, 100);
            isDashing = false;
            anime.ResetTrigger("Dash");
        }
    }


    IEnumerator IDash() {
        if (Input.GetButton("Jump") && !isGrounded && jetpackTimeCounter > 0) {
            anime.SetBool("Thrust", true);
            yield return new WaitForFixedUpdate();
            if (rb.velocity.y < 0 || rb.velocity.y < jumpForce) {
                rb.velocity = Vector2.up * jetPackForce * Time.fixedDeltaTime + Vector2.right * rb.velocity.x;
            } else
                rb.velocity = Vector2.up * jetPackForce * Time.fixedDeltaTime + Vector2.right * rb.velocity.x;
            jetpackTimeCounter -= Time.fixedDeltaTime * jetPackFuelRate;
        } else if (jetpackTimeCounter < 0)
            anime.SetBool("Thrust", false);
    }


    private void Jump() {

        if (rb.velocity.y < 0)
            anime.SetBool("Fall", true); //fall animation here*/


        //after jump lowkey and fallkeymodifiers
        if (rb.velocity.y < 0 && isJumping) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        if (rb.velocity.y > 0 && !isJumping) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowKeyMultiplier - 1) * Time.fixedDeltaTime;
        }



        if (jump) {
            anime.SetBool("Jump", true);
            jump = false;
            rb.velocity = Vector2.up * jumpForce + Vector2.right * rb.velocity.x;
            isJumping = true;
        }

        StartCoroutine(IDash());

        /*if (Input.GetButton("Jump") && !isGrounded && jetpackTimeCounter > 0) {
            anime.SetBool("Thrust", true);
            if (rb.velocity.y < 0 || rb.velocity.y < jumpForce) {
                rb.velocity = Vector2.up * jetPackForce * Time.fixedDeltaTime + Vector2.right * rb.velocity.x;
            }else 
                rb.velocity = Vector2.up * jetPackForce * Time.fixedDeltaTime + Vector2.right * rb.velocity.x;
            jetpackTimeCounter -= Time.fixedDeltaTime * jetPackFuelRate;
        } else if (jetpackTimeCounter < 0)
            anime.SetBool("Thrust", false);*/

        if (isGrounded) {

            if (jetpackTimeCounter < jetPackTime) {
                Debug.Log("Hello");
                isJumping = false;
                anime.SetBool("Jump", false);
                anime.SetBool("Thrust", false);
                jetpackTimeCounter += Time.fixedDeltaTime * jetPackFuelRate;
            }

            anime.SetBool("Ground", true);
            anime.SetBool("Fall", false);
        } else
            anime.SetBool("Ground", false);

    }

    #endregion

    #endregion

    #region Input
    private void InputManagment() {


        SimpleMove();
        JumpInput();
        DashInput();



    }


    private void SimpleMove() {

        moveHorizontal = Input.GetAxis("Horizontal") * playerSpeed;

        
        if (moveHorizontal != 0) {

            rb.sharedMaterial = noFriction;
            // animation speed 1
            anime.SetFloat("Speed", 1);

        } else {
            //animation speed 0
            anime.SetFloat("Speed", 0);
            rb.sharedMaterial = withFriction;
        }
    }


    private void DashInput() {
        if (Input.GetButtonDown("Fire1")) {
            isDashing = true;
        }

    }

    private void JumpInput() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            //animation jump
            jump = true;
        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);

    }
    #endregion

}
