using UnityEngine;

public class spikeGenerator : MonoBehaviour
{
    public GameObject spike;

    // speed settings for how fast spikes move
    public float MinSpeed;
    public float MaxSpeed;
    public float currentSpeed;

    public float speedMultiplier;

    void Awake()
    {
        // set starting speed and spawn the first spike
        currentSpeed = MinSpeed;
        generateSpike();
    }

    // called from SpikeScript when it hits nextLine
    public void GenerateNextSpikeTimer()
    {
        // wait a random time then spawn a spike
        float waitTime = Random.Range(0.278f, 0.6f);
        Invoke("generateSpike", waitTime);
    }

    void generateSpike()
    {
        // create a new spike on gen
        GameObject SpikeIns = Instantiate(spike, transform.position, transform.rotation);
        SpikeIns.tag = "Spike";
        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;

        // make spike different height
        float randomHeight = Random.Range(1.0f, 2.5f);
        Vector3 scale = SpikeIns.transform.localScale;
        SpikeIns.transform.localScale = new Vector3(scale.x, randomHeight, scale.z);

        // make sure the bottom of the spike touches the ground, this is needed because of the random spike height was making it float in the air
        float groundY = -4.7705f;
        float spikeHeight = SpikeIns.GetComponent<SpriteRenderer>().bounds.size.y;
        float adjustedY = groundY + spikeHeight / 2f;

        // use new Y position
        SpikeIns.transform.position = new Vector3(transform.position.x, adjustedY, transform.position.z);
    }

    void Update()
    {
        // slowly increase the speed until max
        if (currentSpeed < MaxSpeed)
        {
            currentSpeed += speedMultiplier;
        }
    }
}
