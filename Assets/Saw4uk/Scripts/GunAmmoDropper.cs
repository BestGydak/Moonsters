using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GunAmmoDropper : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ammoPrefab;
    [SerializeField] private Transform gunRotator;
    [SerializeField] private float Force;
    private static Random random;

    private void Awake()
    {
        random = new Random();
    }

    public void DropAmmo()
    {
        var randomModifier1 = random.Next(-20,20)/100f + 1;
        var randomModifier2 = random.Next(-20,20)/100f + 1;
        var instance = Instantiate(ammoPrefab,transform);
        StartCoroutine(AmmoStopCoroutine(instance));
        instance.transform.localPosition = Vector3.zero;
        instance.transform.parent = null;
        var radians = Mathf.Deg2Rad*(gunRotator.rotation.eulerAngles.z-110);
        var vector = new Vector2((float)Math.Cos(radians)*randomModifier1, (float)Math.Sin(radians)*randomModifier2);
        instance.AddForce(vector * Force,ForceMode2D.Impulse);
        instance.AddTorque(randomModifier1 * 5, ForceMode2D.Impulse);
    }

    private IEnumerator AmmoStopCoroutine(Rigidbody2D rigidbody)
    {
        yield return new WaitForSeconds(0.6f);
        rigidbody.isKinematic = true;
        rigidbody.freezeRotation = true;
        rigidbody.velocity = Vector2.zero;
        rigidbody.transform.GetComponent<SpriteRenderer>().sortingOrder = 90;
    }
}
