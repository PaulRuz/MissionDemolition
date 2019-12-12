using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    public GameObject cloudPrefab = null;
    
    private int _numClouds = 40;
    private Vector3 _minCloudPos = new Vector3(-50, -5, 30);
    private Vector3 _maxCloudPos = new Vector3(70, 20, 50);
    private float _minCloudScale = 0.2f;
    private float _maxCloudScale = 0.8f;
    private float _speedCloud = 1f;

    private GameObject[] _cloudInstance;

    private void Awake()
    {
        _cloudInstance = new GameObject[_numClouds];

        for (int i = 0; i < _numClouds; i++)
        {
            GameObject newCloud = Instantiate(cloudPrefab);
            Vector3 newPos = Vector3.zero;
            newPos.x = Random.Range(_minCloudPos.x, _maxCloudPos.x);
            newPos.y = Random.Range(_minCloudPos.y, _maxCloudPos.y);

            float depthScale = Random.value;
            float newScale = Mathf.Lerp(_minCloudScale, _maxCloudScale, depthScale);

            newPos.y = Mathf.Lerp(_minCloudPos.y, newPos.y, depthScale);
            newPos.z = _maxCloudPos.z - _minCloudPos.z * depthScale; 

            newCloud.transform.position = newPos;
            newCloud.transform.localScale = Vector3.one * newScale;

            newCloud.transform.SetParent(this.transform);
            _cloudInstance[i] = newCloud;
        }
    }

    private void Update()
    {
        foreach (GameObject currentCloud in _cloudInstance)
        {
            float currentScale = currentCloud.transform.localScale.z;
            Vector3 currentPos = currentCloud.transform.position;
            currentPos.x -= currentScale * Time.deltaTime * _speedCloud;
            if (currentPos.x <= _minCloudPos.x)
            {
                currentPos.x = _maxCloudPos.x;
            }

            currentCloud.transform.position = currentPos;
        }
    }
}
