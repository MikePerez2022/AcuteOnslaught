using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationRate;


    private float currentRotation;
    // Update is called once per frame
    void Update()
    {
        currentRotation += rotationRate * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
    }
}
