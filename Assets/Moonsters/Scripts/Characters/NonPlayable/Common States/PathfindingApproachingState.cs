using Pathfinding;
using System.Collections;
using UnityEngine;
using SBA;

namespace Moonsters
{
    public class PathfindingApproachingState : State
    {
        private readonly float speed;
        private readonly GameObject target;
        private readonly Rigidbody2D controller;
        private readonly Seeker seeker;
        private readonly SteeringBehaviourAgent sba;
        private readonly float distanceToReachNode;
        private readonly float findPathDelayInSeconds;

        private Coroutine pathCoroutine;

        private Path path;
        private int currentDestinationNodeId;

        public PathfindingApproachingState(
            float speed,
            Rigidbody2D controller, 
            GameObject target,
            Seeker seeker,
            SteeringBehaviourAgent sba,
            float distanceToReachNode,
            float findPathDelayInSeconds)
        {
            this.speed = speed;
            this.target = target;
            this.controller = controller;
            this.seeker = seeker;
            this.distanceToReachNode = distanceToReachNode;
            this.findPathDelayInSeconds = findPathDelayInSeconds;   
            this.sba = sba;
        }

        public override void OnEnter()
        {
            path = null;
            pathCoroutine = seeker.StartCoroutine(PathCoroutine());
        }

        public override void OnExit()
        {
            seeker.StopCoroutine(pathCoroutine);
        }

        private IEnumerator PathCoroutine()
        {
            while(true)
            {
                if(!seeker.IsDone())
                {
                    yield return new WaitForFixedUpdate();
                    continue;
                }
                yield return new WaitForSeconds(findPathDelayInSeconds);
                seeker.StartPath(
                    controller.transform.position,
                    target.transform.position,
                    OnPathCreated);
            }
        }

        private void OnPathCreated(Path newPath)
        {
            if (newPath.error)
                return;
            path = newPath;
            currentDestinationNodeId = 0;
        }

        public override void OnPhysics()
        {
            if (path == null)
                return;

            if (currentDestinationNodeId >= path.vectorPath.Count)
                return;

            var distanceToNode = Vector2.Distance(
                path.vectorPath[currentDestinationNodeId], 
                controller.transform.position);

            if (distanceToNode <= distanceToReachNode)
                currentDestinationNodeId += 1;

            if (currentDestinationNodeId >= path.vectorPath.Count)
                return;

            var desiredDirection = GetDirectionToNode();
            var finalDirection = sba.CalculateDirection(desiredDirection).normalized;
            controller.MovePosition(controller.position + finalDirection * speed * Time.fixedDeltaTime);
        }

        private Vector2 GetDirectionToNode()
        {
            Vector2 node = path.vectorPath[currentDestinationNodeId];
            var direction = (node - (Vector2)controller.transform.position).normalized;
            return direction;
        }
    }
}

