using UnityEngine;

public class Collectble : MonoBehaviour
{

    [SerializeField] Transform collectble;
    [SerializeField] float coinHeight = 1.0f;
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] float movementAmplitude = .5f;
    [SerializeField] float frequecy = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collectble.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

        float Y = movementAmplitude * Mathf.Sin(frequecy * Time.time);

        collectble.localPosition = new Vector3(0f, coinHeight + Y, 0f);

    }
}
