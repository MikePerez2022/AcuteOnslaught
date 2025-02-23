using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 launchForceMinMax;


    private float destroyCounter;
    [SerializeField] private AnimationCurve sizeScalar;

    private void Start()
    {
        Vector2 dir = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
        rb.AddForce(new Vector2(dir.x * Random.Range(launchForceMinMax.x, launchForceMinMax.y), dir.y * Random.Range(launchForceMinMax.x, launchForceMinMax.y)), ForceMode2D.Force);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
        destroyCounter += Time.deltaTime;
        float currentScale = sizeScalar.Evaluate(destroyCounter / destroyTime);
        transform.localScale = new Vector3(currentScale, currentScale, 1);


        if (destroyCounter > destroyTime)
        {
            Destroy(gameObject);
        }
    }

}
