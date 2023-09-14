using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PowerUpBall : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private string Item;
    [SerializeField] private int BonusTopIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (Item == "Arm")
        {
            if (other.CompareTag("PickerTrigger"))
            {
                gameManager.GetArms();
                gameObject.SetActive(false);
            }
        }
        else if (Item == "Bonus")
        {
             if (other.CompareTag("PickerTrigger"))
            {
                gameManager.ActiveBonusBalls(BonusTopIndex);
                gameObject.SetActive(false);
            }
        }

    }
}
