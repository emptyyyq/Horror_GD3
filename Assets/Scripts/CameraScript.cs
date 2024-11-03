using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;  // Посилання на об'єкт гравця
    public Vector3 offset;  // Зміщення камери відносно гравця
    public float camPositionSpeed = 5f;  // Швидкість руху камери

    private void LateUpdate()
    {
        // Визначаємо нову позицію камери з урахуванням поточної позиції гравця та зміщення
        Vector3 newCamPosition = player.position + offset;

        // Плавне переміщення камери до нової позиції
        transform.position = Vector3.Lerp(transform.position, newCamPosition, camPositionSpeed * Time.deltaTime);
    }
}

