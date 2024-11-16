using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;        // —сылка на объект игрока
    public Vector3 offset;          // —мещение камеры относительно игрока
    public float camPositionSpeed = 10f; // —корость перемещени€ камеры

    private void LateUpdate()
    {
        // ѕроверка, чтобы избежать значений около нул€ дл€ скорости
        if (camPositionSpeed <= 0f)
        {
            Debug.LogWarning("camPositionSpeed должно быть больше 0");
            return;
        }

        // ¬ычисл€ем целевую позицию камеры относительно позиции игрока и смещени€
        Vector3 newCamPosition = player.position + offset;

        // ѕлавное перемещение камеры к новой позиции
        transform.position = Vector3.Lerp(transform.position, newCamPosition, camPositionSpeed * Time.deltaTime);
    }
}



