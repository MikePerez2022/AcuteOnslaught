using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float health { get; private set; }
    [SerializeField] private float maxHealth;
    public UnityEvent deathEvent;
    [SerializeField] private bool destroyOnDeath = true;

    private void Awake()
    {
        health = maxHealth;
    }

    public virtual void GainHealth(float amount)
    {
        health += amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public virtual void DealDamage(float amount)
    {
        health -= amount;
        if (CompareTag("Player"))
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
        deathEvent?.Invoke();

        if (destroyOnDeath)
            Destroy(gameObject);
    }
}
