using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject _target = null;
    private Vector3 _defaultCameraPosition;
    private Vector3 _destination;
    private float _defaultOrthographicSize = 7f;
    private float _easedLerp = 0.07f;
    private float _rainforcedLerp = 0.6f;

    public GameObject Target
    {
        get { return _target; }
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
        _defaultCameraPosition = transform.position;
        _destination = _defaultCameraPosition;
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (_target != null)
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
        Vector2 minXY = Vector2.zero;
        _destination = target.transform.position;
        _destination.x = Mathf.Max(minXY.x, _destination.x);
        _destination.y = Mathf.Max(minXY.y, _destination.y);
        _destination.z = _defaultCameraPosition.z;
    }

    private void CalculateCameraSize()
    {
        if (_destination.y == 0) return;
        float newOrthographicSize = _defaultOrthographicSize + _destination.y;
        Camera.main.orthographicSize = Mathf.Lerp(_defaultOrthographicSize,
            newOrthographicSize, _rainforcedLerp);
    }


}
