using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 3f;
    public float damage = 2;

    void Start()
    {
        Destroy(gameObject, lifeTime); // 防止飞太远
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyController>())
        {
            other.GetComponent<EnemyController>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}