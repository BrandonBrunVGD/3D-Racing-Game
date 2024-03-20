using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBobbing : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float bobRate;
    [SerializeField] private float speed = 1;

    private bool isBobbing = false;
    private bool bobUp = true;
    private float bobTime;
    void Update() {
        bobTime += Time.deltaTime;

        if (isBobbing) {
            if (bobUp) { rb.MovePosition(transform.position + Vector3.up * Time.deltaTime * speed);}
            else if (!bobUp) { rb.MovePosition(transform.position + Vector3.down * Time.deltaTime * speed);}
        }

        if (bobTime >= bobRate) {
            bobUp = !bobUp;
            bobTime = 0;
            isBobbing = true;
        }
    }
}
