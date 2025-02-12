using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform MALE2; // Ссылка на объект игрока (родительский объект камеры)
    public float mouseSensitivity = 350f; // Чувствительность мыши
    public float movementSpeed = 3f; // Скорость движения

    private float xRotation = 0f; // Текущий угол поворота камеры по оси X (вверх/вниз)
    private Vector3 velocity; // Вектор для хранения скорости движения камеры

    void Start()
    {
        // Изначально скрывать курсор не нужно
        Cursor.lockState = CursorLockMode.None;  // Убираем блокировку курсора
        Cursor.visible = true;  // Делаем курсор всегда видимым
    }

    void Update()
    {
        // Получаем ввод мыши для вращения камеры
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Обновляем угол поворота камеры вверх/вниз
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничиваем поворот вверх и вниз

        // Применяем вращение камеры по оси X
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Вращаем игрока (MALE2) по оси Y
        MALE2.Rotate(Vector3.up * mouseX);

        // Получаем ввод для движения камеры
        float moveX = Input.GetAxis("Horizontal"); // A/D (Left/Right)
        float moveZ = Input.GetAxis("Vertical");   // W/S (Forward/Back)

        // Двигаем камеру по осям X и Z
        Vector3 move = (MALE2.right * moveX + MALE2.forward * moveZ) * movementSpeed * Time.deltaTime;
        MALE2.position += move;
    }
}



