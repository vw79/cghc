using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public bool isAttacking = false;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;

    private Animator animator;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {   
            
            Attack();
        }
    }

    void Attack()
    {
        playerMovement.RB.velocity = new Vector2(0, playerMovement.RB.velocity.y);
        playerMovement.enabled = false;
        animator.Play("Attack");
        StartCoroutine(WaitAnim());

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
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
        yield return new WaitForSeconds(0.6f);
        playerMovement.enabled = true;
    }
}
