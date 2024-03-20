using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    [SerializeField] public HandlePowerUps handlePowerUps;
    RaceManager rm;

    void Start() {
        rm = RaceManager.Instance;
    }

    void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Player") {
            
            if (transform.tag == "SpeedBoost") {
                rm.ActivateSpeedBoost();
                Destroy(gameObject);
            }
            else if (transform.tag == "Oil") {
                Debug.Log("HIT");
                rm.SpinOut();
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag == "Player") {
            if (transform.tag == "Oil") {
                Debug.Log("HIT");
                rm.SpinOut();
            }
        }
    }
}
