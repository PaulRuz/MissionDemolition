using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject launchPoint = null;
    public GameObject projectilePrefab = null;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    private GameObject projectile = null;
    private Rigidbody rigidbodyProjectile;
    private Vector2 mousePosition;
    private Vector2 launchPosition;
    private Vector2 mouseDelta;

    private float maxMagnitude;

    private void Awake()
    {
        launchPosition = launchPoint.transform.position;
        maxMagnitude = GetComponent<CircleCollider2D>().radius;
    }

    private void OnMouseDown()
    {
        launchPoint.SetActive(true);
        CreateProjectile();
    }

    private void OnMouseDrag()
    {
        CalculateProjectilePosition();
    }

    private void OnMouseUp()
    {
        launchPoint.SetActive(false);
        LaunchProjectile();
    }

    private void CreateProjectile()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        projectile = Instantiate(projectilePrefab, mousePosition, Quaternion.identity);
        rigidbodyProjectile = projectile.GetComponent<Rigidbody>();
        rigidbodyProjectile.isKinematic = true;
    }

    private void CalculateProjectilePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDelta = mousePosition - launchPosition;
        Debug.Log("(1) mouseDealta = mousePos - launchPos" + mouseDelta + " = " + mousePosition + " - " + launchPosition);
        Debug.Log("(2) mouseDelta.magnitude" + mouseDelta.magnitude);

        if (mouseDelta.sqrMagnitude > maxMagnitude*maxMagnitude)
        {
            mouseDelta.Normalize();       
            mouseDelta *= maxMagnitude;
        }

        Vector2 newPosition = launchPosition + mouseDelta;
        Debug.Log("(3) newPosition" + newPosition);
        projectile.transform.position = newPosition;
    }
    private void LaunchProjectile()
    {
        // Debug.Log(mouseDelta);
        // Debug.Log(maxMagnitude);

        rigidbodyProjectile.isKinematic = false;
        rigidbodyProjectile.velocity = -mouseDelta * velocityMult;
        projectile = null;
    }

}
