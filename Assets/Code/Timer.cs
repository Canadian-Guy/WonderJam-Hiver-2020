using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/*
 * Scripts that handles a count down timer (Tick, update UI)
 */

public class Timer : MonoBehaviour
{
    [Tooltip("Initial time in seconds")]
    public float CurrentTime = 96f;

    [Tooltip("Text component to display the timer")]
    public TMP_Text TimeText;

    [Tooltip("Ajustment to the time tick. 2 is 2x faster")]
    public float TimeMod = 1;
    // Start is called before the first frame update
    void Start()
    {
        TimeText.text = CurrentTime.ToString("0.0");
    }

    // Update is called once per frame
    void Update()
    {
        //On arrete de tick à 0. 
        if(CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime * TimeMod;
            //Si on a ete trop loins, on met le shit à 0 :D :D :D
            if(CurrentTime < 0)
            {
                CurrentTime = 0;
            }
            TimeText.text = CurrentTime.ToString("0.0");
        }
        if(CurrentTime == 0)
        {
            //TODO: Appeler le script qui s'occupe de la fin de la partie pour trigger la fin de la partie :)
        }


    }
}
