using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountPanelUI : MonoBehaviour
{
    [SerializeField] private string _itemName;
    private TextMeshProUGUI _countTxt;

    private int _maxStack = 99;
    private int _count;

    private void Start()
    {
        if (_countTxt == null)
            _countTxt = GetComponentInChildren<TextMeshProUGUI>();
        _countTxt.text = $"x{_count.ToString()}";
    }

    public void AddQuantity(string name, int quantity)
    {
        print("CountPanelUI - AddQuantity called for item: " + name + " with quantity: " + quantity);
        if (name != _itemName) return;
        _count = Mathf.Clamp(quantity + _count, 0, _maxStack);
        _countTxt.text = $"x{_count.ToString()}";
    }
}