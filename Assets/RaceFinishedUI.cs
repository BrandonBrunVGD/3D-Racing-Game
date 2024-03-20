using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RaceFinishedUI : MonoBehaviour
{
    public static string FinishedPOSPhrase = "4TH";
    [SerializeField] GameObject[] cars;
    [SerializeField] Vector3[] PlacementPOS;
    [SerializeField] TMP_Text youPlaced;

    // Start is called before the first frame update
    void Start()
    {
        switch (FinishedPOSPhrase) {
            case "1ST":
                cars[0].transform.localPosition = PlacementPOS[0];
                cars[1].transform.localPosition = PlacementPOS[1];
                cars[2].transform.localPosition = PlacementPOS[2];
                cars[3].transform.localPosition = PlacementPOS[3];
                cars[0].transform.eulerAngles = new Vector3(0, -30, 0);
                cars[3].transform.eulerAngles = new Vector3(0, 30, 0);
            break;
            case "2ND":
                cars[0].transform.localPosition = PlacementPOS[1];
                cars[1].transform.localPosition = PlacementPOS[0];
                cars[2].transform.localPosition = PlacementPOS[2];
                cars[3].transform.localPosition = PlacementPOS[3];
                cars[1].transform.eulerAngles = new Vector3(0, -30, 0);
                cars[3].transform.eulerAngles = new Vector3(0, 30, 0);
            break;
            case "3RD":
                cars[0].transform.localPosition = PlacementPOS[2];
                cars[1].transform.localPosition = PlacementPOS[0];
                cars[2].transform.localPosition = PlacementPOS[1];
                cars[3].transform.localPosition = PlacementPOS[3];
                cars[1].transform.eulerAngles = new Vector3(0, -30, 0);
                cars[3].transform.eulerAngles = new Vector3(0, 30, 0);
            break;
            case "4TH":
                cars[0].transform.localPosition = PlacementPOS[3];
                cars[1].transform.localPosition = PlacementPOS[0];
                cars[2].transform.localPosition = PlacementPOS[1];
                cars[3].transform.localPosition = PlacementPOS[2];
                cars[1].transform.eulerAngles = new Vector3(0, -30, 0);
                cars[0].transform.eulerAngles = new Vector3(0, 30, 0);
            break;
            default:
            break;
        }
        youPlaced.text = "You Placed " + FinishedPOSPhrase;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickContinue() {
        SceneManager.LoadScene("Menu");
    }
}
