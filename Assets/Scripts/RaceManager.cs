using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.Utility;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class RaceManager : MonoBehaviour
{
    public UnityEvent resetWaypointsHit = new UnityEvent();
    public CarUserControl playerCar;    
    private Rigidbody playerBody;
    public AICar[] aiCars;
    //public PlayerWaypointProgressTracker PlayerWaypointTracker;
    public float respawnDelay = 15f;
    public float distanceToCover = 1f;
    private Rigidbody[] aiBodies;
    public float[] respawnCounters;
    public float[] distancesLeft;
    public Transform[] waypoints;

    [SerializeField]
    private int[] laps;

    public int requiredLaps = 3;
    public GameObject checkpointContainer;

    [SerializeField]
    public int playerLaps = 0;
    [SerializeField]
    private int currentCheckpoint = 0;
    private Checkpoint[] checkpoints;
    public Checkpoint endPoint;
    public int[] carWaypoints;
    public int[] racePositions;
    public int playerRacePosition;
    public string placementPhrase = "TH";
    public int colorSwitch = 0;
    [SerializeField] Spawner[] PUSpawners;
    [SerializeField] HandlePowerUps handlePU;
    public int playerWaypointsHit = 0;
    public static RaceManager Instance { get; private set; } = null;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        } else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        carWaypoints = new int[4] {1, 2, 3, 4};
        racePositions = new int[4] {1, 2, 3, 4};

        aiBodies = new Rigidbody[aiCars.Length];
        respawnCounters = new float[aiCars.Length];
        distancesLeft = new float[aiCars.Length];
        waypoints = new Transform[aiCars.Length];

        laps = new int[aiCars.Length];

        for(int i=0; i < aiCars.Length; i++)
        {
            aiBodies[i] = aiCars[i].gameObject.GetComponent<Rigidbody>();
            respawnCounters[i] = respawnDelay;
            distancesLeft[i] = float.MaxValue;
            laps[i] = 0;
        }

        checkpoints = checkpointContainer.GetComponentsInChildren<Checkpoint>();

        playerBody = playerCar.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
        int carsFinished = 0;

        for(int i=0; i < aiBodies.Length; i++)
        {
            Transform nextWaypoint = aiCars[i].CurrentWaypoint;

            float distanceCovered = (nextWaypoint.position - aiBodies[i].position).magnitude;

            if (distancesLeft[i] - distanceToCover > distanceCovered ||
                waypoints[i] != nextWaypoint)
            {
                waypoints[i] = nextWaypoint;
                respawnCounters[i] = respawnDelay;
                distancesLeft[i] = distanceCovered;
            } else
            {
                respawnCounters[i] -= Time.deltaTime;

                if (respawnCounters[i] <= 0)
                {
                    respawnCounters[i] = respawnDelay;
                    distancesLeft[i] = float.MaxValue;
                    aiBodies[i].velocity = Vector3.zero;
                    Transform lastWaypoint = aiCars[i].LastWaypoint;
                    aiBodies[i].position = lastWaypoint.position;
                    aiBodies[i].rotation = Quaternion.LookRotation(nextWaypoint.position - lastWaypoint.position);
                }
            }       
            
            carWaypoints[i] = aiCars[i].waypointsHit;

            if (laps[i] >= requiredLaps)
            {
                carsFinished += 1;
            }
        }

        carWaypoints[3] = playerWaypointsHit;
        for (int i = 0; i < carWaypoints.Length; i++) {
            racePositions[i] = carWaypoints[i];
        }
        Array.Sort(racePositions);
        int val = playerWaypointsHit;

        if (System.Array.IndexOf(racePositions, val) == 0) {
            playerRacePosition = 4;
            placementPhrase = "4TH";
        }
        else if (System.Array.IndexOf(racePositions, val) == 1) {
            playerRacePosition = 3;
            placementPhrase = "3RD";
        }
        else if (System.Array.IndexOf(racePositions, val) == 2) {
            playerRacePosition = 2;
            placementPhrase = "2ND";
        }
        else if ((System.Array.IndexOf(racePositions, val) == 3)) 
        {
            playerRacePosition = 1;
            placementPhrase = "1ST";
        }
        else {
            playerRacePosition = 00;
            placementPhrase = "00";
        }


        if (carsFinished == aiCars.Length || playerLaps >= requiredLaps)
        {
            RaceFinishedUI.FinishedPOSPhrase = placementPhrase;
            SceneManager.LoadScene("RaceFinished");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerBody.velocity = Vector3.zero;
            Vector3 nextCheckpoint = checkpoints[currentCheckpoint].transform.position;
            Vector3 lastCheckpoint = checkpoints[currentCheckpoint > 0 ? currentCheckpoint - 1 : checkpoints.Length - 1].transform.position;
            playerBody.position = lastCheckpoint;
            playerBody.rotation = Quaternion.LookRotation(nextCheckpoint - lastCheckpoint);
        }
    }

    public void LapFinishedByAI(AICar car)
    {
        int i = Array.FindIndex(aiCars, element => element == car);
        if (i != -1)
        {
            laps[i] += 1;
        }
    }

    public void PlayerCheckpoint(Checkpoint point)
    {
        if (point == checkpoints[currentCheckpoint]) 
        {
            currentCheckpoint += 1;
            //Debug.Log("Player passed checkpoint " + currentCheckpoint.ToString());
            if (currentCheckpoint == checkpoints.Length)
            {
                currentCheckpoint = 0;
                playerLaps += 1;
                
                PUSpawners[0].SpawnObject();
                PUSpawners[1].SpawnObject();
                PUSpawners[2].SpawnObject();
                PUSpawners[3].SpawnObject();  
                resetWaypointsHit.Invoke();     
            }
        }
    }

    public void ActivateSpeedBoost() {
        handlePU.activateSpeedBoost = true;
    }

    public void SpinOut() {
        handlePU.startSpinningOut = true;
    }
}
