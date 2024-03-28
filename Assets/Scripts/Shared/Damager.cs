using System;
using UnityEngine;

public class Damager : MonoBehaviour, IExplodable
{
    public int PlayerId { get; set; }
    public event Action<Vector3, int> OnExplode;

    [SerializeField]
    private bool shouldDisableOnHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        OnExplode?.Invoke(collision.contacts[0].normal, PlayerId);

        if (collision.gameObject.TryGetComponent<Damageable>(out var damageable))
            damageable.Hit(PlayerId);

        if (shouldDisableOnHit)
            transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnExplode?.Invoke(Vector3.up, PlayerId);

        if (other.gameObject.TryGetComponent<Damageable>(out var damageable))
            damageable.Hit(PlayerId);

        if (shouldDisableOnHit)
            transform.gameObject.SetActive(false);
    }
}
