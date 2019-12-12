using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private const float DefaultCameraSize = 7f;
    private Camera _thisCamera = null;
    private GameObject _target = null;
    private Vector3 _defaultCameraPosition;
    private Vector3 _destination;
    private float _easedLerp = 0.07f;
    private float _rainforcedLerp = 0.6f;

    public GameObject Target
    {
        get => _target;
        set
        {
            _target = value;
            if (_target == null)
            {
                _destination = _defaultCameraPosition;
            }
        }
    }

    private void Awake()
    {
        _thisCamera = GetComponent<Camera>();
        _defaultCameraPosition = transform.position;
        _destination = _defaultCameraPosition;
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (_target)
        {
            CalculateDestination(_target);
            CalculateCameraSize();
        }
        if (transform.position != _destination)
        {
            transform.position = Vector3.Lerp(transform.position, _destination, _easedLerp);
        }
    }

    private void CalculateDestination(GameObject target)
    {
        if (target)
        { 
            Vector2 minXy = Vector2.zero;
            _destination = target.transform.position;
            _destination.x = Mathf.Max(minXy.x, _destination.x);
            _destination.y = Mathf.Max(minXy.y, _destination.y);
            _destination.z = _defaultCameraPosition.z;
        }
    }

    private void CalculateCameraSize()
    {
        if (_destination.y == 0) return;
        var newOrthographicSize = DefaultCameraSize + _destination.y;
        _thisCamera.orthographicSize = Mathf.Lerp(DefaultCameraSize,
            newOrthographicSize, _rainforcedLerp);
    }


}
