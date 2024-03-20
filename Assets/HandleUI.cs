using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandleUI : MonoBehaviour
{
    [SerializeField] TMP_Text lapCounter;
    [SerializeField] TMP_Text PlacementPhrase;
    RaceManager rm;

    // Start is called before the first frame update
    void Start()
    {
        rm = RaceManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        lapCounter.text = (rm.playerLaps + 1).ToString();
        PlacementPhrase.text = (rm.placementPhrase);
    }
}
