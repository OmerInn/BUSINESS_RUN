﻿using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;  
        public EndOfPathInstruction endOfPathInstruction;
        public float speed ;
        float distanceTravelled;
        public bool isBackward = false;
        public static PathCreation.Examples.PathFollower pathFollower;

        void Awake()
        {
            if (pathFollower == null)
            {
                pathFollower = this;
            }
        }
        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            /* if (pathCreator != null)
             {
                 distanceTravelled += speed * Time.deltaTime;
                 transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                 transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
             }*/
            if (pathCreator != null && !isBackward)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                Quaternion temp = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                // transform.eulerAngles = temp.eulerAngles + Vector3.forward * 90;
            }
            else if (pathCreator != null && isBackward)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                Quaternion temp = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                //  transform.eulerAngles = temp.eulerAngles + Vector3.forward * 90;
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}