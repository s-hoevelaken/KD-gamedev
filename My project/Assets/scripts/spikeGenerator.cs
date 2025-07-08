using UnityEngine;

public class spikeGenerator : MonoBehaviour
{
    public GameObject spike;

    public float MinSpeed;
    public float MaxSpeed;
    public float currentSpeed;

    public float speedMultiplier;

    void Awake()
    {
        currentSpeed = MinSpeed;
        generateSpike();
    }

    public void GenerateNextSpikeTimer()
    {
        float waitTime = Random.Range(0.721f, 1.2f);
        Invoke("generateSpike", waitTime);
    }

    void generateSpike()
    {
        GameObject SpikeIns = Instantiate(spike, transform.position, transform.rotation);
        SpikeIns.tag = "Spike"; 
        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpeed < MaxSpeed)
        {
            currentSpeed += speedMultiplier;
        }
    }
}
