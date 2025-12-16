using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Collider2D Col { get; private set; }
    public DialogController DialogPanel;
    [SerializeField] private List<string> _dialog;
    private bool _isPlayerInRange = false;
    [SerializeField] private Player _player;
    [SerializeField] private bool _playOnce = false;
    void Start()
    {
        Col = GetComponent<Collider2D>();
        if (_player == null)
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerInRange = true;
            _player.SetIconVisibility(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerInRange = false;
            _player.SetIconVisibility(false);
        }
    }

    public void TriggerDialog() => DialogPanel.InitializeDialogPanel(_dialog);

    void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerDialog();
            if (_playOnce)
                Destroy(this.gameObject, 2f);
        }
    }
}
