using UnityEngine;

public class HpManagerPlayer : HpManager
{
    [SerializeField] GameObject hpBar;
    private HpUI hpUI;
    protected override void Awake()
    {
        base.Awake();
        hpUI = hpBar.GetComponent<HpUI>();
        hpUI.SetMaxHP(maxHP);
        hpUI.UpdateHP(Hp);
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        hpUI.UpdateHP(Hp);
    }

    public void Heal(int amount)
    {
        ChangeHp(amount);
        hpUI.UpdateHP(Hp);
    }

    public override void Die() => DestroyAfterDie();
}
