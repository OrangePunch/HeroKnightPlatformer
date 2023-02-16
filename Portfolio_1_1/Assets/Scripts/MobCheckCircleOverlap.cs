using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using System.Linq;

public class MobCheckCircleOverlap : MonoBehaviour
{
    [SerializeField] private float _radius = 1f;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private string[] _tags;
    [SerializeField] private UnityEvent<GameObject> _onOverlap;

    private readonly Collider2D[] _interactionResult = new Collider2D[10];
    private Hero _hero;

    private void Awake()
    {
        _hero =FindObjectOfType<Hero>().GetComponent<Hero>();
    }

    private void OnDrawGizmosSelected()
    {
        //Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
    }

    public void Check()
    {
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _interactionResult, _mask);

        for (var i = 0; i < size; i++)
        {
            var overlapResult = _interactionResult[i];
            var isInTags = _tags.Any(tag => overlapResult.CompareTag(tag));

            if (isInTags && !_hero.m_combatIdle)
            {
                _onOverlap?.Invoke(overlapResult.gameObject);
            }
        }
    }
}
