using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerColor))]
public class PlayerManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerColor playerColor;
    private Color tankColor;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerColor = GetComponent<PlayerColor>();

        ApplyColor(tankColor);
    }
    
    private void Update()
    {
        //playerMovement.Move();
    }

    public void Initilize(Color tankColor)
    {
        this.tankColor = tankColor;
    }

    public void ApplyColor(Color color)
    {
        playerColor.ApplyColor(color);
    }
}
