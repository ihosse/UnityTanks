using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerColor))]
[RequireComponent(typeof(PlayerShot), typeof(PlayerSound), typeof(Damageable))]
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
    private Damageable damageable;

    private Color tankColor;
    private int playerId;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShot = GetComponent<PlayerShot>();
        playerColor = GetComponent<PlayerColor>();
        playerSound = GetComponent<PlayerSound>();
        damageable = GetComponent<Damageable>();

        playerColor.ApplyColor(tankColor);
        playerShot.CreateShot(playerId);
        damageable.PlayerID = playerId;

        playerShot.CanShot = false;
        playerMovement.CanMove = true;
    }
    public void Initilize(Color tankColor, int playerId)
    {
        this.tankColor = tankColor;
        this.playerId = playerId;
    }
    public void Kill()
    {
        if (isInvencible == false)
        {
            IsDead = true;

            playerShot.CanShot = false;
            playerMovement.CanMove = false;

            playerSound.StopEngineSound();
            gameObject.SetActive(false);
            OnKill!.Invoke();
        }
    }
}
