using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Rigidbody ThisRigidbody = null;

    private void Awake()
    {
        ThisRigidbody = GetComponent<Rigidbody>();
        if (ThisRigidbody)
        {
            ThisRigidbody.Sleep();
        }
    }


}
