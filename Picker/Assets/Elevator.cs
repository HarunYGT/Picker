using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator BarrierArea;
    
    public void LiftBareer()
    {
        BarrierArea.Play("BarrierLift");
    }
    public void End()
    {
        gameManager.isPickerMoving = true;  
    }
}
