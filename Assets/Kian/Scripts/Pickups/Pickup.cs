using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField, Min(0)] private Vector2 launchForceMinMax;

    private void Start()
    {
        Vector2 dir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        rb.AddForce(new Vector2(dir.x * Random.Range(launchForceMinMax.x, launchForceMinMax.y), dir.y * Random.Range(launchForceMinMax.x, launchForceMinMax.y)), ForceMode2D.Force);

    }


}
