using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 target_offset;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,target.transform.position + target_offset, .125f);
    }
}
