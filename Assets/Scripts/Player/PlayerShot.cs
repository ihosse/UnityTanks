using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerInput), typeof(PlayerSound))]
public class PlayerShot : MonoBehaviour
{
    public bool CanShot { get; set; }

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform shotPositionReference;

    [SerializeField]
    private float bulletImpulse = 15;

    private Rigidbody bullet;
    private PlayerSound playerSound;

    public void CreateShot(int playerId)
    {
        bullet = Instantiate(bulletPrefab).GetComponent<Rigidbody>();
        bullet.gameObject.SetActive(false);

        bullet.gameObject.GetComponent<Damager>().PlayerId = playerId;

        playerSound = GetComponent<PlayerSound>();
    }

    public void OnShot(InputAction.CallbackContext context)
    {
        if (bullet == null || CanShot == false || bullet.gameObject.activeInHierarchy)
            return;

        playerSound.PlayFiringSound();

        if(context.phase == InputActionPhase.Performed)
        {
            bullet.gameObject.SetActive(true);

            bullet.velocity = Vector3.zero;
            bullet.angularVelocity = Vector3.zero;

            bullet.transform.SetPositionAndRotation(shotPositionReference.position, shotPositionReference.rotation);
            bullet.AddForce(shotPositionReference.forward * bulletImpulse, ForceMode.Impulse);
        }
    }
}
