using UnityEngine;
using UnityEngine.UI;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] private float range;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Parameters")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private Animator anim;
    private Healthbar playerHealth;
    [SerializeField] private Slider healthSlider;


    private PatrolEnemy patrolEnemyy;

    private void Awake()
    {
        if (healthSlider != null)
            healthSlider.maxValue = maxHealth;

        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        patrolEnemyy = GetComponentInParent<PatrolEnemy>();
    }

    void Start()
    {
        GameManager.Instance.RegisterEnemy();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        bool playerDetected = PlayerInsight();

        if (playerDetected)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                Debug.Log("Attack triggered");
                anim.SetTrigger("MeleeAttack");
            }
        }

        if (patrolEnemyy != null)
            patrolEnemyy.enabled = !playerDetected;
    }


    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        if(hit.collider != null)
            playerHealth = hit.transform.GetComponent<Healthbar>();
        
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInsight())
        playerHealth.TakeDamage(damage);        
    }

    public void TakeDamage(int damage)
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth;

        currentHealth -= damage;
        Debug.Log("Enemy hit! Remaining health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        GameManager.Instance.EnemyDied();
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}