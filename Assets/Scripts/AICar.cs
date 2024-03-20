using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AICar : MonoBehaviour
{
    public GameObject m_waypointContainer;
    public float m_waypointProximity = 15.0f;
    private float m_proximitySqr;
    private Transform[] m_waypoints;
    public int m_currentWaypoint = 0;
    public int waypointsHit = 0;

    public Transform CurrentWaypoint
    {
        get { return m_waypoints[m_currentWaypoint]; }
    }

    public Transform LastWaypoint
    {
        get
        {
            if (m_currentWaypoint - 1 < 0)
            {
                return m_waypoints[m_waypoints.Length - 1];
            }
            return m_waypoints[m_currentWaypoint - 1];
        }
    }

    private void GetWaypoints()
    {
        Transform[] potentialWaypoints = m_waypointContainer.GetComponentsInChildren<Transform>();
        m_waypoints = new Transform[potentialWaypoints.Length - 1];

        for (int i = 1; i < potentialWaypoints.Length; i++)
        {
            m_waypoints[i-1] = potentialWaypoints[i];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetWaypoints();
        m_proximitySqr = m_waypointProximity * m_waypointProximity;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 waypointPosition = m_waypoints[m_currentWaypoint].position;
        Vector3 relativeWaypointPos = transform.InverseTransformPoint(
            new Vector3(
                waypointPosition.x,
                transform.position.y,
                waypointPosition.z));

        CheckWaypointPosition(relativeWaypointPos);
    }

    private void CheckWaypointPosition(Vector3 relativeWaypointPos)
    {
        if (relativeWaypointPos.sqrMagnitude < m_proximitySqr)
        {
            m_currentWaypoint += 1;
            waypointsHit += 1;

            if (m_currentWaypoint >= m_waypoints.Length)
            {
                m_currentWaypoint = 0;
                RaceManager.Instance.LapFinishedByAI(this);
            }
        }
    }

}
