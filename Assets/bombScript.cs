using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control movement speed
    public float selfDestructTime = 4.5f; // Time before the bomb self-destructs
    public float jumpPower = 10f; // Power of the jump

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    private GameObject protagonist; // Reference to the protagonist object

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, selfDestructTime); // Self-destruct after selfDestructTime seconds
        
        // Find the protagonist object by name
        protagonist = GameObject.Find("protagonist");

        // Calculate the initial direction towards the protagonist
        if (protagonist != null)
        {
            moveDirection = (protagonist.transform.position - transform.position).normalized;
        }
        else
        {
            // If protagonist is not found, move randomly
            moveDirection = Random.insideUnitCircle.normalized;
        }
    }

    void Update()
    {
        MoveBomb();
    }

    void MoveBomb()
    {
        // Move the bomb in the calculated direction
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    // OnCollisionEnter2D is called when this collider/rigidbody has begun touching another rigidbody/collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bomb collided with any of the walls
        if (collision.gameObject.CompareTag("wall"))
        {
            // Reflect the bomb's direction upon collision with the wall
            moveDirection = Vector2.Reflect(moveDirection, collision.contacts[0].normal);
        }
    }
}
