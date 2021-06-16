using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    // Configurations 
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    public Joystick joystick;

    float controlThrow = 0f;
    
    //States 
    bool isAlive = true; //Player is alive

    // Cached component references 
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D BodyCollider2D; // Capsule collider for player body
    BoxCollider2D Feet;

    // Message then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        BodyCollider2D = GetComponent<CapsuleCollider2D>();
        Feet = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.touches[i].position);

        }*/
        
        Run();
        Jump();
        FlipSprite();
    }
    private void Run()
    {
        //float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        controlThrow = joystick.Horizontal; 
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        

        // Player always moving at running speed  
        bool playerMovingHoriz = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("RUnning", playerMovingHoriz);
    }

    private void Jump() {
        // Makes sure player can only jump when they are touching the ground layer
        if (!Feet.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        float verticalMove = joystick.Vertical;
        // Lets the player jump by changing y velocity 
        //if (CrossPlatformInputManager.GetButtonDown("Jump")) {
        if (verticalMove >= .5f ) { 
            Vector2 jumpVelocityAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity = jumpVelocityAdd;
        }
    }
    private void FlipSprite() {
        // if player is moving horizontally 
        bool playerMovingHoriz = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerMovingHoriz)
        {
            // Vector2 becomes +1 or -1 depending on the sign of the movement, y stays the same 
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        
        }

    }

}
