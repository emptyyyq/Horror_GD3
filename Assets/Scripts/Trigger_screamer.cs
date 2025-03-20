using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_screamer : MonoBehaviour
{
    public GameObject scream; // Об'єкт скрімера (повинен бути вимкнений на старті)
    public float minTime = 30f; // Мінімальний час між атаками
    public float maxTime = 180f; // Максимальний час між атаками
    public float spawnDistance = 3f; // Відстань появи перед гравцем
    public float screamDuration = 2f; // Час, через який скример зникне

    private void Start()
    {
        if (scream != null)
            scream.SetActive(false); // Вимикаємо скрімера на початку

        StartCoroutine(SpawnScreamerRandomly());
    }

    private IEnumerator SpawnScreamerRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime); // Чекаємо випадковий час
            yield return new WaitForSeconds(waitTime);

            SpawnScreamer(); // З'являється скример
            yield return new WaitForSeconds(screamDuration); // Чекаємо, поки він полякає
            scream.SetActive(false); // Зникає скример
        }
    }

    private void SpawnScreamer()
    {
        if (scream == null)
        {
            Debug.LogError("Scream не встановлений у інспекторі!");
            return;
        }

        // Переміщуємо об'єкт на позицію перед гравцем
        scream.transform.position = transform.position + transform.forward * spawnDistance;
        scream.transform.LookAt(transform); // Повертаємо об'єкт на гравця
        scream.SetActive(true); // Вмикаємо скрімера
    }


    /*private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("scream_trigger") && other.CompareTag("Player"))
        {
            _scream.SetActive(true);
        }
        


    }*/

}


