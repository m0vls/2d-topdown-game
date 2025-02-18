using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Params")]
    public Transform player;
    public float speed = 4;
    public int damage = 20;
    public int maxHealth = 50;
    public int currentHealth;
    
    private Rigidbody2D rb;
    private Vector2 movement;

    public event Action<GameObject> OnDeath; 
    private void Start()
    {
        currentHealth = maxHealth;
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        
        Vector3 direction = new Vector3(player.position.x - transform.position.x, player.position.y - transform.position.y - 0.5f, 0);
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate()
    {
        MoveChar(movement);
    }

    private void MoveChar(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + direction * (speed * Time.fixedDeltaTime));
    }
    
    public void TakeDamage(int outDamage)
    {
        currentHealth -= outDamage;
        if (currentHealth <= 0)
        {
            Die();
            SaveSystem.Instance.kills++;
        }
    }

    private void Die()
    {
        OnDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            var p = other.collider.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamage(damage);
            }
        }
    }
}
