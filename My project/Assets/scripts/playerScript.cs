using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerScript : MonoBehaviour
{
    public float jumpForce = 10f;
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private bool isAlive = false;
    float score;
    public TMP_Text Scoretext;
    private Rigidbody2D rb;


void Start()
{
    isAlive = true;

    if (Scoretext == null)
    {
        Debug.LogError("Scoretext is NOT assigned!");
    }
    else
    {
        Debug.Log("Scoretext is: " + Scoretext.name);
    }
}

    private void Awake()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    
        // Jump when Space is pressed and the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
        if(isAlive)
        {
            score += Time.deltaTime * 4;
            Scoretext.text = "SCORE : " + ((int)score).ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Set grounded when touching an object tagged "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            isAlive = false;
            Time.timeScale = 0;
        }
    }
}
