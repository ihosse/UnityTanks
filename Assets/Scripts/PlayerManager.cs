using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerColor))]
[RequireComponent(typeof(PlayerShot), typeof(PlayerSound))]
public class PlayerManager : MonoBehaviour
{
    public Action OnKill;

    public bool isInvencible { get; set; }

    public bool IsDead { get; private set; }

    public PlayerMovement playerMovement {  get; private set; }

    public PlayerShot playerShot { get; private set; }

    [SerializeField]
    private GameObject tankRenreder;

    private PlayerSound playerSound;

    private PlayerColor playerColor;
    private Color tankColor;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShot = GetComponent<PlayerShot>();
        playerColor = GetComponent<PlayerColor>();
        playerSound = GetComponent<PlayerSound>();

        playerColor.ApplyColor(tankColor);

        playerShot.CanShot = false;
        playerMovement.CanMove = true;
    }
    public void Initilize(Color tankColor) => this.tankColor = tankColor;
    public void Kill()
    {
        if (isInvencible == false)
        {
            IsDead = true;

            playerShot.CanShot = false;
            playerMovement.CanMove = false;

            playerSound.StopEngineSound();
            tankRenreder.SetActive(false);
            OnKill!.Invoke();
        }
    }
}
