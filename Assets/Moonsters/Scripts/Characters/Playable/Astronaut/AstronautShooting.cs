using System;
using System.Collections;
using System.Collections.Generic;
using Moonsters;
using UnityEngine;
using UnityEngine.InputSystem;

public class AstronautShooting : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Health health;

    [Header("Gun Settings")] 
    [SerializeField] private Transform gunRotator;

    [SerializeField] private Transform gun;
    [SerializeField] private SpriteRenderer gunSpriteRenderer;

    [Header("Shoot Settings")] 
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileSpeed = 25f;
    [SerializeField] private float shootingDelay = 0.5f;
    [SerializeField] private int maxAmmo = 5;

    private bool isShooting;
    private float remainingShootingDelay;
    private int currentAmmo;
    
    public int CurrentAmmo => currentAmmo;
    public bool IsFullAmmo => currentAmmo == maxAmmo;

    private void Awake()
    {
        remainingShootingDelay = 0;
        currentAmmo = maxAmmo;
        
        health.CurrentHealthChanged.AddListener((health, previousCurrentHealth, currentHealth) =>
        {
            Debug.Log($"Prev. heal: {previousCurrentHealth}; Curr. heal: {currentHealth}");
        });
    }

    private void FixedUpdate()
    {
        if (isShooting && remainingShootingDelay <= 0 && currentAmmo > 0)
        {
            var gunTransform = gunRotator.transform;
            var rads = (gunTransform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
            var direction = new Vector3(Mathf.Cos(rads), Mathf.Sin(rads), 0);
            Shoot(direction, gun.position);
        }
        else
        {
            remainingShootingDelay -= Time.fixedDeltaTime;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        isShooting = context.performed;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        var mouseScreenPosition = context.ReadValue<Vector2>();
        var mouseWordPosition = camera.ScreenToWorldPoint(mouseScreenPosition);

        gunRotator.transform.rotation = Quaternion.LookRotation(
            Vector3.forward,
            mouseWordPosition - transform.position
        );

        var rad = gunRotator.rotation.eulerAngles.z * Mathf.Deg2Rad;

        gunSpriteRenderer.flipY = Mathf.Sin(rad) > 0;
        gunSpriteRenderer.sortingOrder = Mathf.Cos(rad) > 0 ? 0 : 1;
    }

    private void Shoot(Vector2 direction, Vector3 shootPosition)
    {
        var bullet = Instantiate(projectilePrefab, shootPosition, gunRotator.rotation);
        bullet.LaunchProjectile(direction, projectileSpeed);

        currentAmmo -= 1;
        remainingShootingDelay = shootingDelay;
        
        Debug.Log($"Current ammo amount: {currentAmmo}");
    }

    public void AddAmmo(int count)
    {
        currentAmmo += Mathf.Clamp(count, 0, maxAmmo);
        Debug.Log($"Added ammo: {count}");
    }

    public void AddHealth(int healAmount)
    {
        health.CurrentHealth += healAmount;
    }
}