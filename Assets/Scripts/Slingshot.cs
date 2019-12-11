using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public GameObject launchPoint = null;
    public GameObject projectilePrefab = null;

    private GameObject _projectileObject = null;
    private Projectile Projectile;
    private Vector2 _mousePosition;
    private Vector2 _launchPosition;
    private Vector2 _mouseDelta;
    private float _maxMagnitude;
    private bool _isLaunhProjectile = false;

    private void Awake()
    {
        _launchPosition = launchPoint.transform.position;
        _maxMagnitude = GetComponent<CircleCollider2D>().radius;
    }
    
    private void OnMouseDrag()
    {
        if (_projectileObject == null)
        {
            launchPoint.SetActive(true);
            CreateProjectile();
        }
        else if (_isLaunhProjectile == false)
        {
            CalculateProjectilePosition();
        }
    }

    private void OnMouseUp()
    {
        launchPoint.SetActive(false);
        LaunchProjectile();
    }

    private void CreateProjectile()
    {
        _isLaunhProjectile = false;
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _projectileObject = Instantiate(projectilePrefab, _mousePosition, Quaternion.identity);
        Projectile = _projectileObject.GetComponent<Projectile>();
    }

    private void CalculateProjectilePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseDelta = _mousePosition - _launchPosition;
        if (_mouseDelta.sqrMagnitude > _maxMagnitude*_maxMagnitude)
        {
            _mouseDelta.Normalize();       
            _mouseDelta *= _maxMagnitude;
        }
        Vector2 newPosition = _launchPosition + _mouseDelta;

        Projectile.Position = newPosition;
    }
    private void LaunchProjectile()
    {
        _isLaunhProjectile = true;
        Projectile.SetVelocity(_mouseDelta);
    }
}
