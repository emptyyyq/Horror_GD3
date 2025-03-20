using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_screamer : MonoBehaviour
{
    public GameObject scream; // ��'��� ������� (������� ���� ��������� �� �����)
    public float minTime = 30f; // ̳�������� ��� �� �������
    public float maxTime = 180f; // ������������ ��� �� �������
    public float spawnDistance = 3f; // ³������ ����� ����� �������
    public float screamDuration = 2f; // ���, ����� ���� ������� ������

    private void Start()
    {
        if (scream != null)
            scream.SetActive(false); // �������� ������� �� �������

        StartCoroutine(SpawnScreamerRandomly());
    }

    private IEnumerator SpawnScreamerRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime); // ������ ���������� ���
            yield return new WaitForSeconds(waitTime);

            SpawnScreamer(); // �'��������� �������
            yield return new WaitForSeconds(screamDuration); // ������, ���� �� ������
            scream.SetActive(false); // ����� �������
        }
    }

    private void SpawnScreamer()
    {
        if (scream == null)
        {
            Debug.LogError("Scream �� ������������ � ���������!");
            return;
        }

        // ��������� ��'��� �� ������� ����� �������
        scream.transform.position = transform.position + transform.forward * spawnDistance;
        scream.transform.LookAt(transform); // ��������� ��'��� �� ������
        scream.SetActive(true); // ������� �������
    }


    /*private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("scream_trigger") && other.CompareTag("Player"))
        {
            _scream.SetActive(true);
        }
        


    }*/

}


