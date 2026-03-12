using UnityEngine;

public class DestructibleWall : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public ParticleSystem destroyEffect;

    public Renderer wallRenderer;

    public Material crackMaterial;
    public Material dissolveMaterial;

    private bool isDissolving = false;
    private float dissolveValue = -1f;

    void Start()
    {
        currentHealth = maxHealth;

        wallRenderer.material = crackMaterial;
    }

    void Update()
    {
        if (isDissolving)
        {
            dissolveValue += 0.01f;

            wallRenderer.material.SetFloat("_dissolvepercent", dissolveValue);

            if (dissolveValue >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.gameObject.CompareTag("bullet"))
        {
            Debug.Log("collision nullet");
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        Debug.Log("Wall hit!");
        if (isDissolving) return;

        currentHealth -= damage;

        UpdateCracks();

        if (currentHealth <= 0)
        {
            StartDissolve();
        }
    }

    void UpdateCracks()
    {
        float damageAmount = 1f - ((float)currentHealth / maxHealth);

        wallRenderer.material.SetFloat("_CrackAmount", damageAmount);
    }

    void StartDissolve()
    {
        if (destroyEffect != null)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
        }

        wallRenderer.material = dissolveMaterial;

        dissolveValue = -1f;

        isDissolving = true;
    }
}