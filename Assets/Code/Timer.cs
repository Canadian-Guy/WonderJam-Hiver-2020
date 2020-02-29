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

    [Tooltip("The handler used to transition between the game and the end screen")]
    public EndGameHandler EndGameHandler;

    void Start()
    {
        TimeText.text = CurrentTime.ToString("0.0");
    }

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
            EndGameHandler.EndGame();
        }
    }
}
