using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{   
    [SerializeField] GameObject Right;
    [SerializeField] GameObject Left;
    [SerializeField] Renderer car;
    [SerializeField] Transform trackSelector;
    [SerializeField] Canvas[] canvases;
    public int menuColorSwitch = 0;
    private string trackName = "track1";


    // Start is called before the first frame update
    void Start()
    {
        canvases[0].enabled = true;
        canvases[1].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(menuColorSwitch) {
            case 0:
            car.material.color = Color.white;
            break;
            case 1:
            car.material.color = Color.black;
            break;
            case 2:
            car.material.color = Color.green;
            break;
            case 3:
            car.material.color = Color.blue;
            break;
            case 4:
            car.material.color = Color.red;
            break;
            default:
            car.material.color = Color.white;
            break;
        }

        if (menuColorSwitch >= 5) {
            menuColorSwitch = 0;
        }
        else if (menuColorSwitch < 0) {
            menuColorSwitch = 4;
        }
    }

    public void OnRightClick() {
        menuColorSwitch += 1;  
        HandleColors.colorSwitch += 1;
    }

    public void OnLeftClick() {
        menuColorSwitch -= 1;  
        HandleColors.colorSwitch -= 1;    
    }

    public void OnClickGo() {
        SceneManager.LoadScene(trackName);
    }

    public void OnClickTrack1() {
        trackName = "track1";
        trackSelector.localPosition = new Vector3(-150, 0, 0);
    }

    public void OnClickTrack2() {
        trackName = "track2";
        trackSelector.localPosition = new Vector3(150, 0, 0);
    }

    public void OnClickSelectTrack() {
        canvases[0].enabled = false;
        canvases[1].enabled = true;
    }
}
