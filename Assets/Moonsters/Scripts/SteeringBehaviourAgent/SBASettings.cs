using UnityEngine;
using System.Collections.Generic;

namespace SBA
{
    [CreateAssetMenu(fileName = "New SBA Settings", menuName = "SBA Settings")]
    public class SBASettings : ScriptableObject
    {
        [SerializeField] private float _agentRadius;
        [SerializeField] private List<SBAObstacle> _obstacles;

        public float AgentRadius => _agentRadius;
        public IReadOnlyCollection<SBAObstacle> Obstacles => _obstacles;
    }

    [System.Serializable]
    public struct SBAObstacle
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _radius;

        public LayerMask Layer => _layer;
        public float Radius => _radius;
    }
}


