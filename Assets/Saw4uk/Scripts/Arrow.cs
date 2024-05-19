using UnityEngine;

namespace Saw4uk.Scripts
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private GameObject destinationObject;
        [SerializeField] private GameObject circleCenter;
        [SerializeField] private float minDistance;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteRenderer icon;

        public GameObject DestinationObject
        {
            get => destinationObject;
            set => destinationObject = value;
        }


        private float radius;
        void Start()
        {
            radius = Vector2.Distance(circleCenter.transform.position, transform.position);
        }
    
        void LateUpdate()
        {
            if(destinationObject == null)
                return;
            var distanceVector = destinationObject.transform.position - circleCenter.transform.position;
            var direction = (distanceVector).normalized;

            spriteRenderer.enabled = !(distanceVector.magnitude < minDistance);
            icon.enabled = !(distanceVector.magnitude < minDistance);
        
            transform.position = circleCenter.transform.position + direction * radius;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }
}