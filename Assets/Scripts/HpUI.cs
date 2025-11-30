using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HpUI : MonoBehaviour
{
    private List<Image> images = new List<Image>();
    [SerializeField] private string elementName;

    private int maxHp;
    private int currentHp;
    private float barScale = 0.5f; // minimum bar division

    public void SetMaxHP(int maxHp)
    {
        this.maxHp = maxHp;
    }
    public void SetCurrentHP(int maxHp)
    {
        this.maxHp = maxHp;
    }

    private void Start()
    {
        foreach (Transform heart in transform)
        {
            Transform elem = heart.Find(elementName);
            if (elem != null)
            {
                images.Add(elem.GetComponent<Image>());
            }
        }
        UpdateBar();
    }
    public void UpdateHP(int amount)
    {
        currentHp = Mathf.Clamp(amount, 0, maxHp);
        UpdateBar();
    }

    private void UpdateBar()
    {
        float barFill = currentHp * barScale;
        foreach (Image image in images)
        {
            if (barFill >= 1)
                image.fillAmount = 1f;
            else 
                image.fillAmount = barFill > 0 ? 0.5f : 0;

            barFill--;
        }
    }
}