using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float cooldown = .5f;
    private float attackTimer = 0f;
    public float attackRange = 3f;
    private bool isActive = false;
    public float attackDamage = 2f;

    public float HP = 5;

    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < attackRange)
        {
            Activate();
        }


        if (isActive && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * (moveSpeed * Time.deltaTime));
        }


        attackTimer -= Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        // 攻击范围（红色）
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, transform.lossyScale.x * collider.radius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void Attack()
    {
        PlayerController PC = player.GetComponent<PlayerController>();
        if (PC != null)
        {
            PC.GetDamage(attackDamage);
        }

        Debug.Log($"Enemy Attack");
    }

    public void Activate()
    {
        isActive = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive && attackTimer <= 0f)
        {
            Attack();
            attackTimer = cooldown;
        }
    }
}