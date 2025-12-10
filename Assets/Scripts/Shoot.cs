using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private float _cooldown;
    [SerializeField] private Timer _cooldownTimer;

    void Start()
    {
        _cooldownTimer.Restart(0);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && !_cooldownTimer.timerOn)
        {
            ShootBullet();
            _cooldownTimer.Restart(_cooldown);
        }
    }
    private void ShootBullet()
    {
            GameObject newBullet = Instantiate(obj, transform.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().direction = transform.localScale.x > 0 ? 1 : -1;
    }
}
