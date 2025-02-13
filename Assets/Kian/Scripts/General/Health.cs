using UnityEditor.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float health { get; private set; }

    public virtual void DealDamage(float amount)
    {
        health -= amount;
        if (this.CompareTag("Player"))
        {
            ScoreManager.instance.multiplier = 1f;
        }

        if (health <= 0)
        {
            if (this.GetComponent<AIScoring>() != null)
            {
                ScoreManager.instance.multiplier += 0.1f;
                ScoreManager.instance.ChangeScore(this.GetComponent<AIScoring>().score);
            }
            Death();
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
