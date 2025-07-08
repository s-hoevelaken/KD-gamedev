using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour
{
    public float jumpForce = 10f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isAlive = false;

    private float score;
    public TMP_Text Scoretext;
    public GameObject winScreen;
    public GameObject startScreen;
    public GameObject poweredBy;
    public TMP_Text startScreenText;
    public TMP_Text poweredByText;
    private bool isGameOver = false;

    public float minWinScore = 40f;
    public float maxWinScore = 80f;
    private float winScore;

    private Rigidbody2D rb;
    private bool gameStarted = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isAlive = false;
        gameStarted = false;
        winScore = Random.Range(minWinScore, maxWinScore);

        startScreenText.text = "Press SPACE to start!";
        poweredByText.text = "Powered by Best Education";
        startScreen.SetActive(true);
        poweredBy.SetActive(true);
        winScreen.SetActive(false);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!gameStarted && !isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;
            isAlive = true;
            Time.timeScale = 1f;
            startScreen.SetActive(false);
            poweredBy.SetActive(false); 
        }

        if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Handle jumping
        if (Input.GetKeyDown(KeyCode.Space) && isAlive)
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }

        // Scoring + win condition
        if (isAlive)
        {
            score += Time.deltaTime * 4;
            Scoretext.text = "SCORE : " + ((int)score) + "\n TARGET : " + ((int)winScore);

            if (score >= winScore)
            {
                startScreenText.text = "You Won!";
                startScreen.SetActive(true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            isGameOver = true;
            if (score >= winScore)
            {
                startScreenText.text = "You Won Play Again!\nPress SPACE to restart!";
            }
            else
            {
                startScreenText.text = "You Lost! Try Again\nPress SPACE to restart!";
            }
            isAlive = false;
            Time.timeScale = 0;

            startScreen.SetActive(true);
            gameStarted = false;
        }
    }
}
