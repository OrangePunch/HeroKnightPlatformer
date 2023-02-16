using UnityEngine;

public class SawTrapMoving : MonoBehaviour
{
    [SerializeField] private WayPointPath _waypointPath;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private int _targetWaypointIndex;
    private float _timeToWaypoint;
    private float _elapsedTime;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private void Start()
    {
        TargetNextWayypoint();
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector2.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        transform.Rotate(0, 0, _rotationSpeed);

        if (elapsedPercentage >= 1)
        {
            TargetNextWayypoint();
        }
    }

    private void TargetNextWayypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0f;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.SetParent(transform);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        other.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
