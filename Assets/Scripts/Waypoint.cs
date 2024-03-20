using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class Waypoint : MonoBehaviour
    {
        public bool wasHit = false;
        void Start() {
            RaceManager.Instance.resetWaypointsHit.AddListener(resetHit);
        }
        
        void Update() {

        }

        private void OnTriggerEnter(Collider other) {
            if (other.transform.tag == "PlayerWaypoint" && !wasHit) {
                RaceManager.Instance.playerWaypointsHit += 1;
                Debug.Log("Waypoint Hit");
                wasHit = true;
            }
            else {
                Debug.Log("Waypoint Already Hit");
            }
        }

        private void OnDrawGizmos()
        {
            // Draw a yellow sphere and the transforms position
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 2);
        }

        private void resetHit() {
            wasHit = false;
        }
    }

