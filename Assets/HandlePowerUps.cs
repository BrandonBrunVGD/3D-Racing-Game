using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePowerUps : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform transform;
    public bool activateSpeedBoost = false;
    public bool startSpinningOut = false;
    private float speedTimer;
    private float speedTimerTime = 3.0f;
    private float spinTimer;
    private float spinTimerTime = 1.5f;
    private float spinVal = 10;

    void Update() {
        SpeedBoost();
        SpinOut();
    }

    public void SpeedBoost() {
        if (activateSpeedBoost) {
            speedTimer += 1 * Time.deltaTime;
            if (speedTimer < speedTimerTime) {
                rb.velocity *= 1.005f;
            }
            else {
                activateSpeedBoost = false;
                speedTimer = 0;
            }
        }   
    }

    public void SpinOut() {
        
        if (startSpinningOut) {
            Debug.Log("SPINNING OUT");
            spinTimer += 1 * Time.deltaTime;
            if (spinTimer < spinTimerTime) {
                transform.Rotate(0, spinVal, 0, Space.World);
                spinVal -= 0.05f;
            }
            else {
                startSpinningOut = false;
                spinTimer = 0;
                spinVal = 10;
            }
        }   
    }
}
