using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Target" || other.tag == "Player")
            UI.instance.OpenEndScreen();
    }
}