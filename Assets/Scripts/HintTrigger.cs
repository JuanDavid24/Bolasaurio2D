using System.Collections.Generic;
using UnityEngine;

public class HintTrigger : MonoBehaviour
{
    public Collider2D Col { get; private set; }
    public HintController HintPanel;
    [SerializeField] private List<string> _hintSentences;
    private bool _isPlayerInRange = false;
    [SerializeField] private float _sentenceDelay = 2f;
    [SerializeField] private bool _playOnce = false;
    private bool _justPlayed = false;
    void Start()
    {
        Col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) =>
        _isPlayerInRange = collision.CompareTag("Player");

    private void OnTriggerExit2D(Collider2D collision)
    {
        _isPlayerInRange = !collision.CompareTag("Player");
        _justPlayed = false;
    }

    void Update()
    {
        if (!_isPlayerInRange) 
            return;
        if (!HintPanel.IsPanelActive && !_justPlayed)
        {
            HintPanel.InitializeHintPanel(_hintSentences, _sentenceDelay);
            _justPlayed = true;
            if (_playOnce)
                gameObject.SetActive(false);
        }
    }
}
