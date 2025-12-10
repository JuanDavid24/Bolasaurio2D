using UnityEngine;

public class HpManager : MonoBehaviour
{
    [SerializeField] protected int maxHP = 6;
    public bool isAlive;
    private int _hp;
    public int Hp
    {
        get { return _hp; }
        private set
        {
            _hp = Mathf.Clamp(value, 0, maxHP);
        }
    }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if(Hp == 0) 
            ChangeHp(maxHP);

        print("Vida actual: " + Hp);
        isAlive = true;
    }

    public void ChangeHp(int value)
    {
        _hp = Hp + value;
    }

    public virtual void TakeDamage(int dmg)
    {
        ChangeHp(-dmg);
        Debug.Log("Se ha recibido daño: " + dmg + ". Vida restante: " + Hp);
        CheckIsAlive();
    }

    void CheckIsAlive()
    {
        isAlive = Hp > 0;
    }

    public virtual void Die()
    {
        Debug.Log(gameObject.name + " se murió XD");
        //gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Animator>().SetTrigger("die");
    }

    public void DestroyAfterDie() => Destroy(gameObject);

    void Update()
    {
        if (!isAlive)
            Die();
    }
}
