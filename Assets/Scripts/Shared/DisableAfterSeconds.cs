using System.Collections;
using UnityEngine;

public class DisableAfterSeconds : MonoBehaviour
{
    [SerializeField]
    private float secondsToDisable = .3f;
    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(secondsToDisable);
        gameObject.SetActive(false);
    }
}
