using UnityEngine;

public class BubbleTrail : MonoBehaviour
{
    public ParticleSystem bubbles;
    public float distance = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbles.gameObject.transform.localScale = new Vector3(distance, 0, 0);
        bubbles.gameObject.transform.localPosition = new Vector3 (0, 0, distance);
        var rate = bubbles.main.maxParticles;
        rate = (int) distance * 10;
        Debug.Log(distance + " " +  bubbles.main.maxParticles);

    }
}
