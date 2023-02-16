using UnityEngine;

public class FloorTrap : MonoBehaviour
{
    [SerializeField] private float _timeToDestroy;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, _timeToDestroy);
        }
    }
}
