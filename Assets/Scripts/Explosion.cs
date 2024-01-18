using UnityEngine;

[RequireComponent(typeof(IExplodable), typeof(AudioSource))]
public class Explosion : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionPrefab;

    private ParticleSystem explosion;
    private IExplodable explodable;

    private void Start()
    {
        explosion = Instantiate(explosionPrefab.gameObject).GetComponent<ParticleSystem>();
        explosion.gameObject.SetActive(false);

        explodable = GetComponent<IExplodable>();
        explodable.OnExplode += CreateExplosion;
    }
    private void CreateExplosion(Vector3 hitNormal)
    {
        explosion.gameObject.SetActive(true);
        explosion.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        explosion.transform.forward = hitNormal;
        explosion.Play();
    }
}
