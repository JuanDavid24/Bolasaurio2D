using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintController : MonoBehaviour
{
    public TextMeshProUGUI hintTextbox;
    public Animator animator { get; private set; }
    private Queue<string> _hintSentences;
    public bool IsPanelActive = false;
    private Coroutine _currentCoroutine;

    private void Awake()
    {
        _hintSentences = new Queue<string>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        hintTextbox = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void InitializeHintPanel(List<string> hints, float _hintDelay)
    {
        if (hints.Count < 1)
        {
            IsPanelActive = false;
            return;
        }

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _hintSentences.Clear();

        foreach (string hint in hints)
        {
            _hintSentences.Enqueue(hint);
        }
        animator.SetTrigger("openHint");
        IsPanelActive = true;

        _currentCoroutine = StartCoroutine(ShowHint(_hintDelay));
    }

    IEnumerator ShowHint(float _hintDelay)
    {
        while(_hintSentences.Count > 0)
        {
            ShowNextSentence();
            yield return new WaitForSeconds(_hintDelay);
        }
        EndHint();
    }

    private void ShowNextSentence()
    {
        string _sentence = _hintSentences.Dequeue();
        hintTextbox.text = _sentence;
    }

    private void EndHint()
    {
        IsPanelActive = false;
        animator.SetTrigger("closeHint");
        hintTextbox.text = "";
    }
}
