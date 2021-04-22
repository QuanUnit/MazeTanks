using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public event Action OnStartedCount;
    public event Action OnFinishedCount;

    [SerializeField] private Text counterText;
    public void StartCount(uint number)
    {
        gameObject.SetActive(true);
        OnStartedCount?.Invoke();
        StartCoroutine(CountProcess(number));
    }
    private IEnumerator CountProcess(uint countNumber)
    {
        while (countNumber > 0)
        {
            counterText.text = countNumber.ToString();
            yield return new WaitForSeconds(1);
            countNumber--;
        }
        OnFinishedCount?.Invoke();
        gameObject.SetActive(false);
    }
}
