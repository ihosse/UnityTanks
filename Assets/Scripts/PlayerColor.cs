using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PlayerColor : MonoBehaviour
{
    private Renderer[] renderers;
    private Color color;

    void ApplyColor(Color color)
    {
        renderers = GetComponentsInChildren<Renderer>();

        foreach (var renderer in renderers)
        {
            renderer.material.color = color;
        }
    }
}
