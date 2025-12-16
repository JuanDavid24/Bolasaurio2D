using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeliverItems : MonoBehaviour
{
    public Scenes sceneManager;
    private Collider2D col;
    [SerializeField] private List<string> requiredQuestItems;
    private bool _hasAllQuestItems = false;
    [SerializeField] private List<string> _victoryDialog;
    [SerializeField] private List<string> _notYetDialog;
    [SerializeField] private DialogController _dialogPanel;
    private bool _victory = false;

    public bool HasAllQuestItems => _hasAllQuestItems;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject _player = collision.gameObject;
            CheckQuestItems(_player);
        }
    }

    private IEnumerator Victory() 
    {
        _dialogPanel.InitializeDialogPanel(_victoryDialog);
        yield return new WaitForSeconds(3f);
        _victory = true;
    }

    private void CheckQuestItems(GameObject player)
    {
        ItemManager itemMgr = player.GetComponent<ItemManager>();
        if (itemMgr != null)
        {
            int item1Count = itemMgr.GetItemCount(requiredQuestItems[0]);
            _hasAllQuestItems = item1Count > 100;
            //print($"ferrous ore count: {item1Count}");
        }

        if (_hasAllQuestItems)
            StartCoroutine(Victory());
        else
            _dialogPanel.InitializeDialogPanel(_notYetDialog);
    }
    private void Update()
    {
        if (_victory && !_dialogPanel.IsPanelActive)
        {
            Debug.Log("volver a menu principal");
            sceneManager.ChangeScene("StartMenu");
        }
    }
}
