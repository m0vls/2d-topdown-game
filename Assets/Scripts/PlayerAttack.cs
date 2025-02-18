using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    
    public Transform attackPos;
    public LayerMask enemyLayer;
    public float attackRange = 0.5f;

    public int damage = 25;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public Player player;
    
    private void Update()
    {
        UpdateAttackPosition();
        
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            } 
        }
    }
    private void UpdateAttackPosition()
    {
        // Берём направление из анимации
        float horizontal = player.animator.GetFloat("LastHorizontal");
        float vertical = player.animator.GetFloat("LastVertical");

        // Определяем смещение для каждой стороны
        Vector3 attackOffset = Vector3.zero;

        if (horizontal > 0) // Направо
        {
            attackOffset = new Vector3(0.7f, -0.3f, 0f); // Смещение чуть вправо и вниз
        }
        else if (horizontal < 0) // Налево
        {
            attackOffset = new Vector3(-0.7f, -0.3f, 0f); // Смещение чуть влево и вниз
        }
        else if (vertical > 0) // Вверх
        {
            attackOffset = Vector3.zero; // Смещение прямо вверх
        }
        else if (vertical < 0) // Вниз
        {
            attackOffset = new Vector3(0f, -1f, 0f); // Смещение прямо вниз
        }

        // Обновляем позицию точки атаки
        attackPos.localPosition = attackOffset;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void StartAttack()
    {
        if (player != null)
        {
            player.canMove = false;
        }
        
        //анимация атаки
        animator.SetTrigger("Attack");
        
        //нахождение врага в области атаки
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
        
        //нанесение урона врагам
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    
    public void FinishAttack()
    {
        // Разблокируем движение игрока
        if (player != null)
        {
            player.canMove = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
