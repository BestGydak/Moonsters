using UnityEngine;

namespace SBA
{
    public class SteeringBehaviourAgent : MonoBehaviour
    {
        [SerializeField] private SBASettings settings;
        [SerializeField] private bool showGizmos;

        public Vector2 CalculateDirection(Vector2 desiredDirection)
        {
            var obstacleMap = CalculateObstacleMap();
            var targetMap = CalculateTargetMap(desiredDirection);

            var finalMap = new float[8];
            for (var i = 0; i < 8; i++)
            {
                finalMap[i] = Mathf.Max(0, targetMap[i] - obstacleMap[i]);
            }
            var finalDirection = Vector2.zero;
            for (var i = 0; i < 8; i++)
            {
                finalDirection += DirectionUtils.EightDirections[i] * finalMap[i];
            }
            if (showGizmos)
                Debug.DrawRay(transform.position, finalDirection.normalized, Color.red, 2);
            return finalDirection.normalized;
        }

        private float[] CalculateObstacleMap()
        {
            var obstacleMap = new float[8];
            foreach (var obstacle in settings.Obstacles)
            {
                AddToTargetMap(obstacleMap, obstacle.Layer, obstacle.Radius);
            }

            return obstacleMap;
        }

        private void AddToTargetMap(float[] obstacleMap, LayerMask obstacleLayer, float radius)
        {
            var obstacles = Physics2D.OverlapCircleAll(transform.position, radius, obstacleLayer);
            foreach (var obstacle in obstacles)
            {
                var directionToObstacle = obstacle.ClosestPoint(transform.position) - (Vector2)transform.position;
                var distanceToObstacle = directionToObstacle.magnitude;
                var weight = distanceToObstacle <= settings.AgentRadius ? 1 : (radius - distanceToObstacle) / radius;

                for (var i = 0; i < 8; i++)
                {
                    var result = Vector2.Dot(directionToObstacle.normalized, DirectionUtils.EightDirections[i]) * weight;

                    if (result > obstacleMap[i])
                    {
                        obstacleMap[i] = result;
                    }
                }
            }
        }

        private float[] CalculateTargetMap(Vector2 desiredDirection)
        {
            var targetMap = new float[8];

            for (var i = 0; i < 8; i++)
            {
                targetMap[i] = Vector2.Dot(desiredDirection.normalized, DirectionUtils.EightDirections[i]);
            }

            return targetMap;
        }
    }
}

