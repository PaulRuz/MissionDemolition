using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Rigidbody _thisRigidbody = null;

    private void Awake()
    {
        _thisRigidbody = GetComponent<Rigidbody>();
        if (_thisRigidbody)
        {
            _thisRigidbody.Sleep();
        }
    }


}
