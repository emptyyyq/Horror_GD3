using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intera : MonoBehaviour
{
    public float interactDistance = 3f;
    private InteractableItem currentItem;

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); // Îע דמכמגû (ךאלונû)
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            InteractableItem item = hit.collider.GetComponent<InteractableItem>();

            if (item != null)
            {
                currentItem = item;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentItem.Pickup();
                    currentItem = null; // Ñבנמס ןמסכו ןמהבמנא
                }

                return;
            }
        }

        currentItem = null;
    }
}
