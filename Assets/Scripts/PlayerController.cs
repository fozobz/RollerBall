using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
 // Rigidbody of the player.
 private Rigidbody rb;

 private int jumpsLeft = 2;

 private bool braking = false;

 private bool onGround = true;

 // Variable to keep track of collected "PickUp" objects.
 private int count;

 // Movement along X and Y axes.
 private float movementX;
 private float movementY;

 // Speed at which the player moves.
 public float speed = 0;

 public int winCount = 0;

public float jumpForce = 0;


 // UI text component to display count of "PickUp" objects collected.
 public TextMeshProUGUI countText;

 // UI object to display winning text.
 public GameObject winTextObject;

 // Start is called before the first frame update.
 void Start()
    {
 // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();

 // Initialize count to zero.
        count = 0;

 // Update the count display.
        SetCountText();

 // Initially set the win text to be inactive.
        winTextObject.SetActive(false);
    }
 
 // This function is called when a move input is detected.
 void OnMove(InputValue movementValue)
    {
 // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

 // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

 // FixedUpdate is called once per fixed frame-rate frame.
 private void Update() 
    {
 // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        Vector3 jump = new Vector3 (0, jumpForce, 0);


 // Apply force to the Rigidbody to move the player.


              if(Input.GetKey(KeyCode.F) && onGround == true){
                     Debug.Log("braking!");
                     braking = true;
                     Vector3 newVelocity = new Vector3 (rb.linearVelocity.x * -0.3f, 0, rb.linearVelocity.z * -0.3f);
                     //rb.linearVelocity = newVelocity;
                     rb.AddForce(newVelocity);

              }else {
                     braking = false;
              }

               if(braking != true && onGround == true){
                     rb.AddForce(movement * speed);
               }



              if(Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0 && braking != true){
                     Debug.Log("jumping!");
                      rb.AddForce(jump);
                      jumpsLeft -= 1;

              }
        
    }

private void OnCollisionEnter (Collision collision)
{
       if (collision.gameObject.tag == "Ground" )
       {
              Debug.Log ("enter");
             jumpsLeft = 2;
             onGround = true;

       }
}

private void OnCollisionExit (Collision collision)
{
       if (collision.gameObject.tag == "Ground" )
       {
              Debug.Log ("exiting");
              jumpsLeft = 1;
              onGround = false;

       }
}

 
 void OnTriggerEnter(Collider other) 
    {
 // Check if the object the player collided with has the "PickUp" tag.
 if (other.gameObject.CompareTag("PickUp")) 
        {
 // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);

 // Increment the count of "PickUp" objects collected.
            count = count + 1;

 // Update the count display.
            SetCountText();
        }
    }

 // Function to update the displayed count of "PickUp" objects collected.
 void SetCountText() 
    {
 // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

 // Check if the count has reached or exceeded the win condition.
 if (count >= winCount)
        {
 // Display the win text.
            winTextObject.SetActive(true);
        }
    }
}