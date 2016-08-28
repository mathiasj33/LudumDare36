using System;
using System.Collections;
using UnityEngine;

public class Invoker : MonoBehaviour
{
    public void Invoke(float delay, Action action)
    {
        StartCoroutine(ExecuteAfter(delay, action));
    }

    private IEnumerator ExecuteAfter(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}

