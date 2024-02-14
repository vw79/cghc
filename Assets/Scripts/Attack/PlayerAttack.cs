using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool isAttacking;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    private Animator animator;
    private PlayerMovement playerMovement;
    private PlayerHealthSystem playerHealth;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealthSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {             
            Attack();
            isAttacking = true;
        }
    }

    void Attack()
    {
        if (!playerHealth.isDead)
        {
            playerMovement.enabled = false;
            StartCoroutine(WaitAttack());
            StartCoroutine(WaitAnim());
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(0.47f);
        playerMovement.enabled = true;
        isAttacking = false;
    }

    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(0.2f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<Enemy_Health>().Die();
        }
    }
}
