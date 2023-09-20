using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutBound : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        collision.transform.position = Vector3.up * 4;
    }
}
