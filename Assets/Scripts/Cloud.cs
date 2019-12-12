using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cloud : MonoBehaviour
{
    #region Variables
    public GameObject cloudSphere = null;
    
    private const int MinNumClouds = 6;
    private const int MaxNumClouds = 10;
    private readonly Vector3 _offsetSphereScale = new Vector3(4, 2, 1);
    private readonly Vector2 _rangeSphereScaleX = new Vector2(4, 7);
    private readonly Vector2 _rangeSphereScaleY = new Vector2(2, 4);
    private readonly Vector2 _rangeSphereScaleZ = new Vector2(2, 4);
    private float _minScaleY = 2f;

    private List<GameObject> _cloudsList = null;
    #endregion

    private void Start()
    {
        _cloudsList = new List<GameObject>();
        int currentAmountClouds = Random.Range(MinNumClouds, MaxNumClouds);
        for (int i = 0; i < currentAmountClouds; i++)
        {
            CreateCloud();
        }
    }

    private void CreateCloud()
    {
        GameObject newCloud = Instantiate(cloudSphere);
        _cloudsList.Add(newCloud);
        Transform transformCloud = newCloud.transform;
        transformCloud.SetParent(this.transform);
            
        transformCloud.localPosition = GetNewPosition();
        transformCloud.localScale = GetNewScale(transformCloud);
    }

    private Vector3 GetNewPosition()
    {
        Vector3 newPosition = Random.insideUnitSphere;
        newPosition.x *= _offsetSphereScale.x;
        newPosition.y *= _offsetSphereScale.y;
        newPosition.z *= _offsetSphereScale.z;
        
        return newPosition;
    }

    private Vector3 GetNewScale(Transform cloud)
    {
        Vector3 newScale = Vector3.one;
        newScale.x = Random.Range(_rangeSphereScaleX.x, _rangeSphereScaleX.y);
        newScale.y = Random.Range(_rangeSphereScaleY.x, _rangeSphereScaleY.y);
        newScale.y *= 1 - (Mathf.Abs(cloud.localPosition.x) / _offsetSphereScale.x);
        newScale.y = Mathf.Max(newScale.y, _minScaleY);
        newScale.z = Random.Range(_rangeSphereScaleZ.x, _rangeSphereScaleZ.y);
        
        return newScale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reset();
        }
    }

    private void Reset()
    {
        foreach (GameObject cloud in _cloudsList)
        {
            Destroy(cloud);
        }

        Start();
    }
}
