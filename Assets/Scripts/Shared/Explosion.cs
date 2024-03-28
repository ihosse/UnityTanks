using UnityEngine;

[RequireComponent(typeof(IExplodable), typeof(AudioSource))]
public class Explosion : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionPrefab;

    private ParticleSystem explosion;
    private IExplodable explodable;

    private Damager explosionDamager;

    private void Start()
    {
        explosion = Instantiate(explosionPrefab.gameObject).GetComponent<ParticleSystem>();
        explosion.gameObject.SetActive(false);

        explosionDamager = explosion.gameObject.GetComponentInChildren<Damager>();

        explodable = GetComponent<IExplodable>();
        explodable.OnExplode += CreateExplosion;
    }
    private void CreateExplosion(Vector3 hitNormal, int playerId)
    {
        if (explosionDamager != null)
        {
            explosionDamager.PlayerId = playerId;
        }


        explosion.gameObject.SetActive(true);
        explosion.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        explosion.transform.forward = hitNormal;
        explosion.Play();
    }
}
