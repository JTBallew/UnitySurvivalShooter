using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
    public int timer;

    void Awake()
    {
        Debug.Log("Start Timer");
        timer = 5;
        StartCoroutine(nameof(CountdownTimer));
    }

    private IEnumerator CountdownTimer()
    {
        while (timer >0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            Debug.Log("Time left: " + timer);
        }
        //yield return new WaitForSeconds(5f);
        Debug.Log("Timer Completed");
    }

}
