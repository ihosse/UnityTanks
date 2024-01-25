using System;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IExplodable
{
    public int PlayerID { get; set; }
    public event Action<Vector3> OnExplode;
    public UnityEvent OnKill;

    public void Hit(int damagerPlayerID)
    {
        if (PlayerID == damagerPlayerID)
            return;

        OnExplode!.Invoke(Vector3.up);
        OnKill!.Invoke();
    }
}
