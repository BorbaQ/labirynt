using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AiNav_NC : MonoBehaviour
{
    public Transform[] points;

    public float agroRange = 5f;
    public float speedBoost = 10f;
    public float agroFillTime = 2f;
    public float agroLoseTime = 10f;
    public Image agroIcon;

    public float attackRange = 1.8f;
    public float attackCooldown = 1.5f;
    public int damage = 10;

    public int health = 10;

    public Animation anim;

    public AnimationClip idleAnim;
    public AnimationClip walkAnim;
    public AnimationClip runAnim;
    public AnimationClip attackAnim;

    private string currentAnim;
    private float lastAttackTime = 0f;

    private int currentIndex = 0;
    private NavMeshAgent agent;
    private Transform currentTarget;
    private float baseSpeed;
    private Transform player;
    private float agro = 0f;
    private bool chase = false;
    private bool dead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentTarget = points[0];
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.SetDestination(currentTarget.position);
        baseSpeed = agent.speed;

        PlayAnim(idleAnim);
    }

    void Update()
    {
        if (dead) return;

        CheckAgro();

        if (chase)
            Chase();
        else
            Patrol();
    }

    void Patrol()
    {
        PlayAnim(walkAnim);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentIndex = (currentIndex + 1) % points.Length;
            currentTarget = points[currentIndex];
            agent.SetDestination(currentTarget.position);
        }

        if (agro >= 0.8f)
        {
            chase = true;
            agent.speed = baseSpeed + speedBoost;
        }
    }

    void CheckAgro()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= agroRange)
            agro += Time.deltaTime / agroFillTime;
        else
            agro -= Time.deltaTime / agroLoseTime;

        agro = Mathf.Clamp01(agro);
        agroIcon.fillAmount = agro;
    }

    void Chase()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            agent.isStopped = true;
            Attack();
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            PlayAnim(runAnim);
        }

        if (agro <= 0f)
        {
            agent.speed = baseSpeed;
            currentIndex = GetClosestPatrolPoint();
            agent.SetDestination(points[currentIndex].position);
            chase = false;
        }
    }

    void Attack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;

            PlayAnim(attackAnim);

            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damage);
        }
    }

    void TakeDamage(int dmg)
    {
        if (dead) return;

        health -= dmg;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        dead = true;
        agent.isStopped = true;

        Destroy(gameObject, 0.2f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet"))
        {
            TakeDamage(1);
        }
    }

    void PlayAnim(AnimationClip clip)
    {
        if (currentAnim == clip.name) return;

        anim.CrossFade(clip.name);
        currentAnim = clip.name;
    }

    int GetClosestPatrolPoint()
    {
        int closestIndex = 0;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < points.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, points[i].position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        return closestIndex;
    }
}