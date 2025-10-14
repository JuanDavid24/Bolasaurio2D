using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : MonoBehaviour
{
    private int hp = 100;
    [SerializeField] private int dmg = 10;
    private bool berserkOn;
    [SerializeField] private Timer timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!berserkOn && timer.timeLeft <= 0)
        {
            berserkOn = true;
            Debug.Log("Modo salvaje");
            dmg *= 2;
        }
    }
}
