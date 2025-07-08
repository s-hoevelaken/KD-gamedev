using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    // how high the player jumps when pressing space
    public float jumpForce = 10f;

    // Keep track of whether the player can jump or is alive
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isAlive = false;

    private float score;
    public TMP_Text Scoretext;

    // UI elements
    public GameObject winScreen;
    public GameObject startScreen;
    public GameObject poweredBy;
    public GameObject slogan;

    public TMP_Text startScreenText;
    public TMP_Text sloganText;
    public TMP_Text poweredByText;

    private bool isGameOver = false;

    public float minWinScore = 45f;
    public float maxWinScore = 90f;
    private float winScore;

    private Rigidbody2D rb;

    private bool gameStarted = false;

    void Awake()
    {
        // rigidbody2D component for jump forces
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isAlive = false;
        gameStarted = false;

        //random score to win the game
        winScore = Random.Range(minWinScore, maxWinScore);

        // Set the start screen and branding messages
        startScreenText.text = "Press SPACE to start!";
        poweredByText.text = "Powered by Best Education";
        sloganText.text = "Wij lanceren je de toekomst in!";

        // Show start screen and branding stuff
        startScreen.SetActive(true);
        poweredBy.SetActive(true);
        slogan.SetActive(true);

        // hide the win screen.
        winScreen.SetActive(false);

        // Pause everything until start
        Time.timeScale = 0f;
    }

    void Update()
    {
        //  start if its not started and the player presses space
        if (!gameStarted && !isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;
            isAlive = true;
            Time.timeScale = 1f; // unpause game
            startScreen.SetActive(false);
            poweredBy.SetActive(false);
            slogan.SetActive(false);
        }

        // If the player has died and presses space restart game
        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f; // unpause game
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // check if player is on ground so he can jump if true 
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }

        // Increase score and check if player passed win score
        if (isAlive)
        {
            score += Time.deltaTime * 4;
            Scoretext.text = "SCORE : " + ((int)score) + "\n TARGET : " + ((int)winScore);

            if (score >= winScore)
            {
                // Show win screen, dont pause the game as player should be able to get as high as possible even if goal is reached
                startScreenText.text = "You Won!";
                startScreen.SetActive(true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // we have a var for if player hits grouded to make it easier to check if he can jump
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // end game if spike is hit
        if (collision.gameObject.CompareTag("Spike"))
        {
            isGameOver = true;
            isAlive = false;
            Time.timeScale = 0;

            if (score >= winScore)
            {
                startScreenText.text = "You Won! Play Again?\nPress SPACE to restart!";
            }
            else
            {
                startScreenText.text = "You Lost! Try Again\nPress SPACE to restart!";
            }

            // Show the restart screen
            startScreen.SetActive(true);
            gameStarted = false;
        }
    }
}
