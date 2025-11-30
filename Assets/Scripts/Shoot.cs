using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject obj;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ShootBullet();
        }
    }
    private void ShootBullet()
    {
            GameObject newBullet = Instantiate(obj, transform.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().direction = transform.localScale.x > 0 ? 1 : -1;
    }
}
