using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float velocityMult = 9f;

    private float _lifeTime = 10f;
    private float _minVelocity = 0.003f;
    private FollowCamera _followCamera = null;
    private Rigidbody _thisRigidbody = null;

    public Vector3 Position
    {
        get => transform.position; 
        set => transform.position = value; 
    }

    private void Awake()
    {
        _followCamera = Camera.main.GetComponent<FollowCamera>();
        _thisRigidbody = GetComponent<Rigidbody>();
        _thisRigidbody.isKinematic = true;
        StartCoroutine(CountLifeTime(_lifeTime));
    }

    private void FixedUpdate()
    {
        if (!_thisRigidbody.isKinematic)
        {
            Vector2 velocity = (Vector2)_thisRigidbody.velocity;
            float sqrVelocity = velocity.sqrMagnitude;
            if (sqrVelocity <= _minVelocity)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetVelocity(Vector2 mouseDelta)
    {
        _followCamera.Target = gameObject;
        _thisRigidbody.isKinematic = false;
        _thisRigidbody.velocity = -mouseDelta * velocityMult;
    }

    private IEnumerator CountLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _followCamera.Target = null;
    }
}
