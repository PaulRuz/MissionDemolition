using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float velocityMult = 9f;

    private float _lifeTime = 10f;
    private float _minVelocity = 0.003f;
    private FollowCamera followCamera = null;
    private Rigidbody ThisRigidbody = null;

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void Awake()
    {
        followCamera = Camera.main.GetComponent<FollowCamera>();
        ThisRigidbody = GetComponent<Rigidbody>();
        ThisRigidbody.isKinematic = true;
        StartCoroutine(CountLifeTime(_lifeTime));
    }

    private void FixedUpdate()
    {
        if (!ThisRigidbody.isKinematic)
        {
            Vector2 velocity = (Vector2)ThisRigidbody.velocity;
            float sqrVelocity = velocity.sqrMagnitude;
            if (sqrVelocity <= _minVelocity)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetVelocity(Vector2 mouseDelta)
    {
        followCamera.Target = gameObject;
        ThisRigidbody.isKinematic = false;
        ThisRigidbody.velocity = -mouseDelta * velocityMult;
    }

    private IEnumerator CountLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        followCamera.Target = null;
    }
}
