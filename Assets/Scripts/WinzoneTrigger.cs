using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWinzone : MonoBehaviour
{
    [SerializeField] private GameObject winZone;
    private Collider2D _col;
    // Start is called before the first frame update
    void Start()
    {
        if (winZone == null)
            winZone = GameObject.Find("WinZone");
        _col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (winZone != null)
                winZone.SetActive(true);
        }
    }    
}
