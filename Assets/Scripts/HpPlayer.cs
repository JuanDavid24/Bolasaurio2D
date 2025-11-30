using UnityEngine;

public class HpPlayer : Hp
{
    [SerializeField] GameObject hpBar;
    private HpUI hpUI;
    protected override void Awake()
    {
        base.Awake();
        hpUI = hpBar.GetComponent<HpUI>();
        hpUI.SetMaxHP(maxHP);
        hpUI.UpdateHP(hp);
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        hpUI.UpdateHP(hp);
    }
}
