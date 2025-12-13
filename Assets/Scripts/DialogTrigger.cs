using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Collider2D Col { get; private set; }
    public DialogController DialogPanel;
    [SerializeField] private List<string> _dialog;
    private bool _isPlayerInRange = false;

    void Start()
    {
        Col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            _isPlayerInRange = false;
    }

    void Update()
    {
        if (!DialogPanel.IsPanelActive)
        {
            if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
                DialogPanel.InitializeDialogPanel(_dialog);
        }
    }
}
