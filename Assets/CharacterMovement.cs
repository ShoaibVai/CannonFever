using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import Unity UI namespace

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control the movement speed
    private Rigidbody2D rb; // Rigidbody component for physics interactions
    private bool gameIsPaused = false; // Flag to track game pause state

    public Text gameOverText; // Reference to the Text component displaying "Game Over"
    public Text restartText; // Reference to the Text component displaying "Press 'Space' to restart"

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody component
        gameOverText.gameObject.SetActive(false); // Initially hide the "Game Over" text
        restartText.gameObject.SetActive(false); // Initially hide the "Press 'Space' to restart" text
    }

    void Update()
    {
        if (gameIsPaused) // Check if the game is paused
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Check if Space key is pressed
            {
                // Restart the game
                RestartGame();
            }
        }
        else // If the game is not paused
        {
            // Input handling for movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate the movement direction based on input
            Vector2 movement = new Vector2(horizontalInput, verticalInput);

            // Normalize the movement vector to prevent faster diagonal movement
            movement.Normalize();

            // Move the player
            rb.linearVelocity = movement * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bomb"))
        {
            // Display "Game Over" message
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over";

            // Display "Press 'Space' to restart" message
            restartText.gameObject.SetActive(true);
            restartText.text = "Press 'Space' to restart";

            // Pause the game
            gameIsPaused = true;
        }
    }

    void RestartGame()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
