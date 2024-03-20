using System;
using UnityEngine;

#pragma warning disable 649
namespace UnityStandardAssets.Utility
{
    public class PlayerWaypointProgressTracker : MonoBehaviour
    {
        public static int playerWaypointsHit = 0;
        // This script can be used with any object that is supposed to follow a
        // route marked out by waypoints.

        // This script manages the amount to look ahead along the route,
        // and keeps track of progress and laps.

        [SerializeField] public WaypointCircuit circuit; // A reference to the waypoint-based route we should follow

        [SerializeField] private ProgressStyle progressStyle = ProgressStyle.SmoothAlongRoute;
        // whether to update the position smoothly along the route (good for curved paths) or just when we reach each waypoint.

        [SerializeField] private float pointToPointThreshold = 10;
        // proximity to waypoint which must be reached to switch target to next waypoint : only used in PointToPoint mode.

        public enum ProgressStyle
        {
            SmoothAlongRoute,
            PointToPoint,
        }

        // these are public, readable by other objects - i.e. for an AI to know where to head!
        public WaypointCircuit.RoutePoint targetPoint { get; private set; }
        public WaypointCircuit.RoutePoint speedPoint { get; private set; }
        public WaypointCircuit.RoutePoint progressPoint { get; private set; }

        public Transform target;

        private float progressDistance; // The progress round the route, used in smooth mode.
        public int progressNum; // the current waypoint number, used in point-to-point mode.
        public int waypointsHit = 0;
        private Vector3 lastPosition; // Used to calculate current speed (since we may not have a rigidbody component)
        private float speed; // current speed of this object (calculated from delta since last frame)

        // setup script properties
        private void Start()
        {
            // we use a transform to represent the point to aim for, and the point which
            // is considered for upcoming changes-of-speed. This allows this component
            // to communicate this information to the AI without requiring further dependencies.

            // You can manually create a transform and assign it to this component *and* the AI,
            // then this component will update it, and the AI can read it.
            if (target == null)
            {
                target = new GameObject(name + " Waypoint Target").transform;
            }

            Reset();
        }


        // reset the object to sensible values
        public void Reset()
        {
            progressDistance = 0;
            progressNum = 0;
            if (progressStyle == ProgressStyle.PointToPoint)
            {
                target.position = circuit.Waypoints[progressNum].position;
                target.rotation = circuit.Waypoints[progressNum].rotation;
            }
        }


        private void Update()
        {
            waypointsHit = RaceManager.Instance.playerWaypointsHit;
            
            if (progressStyle == ProgressStyle.SmoothAlongRoute)
            {
                progressPoint = circuit.GetRoutePoint(progressDistance);

                Vector3 progressDelta = progressPoint.position - transform.position;
                if (Vector3.Dot(progressDelta, progressPoint.direction) < 0)
                {
                    progressDistance += progressDelta.magnitude*0.5f;
                    
                    Debug.Log(waypointsHit);
                }

                lastPosition = transform.position;
            }
            else
            {
                // point to point mode. Just increase the waypoint if we're close enough:

                Vector3 targetDelta = target.position - transform.position;
                if (targetDelta.magnitude < pointToPointThreshold)
                {
                    progressNum = (progressNum + 1)%circuit.Waypoints.Length;
                }


                target.position = circuit.Waypoints[progressNum].position;
                target.rotation = circuit.Waypoints[progressNum].rotation;

                // get our current progress along the route
                progressPoint = circuit.GetRoutePoint(progressDistance);
                Vector3 progressDelta = progressPoint.position - transform.position;
                if (Vector3.Dot(progressDelta, progressPoint.direction) < 0)
                {
                    progressDistance += progressDelta.magnitude;
                }
                lastPosition = transform.position;
            }
        }


        
    }
}
