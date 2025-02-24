using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float health { get; private set; }
    [SerializeField] private float maxHealth;
    public UnityEvent deathEvent;
    [SerializeField] private bool destroyOnDeath = true;


    public GameObject textPrefab;
    public SpriteRenderer spriteRenderer;

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


        if (textPrefab != null)
        {
            ShowFloatingText(amount);
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

    private void ShowFloatingText(float damageDealt)
    {
        GameObject spawned = Instantiate(textPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(-45, 45)));
        TMP_Text text = spawned.GetComponent<TMP_Text>();
        text.text = Mathf.Floor(damageDealt).ToString();
        text.color = spriteRenderer.color;
        text.fontSize = Mathf.Clamp(damageDealt, 0, 75);
    }

    public virtual void Death()
    {
        deathEvent?.Invoke();

        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            WeaponPickupSpawner.instance.SpawnPickup((Vector2)transform.position);
            HealthPickupSpawner.instance.SpawnPickup((Vector2)transform.position);
        }

        if (destroyOnDeath)
            Destroy(gameObject);
    }
}
