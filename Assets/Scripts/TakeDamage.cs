using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public UIHealthBarHelper healthBarHelper;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Syringe"))
        {
            healthBarHelper.TakeDamage(10f);
        }
        else if (collision.gameObject.CompareTag("Knife"))
        {
            healthBarHelper.TakeDamage(20f);
        }       

            
    }

}
