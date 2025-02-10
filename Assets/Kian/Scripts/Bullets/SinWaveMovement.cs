using UnityEngine;

public class SinWaveMovement : Bullet
{
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;

    private float timer;

    private Vector2 currentSinDifference;

    private void Update()
    {
        timer += Time.deltaTime;
        Vector2 originalPos = (Vector2)transform.position - currentSinDifference;

        transform.position = originalPos + new Vector2(transform.right.x * Mathf.Sin(timer * frequency) * amplitude, transform.right.y * Mathf.Sin(timer * frequency) * amplitude);
    }

    public override void SetInUse(GameObject parentObject)
    {
        base.SetInUse(parentObject);

        timer += Random.Range(0.1f, 0.3f);
    }

    public override void EndUsage()
    {
        base.EndUsage();

        timer = 0;
    }
}
