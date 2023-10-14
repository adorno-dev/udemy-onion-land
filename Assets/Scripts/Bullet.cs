using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    private void Update() => transform.right = rb.velocity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Target")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            UI.instance.AddScore();
        }
    }
}
