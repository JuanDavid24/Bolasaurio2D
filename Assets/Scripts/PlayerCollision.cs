using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Colision con enemigo: " + collision.gameObject.name);
            player.OnAttacked(collision.gameObject.GetComponent<EnemyOrc>().damage, collision.transform.position);

        }
    }
}
