using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleColors : MonoBehaviour
{
    public static int colorSwitch = 0;
    [SerializeField] Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeColor()
    {
        switch(colorSwitch) {
            case 0:
            renderer.material.color = Color.white;
            break;
            case 1:
            renderer.material.color = Color.black;
            break;
            case 2:
            renderer.material.color = Color.green;
            break;
            case 3:
            renderer.material.color = Color.blue;
            break;
            case 4:
            renderer.material.color = Color.red;
            break;
            default:
            renderer.material.color = Color.white;
            break;
        }
    }
}
