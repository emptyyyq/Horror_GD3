using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulSpawner : MonoBehaviour
{
    public GameObject ghoulPrefab; // ������ ����
    public Transform[] spawnPoints; // ����� ����� ������
    public float spawnInterval = 1f; // �������� ������ (� ��������)
    public int maxGhouls = 7; // ����������� ������� ����

    private int currentGhoulCount = 0; // ������� ������� ����

    void Start()
    {
        // ��������� ������������ ������ ������� SpawnGhoul
        InvokeRepeating(nameof(SpawnGhoul), 0f, spawnInterval);
    }

    void SpawnGhoul()
    {
        // ����������, �� �� �� ���������� ���
        if (currentGhoulCount >= maxGhouls)
        {
            return;
        }

        // �������� ��������� ����� ������
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // ��������� ���� � ������� �����
        Instantiate(ghoulPrefab, spawnPoint.position, spawnPoint.rotation);

        // �������� �������� ����
        currentGhoulCount++;
    }

    public void OnGhoulDestroyed()
    {
        // �������� �������� ����, ���� ��� ���������
        currentGhoulCount--;
    }
}
