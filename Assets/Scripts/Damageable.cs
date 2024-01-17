using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionPrefab;
    private ParticleSystem explosion;

    public UnityEvent OnKill;

    private void Start()
    {
        explosion = Instantiate(explosionPrefab.gameObject).GetComponent<ParticleSystem>();
        explosion.gameObject.SetActive(false);

    }
    public void Hit()
    {
        CreateExplosionEffect();
        gameObject.SetActive(false);

        OnKill!.Invoke();
    }

    private void CreateExplosionEffect()
    {
        explosion.gameObject.SetActive(true);

        explosion.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        explosion.Play();
    }
}
