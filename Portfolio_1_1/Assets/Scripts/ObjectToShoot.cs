using UnityEngine;

public class ObjectToShoot : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 5f);
    }
}
