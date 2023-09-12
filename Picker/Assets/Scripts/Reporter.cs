using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reporter : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PickerTrigger"))
        {
            gameManager.LimitReached();
        }
    }
}
