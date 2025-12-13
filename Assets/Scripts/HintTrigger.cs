using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    public Collider2D Col { get; private set; }
    public HintController HintPanel;
    [SerializeField] private List<string> _hintSentences;
    private bool _isPlayerInRange = false;
    private bool _isHintNew = true;

    void Start()
    {
        Col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) =>
        _isPlayerInRange = collision.CompareTag("Player");

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _isPlayerInRange = false;
    }

    void Update()
    {
        if(_isHintNew)
        {
            if (!HintPanel.IsPanelActive && _isPlayerInRange)
            {
                HintPanel.InitializeHintPanel(_hintSentences);
                _isHintNew = false;
            }
        }
    }
}
