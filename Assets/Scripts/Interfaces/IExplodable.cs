using System;
using UnityEngine;

public interface IExplodable
{
    public event Action<Vector3> OnExplode;
}
