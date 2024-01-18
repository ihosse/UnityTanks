using System;
using UnityEngine;

public class Damager : MonoBehaviour, IExplodable
{
    public event Action<Vector3> OnExplode;

    [SerializeField]
    private bool shouldDisableOnHit = true;

    private void OnCollisionEnter(Collision collision)
    {
        OnExplode!.Invoke(collision.contacts[0].normal);

        if (collision.gameObject.TryGetComponent<Damageable>(out var damageable))
        {
            damageable.Hit();
        }

        if (shouldDisableOnHit)
            transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(OnExplode != null)
            OnExplode(Vector3.up);
        
        if (other.gameObject.TryGetComponent<Damageable>(out var damageable))
        {
            damageable.Hit();
        }

        if (shouldDisableOnHit)
            transform.gameObject.SetActive(false);
    }
}
