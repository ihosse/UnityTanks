using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IExplodable
{
    public event Action<Vector3> OnExplode;
    public UnityEvent OnKill;

    public void Hit()
    {
        OnExplode!.Invoke(Vector3.up);
        OnKill!.Invoke();
    }
}
