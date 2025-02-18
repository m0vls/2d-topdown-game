using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Player Parameters")]
    public int health = 100;
    public float moveSpeed = 6f;
    public PauseMenu pauseMenu;
    
    [HideInInspector]
    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Collider2D col;
    
    [HideInInspector]
    public bool canMove = true;

    
    private SpriteRenderer sprRen;
    private bool isCooldown;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprRen = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        
    }
    
    private void Update()
    {
        if (!canMove) return;
        
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical"));
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            rb.MovePosition(rb.position + movement.normalized * (moveSpeed * Time.fixedDeltaTime));
        }
        
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(TemporaryInvincibility());
        if (health <= 0)
        {
            SaveSystem.Instance.deaths++;
            SceneManager.LoadScene("GameOver");
        }
    }
    
    private IEnumerator TemporaryInvincibility()
    {
        // Отключаем коллайдер и меняем цвет спрайта на красный
        col.enabled = false;
        sprRen.color = Color.red;

        // Ждём 3 секунды
        yield return new WaitForSeconds(3f);

        // Включаем коллайдер и возвращаем исходный цвет спрайта
        col.enabled = true;
        sprRen.color = Color.white;
    }

}
