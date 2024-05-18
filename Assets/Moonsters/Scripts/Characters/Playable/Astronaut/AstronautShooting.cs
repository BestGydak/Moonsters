using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AstronautShooting : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [Header("Shoot Settings")] 
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float distanceToGun = 1f;
    [SerializeField] private float bulletSpeed = 25f;
    [SerializeField] private float shootingDelay = 0.5f;

    private bool isShooting;
    private float remainingShootingDelay;

    private void FixedUpdate()
    {
        if (isShooting && remainingShootingDelay <= 0)
        {
            var playerTransform = transform;
            var rads = (playerTransform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
            var direction = new Vector3(Mathf.Cos(rads), Mathf.Sin(rads), 0);
        
            var bullet = Instantiate(projectilePrefab, playerTransform.position + (direction * distanceToGun), playerTransform.rotation);
            bullet.Shoot(direction, bulletSpeed);

            remainingShootingDelay = shootingDelay;
        }
        else
        {
            remainingShootingDelay -= Time.fixedDeltaTime;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        remainingShootingDelay = 0;
        isShooting = context.performed;
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        var mouseScreenPosition = context.ReadValue<Vector2>();
        var mouseWordPosition = camera.ScreenToWorldPoint(mouseScreenPosition);
        
        transform.rotation = Quaternion.LookRotation(
            Vector3.forward,
            mouseWordPosition - transform.position
        );
    }
}
