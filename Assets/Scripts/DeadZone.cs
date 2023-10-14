using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Target") || other.tag.Equals("Player"))
        {
            UI.instance.OpenEndScreen();
        }
    }
}
