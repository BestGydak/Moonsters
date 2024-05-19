using System;
using System.Collections;
using UnityEngine;

namespace Saw4uk.Scripts
{
    public class TimeToLeave : MonoBehaviour
    {
        [SerializeField] private float timeToLiveSeconds;

        private void Awake()
        {
            StartCoroutine(DeathCoroutine());
        }

        private IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(timeToLiveSeconds);
            Destroy(gameObject);
        }
    }
}