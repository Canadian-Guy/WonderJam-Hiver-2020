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

    [Tooltip("bool for night and day. animation stuff")]
    public bool Night;

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

        //la jam commence a 16H
        if(CurrentTime < 90 && CurrentTime > 70)
        { //si le temps est en dessous de 90 c'est la nuit 22H à 8H
            Night = true;
        }

        if(CurrentTime < 70 && CurrentTime > 42)
        { //si le temps est en dessous de 70 mais au dessus de 42 c'est le jour de 8H à 22H
            Night = false;
        }

        if(CurrentTime < 42 && CurrentTime > 22)
        { //si le temps est en dessous de 42 mais au dessus de 22 c'est la nuit 22H à 8H
            Night = true;
        }

        if(CurrentTime < 22 && CurrentTime > 0)
        { //si le temps est en dessous de 22 mais au dessus de 0 c'est les dernier temps de la jam OwO
            Night = false;
        }

        if (CurrentTime == 0)
        {
            //TODO: Appeler le script qui s'occupe de la fin de la partie pour trigger la fin de la partie :)
        }


    }
}
