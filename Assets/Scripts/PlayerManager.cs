using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerColor), typeof(PlayerShot))]
public class PlayerManager : MonoBehaviour
{
    public Action OnKill;

    public PlayerMovement playerMovement {  get; private set; }

    public PlayerShot playerShot { get; private set; }

    private PlayerColor playerColor;
    private Color tankColor;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShot = GetComponent<PlayerShot>();
        playerColor = GetComponent<PlayerColor>();

        playerColor.ApplyColor(tankColor);

        playerShot.CanShot = false;
    }
    public void Initilize(Color tankColor) => this.tankColor = tankColor;
    public void Kill() => OnKill!.Invoke();
}
