using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Hp : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int hp;
    bool isAlive;

    // Start is called before the first frame update
    private void Start()
    {
        ChangeHp(maxHP);
        print("Vida actual: " + hp);
        isAlive = true;
    }

    void ChangeHp(int value)
    {
        int newHp = hp + value;
        hp = Mathf.Clamp(newHp, 0, maxHP);
    }

    public void takeDamage(int dmg)
    {
        ChangeHp(-dmg);
        Debug.Log("Se ha recibido daño: " + dmg + ". Vida restante: " + hp);
        CheckIsAlive();
    }

    void CheckIsAlive()
    {
        isAlive = hp > 0;
    }

    void Die()
    {
        Debug.Log(gameObject.name + " se murió XD");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            // muerte
            Die();
        }
    }
}
