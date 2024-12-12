using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulSpawner : MonoBehaviour
{
    public GameObject ghoulPrefab; // Префаб гулів
    public Transform[] spawnPoints; // Масив точок спавну
    public float spawnInterval = 1f; // Інтервал спавну (у секундах)
    public int maxGhouls = 7; // Максимальна кількість гулів

    private int currentGhoulCount = 0; // Поточна кількість гулів

    void Start()
    {
        // Запускаємо повторюваний виклик функції SpawnGhoul
        InvokeRepeating(nameof(SpawnGhoul), 0f, spawnInterval);
    }

    void SpawnGhoul()
    {
        // Перевіряємо, чи ми не перевищили ліміт
        if (currentGhoulCount >= maxGhouls)
        {
            return;
        }

        // Вибираємо випадкову точку спавну
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Створюємо гуля у вибраній точці
        Instantiate(ghoulPrefab, spawnPoint.position, spawnPoint.rotation);

        // Збільшуємо лічильник гулів
        currentGhoulCount++;
    }

    public void OnGhoulDestroyed()
    {
        // Зменшуємо лічильник гулів, коли гул знищується
        currentGhoulCount--;
    }
}
