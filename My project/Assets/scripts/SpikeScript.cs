using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public spikeGenerator spikeGenerator;

    void Update()
    {
        // move the spike to the left using the current speed from spike generator
        transform.Translate(Vector2.left * spikeGenerator.currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when spike hits nextLine, spawn next 
        if (collision.gameObject.CompareTag("nextLine"))
        {
            spikeGenerator.GenerateNextSpikeTimer();
        }

        // when spike hits Finish line on left delete it
        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(this.gameObject);
        }
    }
}
