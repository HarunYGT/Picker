using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArm : MonoBehaviour
{
    bool Rotate;
    [SerializeField] private float RotateValue;
    public void StartRotate()
    {
        Rotate =true;
    }
    // Update is called once per frame
    void Update()
    {
        if(Rotate)
            transform.Rotate(0,0,RotateValue,Space.Self);
    }
} 
