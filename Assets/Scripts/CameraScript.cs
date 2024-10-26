using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float camPositionspeed = 5f;

    private void LateUpdate()
    {
        Vector3 newCamPosition = new Vector3(offset.x, player.position.y + offset.y, player.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, newCamPosition, camPositionspeed * Time.deltaTime);
    }
}
