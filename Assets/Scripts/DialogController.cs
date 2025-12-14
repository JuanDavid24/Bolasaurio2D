using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public Animator animator { get; private set; }
    private Queue<string> _dialogSentences;
    public bool IsPanelActive = false;

    private void Awake()
    {
        _dialogSentences = new Queue<string>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        dialogText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void InitializeDialogPanel(List<string> dialog)
    {
        if (IsPanelActive)
            return;

        _dialogSentences.Clear();
        if (dialog.Count < 1)
        {
            IsPanelActive = false;
            return;
        }
        foreach (string dialogLine in dialog)
        {
            _dialogSentences.Enqueue(dialogLine);
        }
        animator.SetTrigger("openDialog");
    }

    public void ShowNextSentence()
    {
        if (_dialogSentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string _sentence = _dialogSentences.Dequeue();
        dialogText.text = _sentence;
        IsPanelActive = true;
    }

    private void EndDialog()
    {
        IsPanelActive = false;
        animator.SetTrigger("closeDialog");
        dialogText.text = "";
    }

   public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPanelActive)
        {
            ShowNextSentence();
        }
    }
}
