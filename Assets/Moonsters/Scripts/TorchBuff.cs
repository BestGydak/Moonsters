using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Moonsters
{
    public class TorchBuff : MonoBehaviour
    {
        [SerializeField] private float newInner;
        [SerializeField] private float newOuter;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent<AstronautShooting>(out var shooting))
            {
                shooting.Light2D.pointLightInnerAngle = newInner;
                shooting.Light2D.pointLightOuterAngle = newOuter;
                Destroy(gameObject);
            }
        }
    }
}
