using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform firePoint;

    //[SerializeField] SpriteRenderer alarmLight;

    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float fireRate;
    [SerializeField] private float force;
    [SerializeField] private float range = 6f;
    [SerializeField] private string _tag;

    private float nextTimeToFire = 2f;
    private bool detected;
    private Vector2 direction;

    void Update()
    {
        Vector2 targetPos = target.position;
        direction = targetPos - (Vector2)transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, range);

        if (rayInfo)
        {
            if (rayInfo.collider.gameObject.tag == _tag)
            {
                if (!detected)
                {
                    detected = true;
                    //alarmLight.color = Color.red;
                }
            }
            else
            {
                if (detected)
                {
                    detected = false;
                    //alarmLight.color = Color.green;
                }
            }
        }
        if (detected)
        {
            gun.transform.right = direction;
            if (Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bulletIns = Instantiate(bullet, firePoint.position, Quaternion.identity);
        bulletIns.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
