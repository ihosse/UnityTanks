using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionPrefab;
    private ParticleSystem explosion;

    private void Start()
    {
        explosion = Instantiate(explosionPrefab.gameObject).GetComponent<ParticleSystem>();
        explosion.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Damageable>(out var damageable))
        {
            damageable.Hit();
        }
        CreateExplosionEffect(collision);
        transform.gameObject.SetActive(false);
    }

    private void CreateExplosionEffect(Collision collision)
    {
        Vector3 collisionNormal = collision.GetContact(0).normal;

        explosion.gameObject.SetActive(true);

        explosion.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        explosion.transform.forward = collisionNormal;
        explosion.Play();
    }
}