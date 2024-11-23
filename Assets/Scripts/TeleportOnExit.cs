using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnExit : MonoBehaviour
{ 

    [SerializeField] private Vector3 minBoundary = new Vector3(-298.8f, 0f, -284.8f);
    [SerializeField] private Vector3 maxBoundary = new Vector3(357.9f, 100f, 294.7f);

    [SerializeField] private Vector3 teleportPoint = new Vector3(15f, 0f, 68f);


    [SerializeField] private Transform player;


    private void Update()
    {

        if (!IsInsideBoundary(player.position))
        {
            TeleportHome();
        }
    }


    private bool IsInsideBoundary(Vector3 position)
    {
        return position.x >= minBoundary.x && position.x <= maxBoundary.x &&
               position.y >= minBoundary.y && position.y <= maxBoundary.y &&
               position.z >= minBoundary.z && position.z <= maxBoundary.z;
    }

    private void TeleportHome()
    {

        player.position = teleportPoint;


        Debug.Log("»грок вышел за границы и был телепортирован домой!");
    }
}



