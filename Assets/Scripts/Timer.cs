using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private int ta;
    public float timeLeft;
    public bool timerOn;

    // Start is called before the first frame update
    void Start()
    {
        timerOn = true;
        ta = (int)timeLeft + 1;
    }

    public void Restart(float newTime)
    {
        timerOn = false;
        timeLeft = newTime;
        timerOn = true;
    }
    private void CheckTime()
    {
        if (timerOn)
        {
            if (timeLeft > 0)
            { 
                timeLeft -= Time.deltaTime;
                //Debug.Log("Tiempo restante: " + (int)timeLeft + "segundos");
            }
            else
            {
                //Debug.Log("Se acabo el tiempo");
                timeLeft = 0;
                timerOn = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckTime(); 
    }
}
