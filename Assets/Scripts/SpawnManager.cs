using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;        // Префаб врага
    public Vector2 areaSize = new Vector2(20f, 10f); // Размер прямоугольной области
    public int maxEnemies = 5;            // Максимальное количество врагов
    public float spawnOffset = 1f;        // Отступ от края прямоугольника

    private List<GameObject> enemies = new List<GameObject>();

    private void Start()
    {
        SpawnInitialEnemies();
    }

    private void SpawnInitialEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomEdgePosition();

        // Создаём врага и подписываемся на его событие смерти
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().OnDeath += HandleEnemyDeath;
        enemies.Add(newEnemy);
    }

    private Vector2 GetRandomEdgePosition()
    {
        // Получаем случайную сторону (0 - верх, 1 - низ, 2 - лево, 3 - право)
        int edge = Random.Range(0, 4);
        Vector2 spawnPosition = Vector2.zero;

        // Используем transform.position как центр области спавна
        Vector2 center = transform.position;

        switch (edge)
        {
            case 0: // Верхняя сторона
                spawnPosition = new Vector2(
                    Random.Range(center.x - areaSize.x / 2, center.x + areaSize.x / 2),
                    center.y + areaSize.y / 2 + spawnOffset
                );
                break;
            case 1: // Нижняя сторона
                spawnPosition = new Vector2(
                    Random.Range(center.x - areaSize.x / 2, center.x + areaSize.x / 2),
                    center.y - areaSize.y / 2 - spawnOffset
                );
                break;
            case 2: // Левая сторона
                spawnPosition = new Vector2(
                    center.x - areaSize.x / 2 - spawnOffset,
                    Random.Range(center.y - areaSize.y / 2, center.y + areaSize.y / 2)
                );
                break;
            case 3: // Правая сторона
                spawnPosition = new Vector2(
                    center.x + areaSize.x / 2 + spawnOffset,
                    Random.Range(center.y - areaSize.y / 2, center.y + areaSize.y / 2)
                );
                break;
        }

        return spawnPosition;
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
        SpawnEnemy();
    }

    private void OnDrawGizmos()
    {
        // Визуализация прямоугольной области спавна
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(areaSize.x, areaSize.y, 0f));
    }
}

