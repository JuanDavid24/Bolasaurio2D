using UnityEngine;

public class Hp : MonoBehaviour
{
    [SerializeField] protected int maxHP = 6;
    public int hp;
    bool isAlive;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if(hp == 0) 
            ChangeHp(maxHP);

        print("Vida actual: " + hp);
        isAlive = true;
    }

    public void ChangeHp(int value)
    {
        int newHp = hp + value;
        hp = Mathf.Clamp(newHp, 0, maxHP);
    }

    public virtual void TakeDamage(int dmg)
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
