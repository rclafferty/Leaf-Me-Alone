using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    Animator thisAnimator;

    // Start is called before the first frame update
    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        StartCoroutine(Glow(3, 25));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Glow(float minTime, float maxTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));

            // Animate glow
            thisAnimator.SetTrigger("Glow");

            yield return new WaitForSeconds(Random.Range(3, 6));

            // Animate dim
            thisAnimator.SetTrigger("Dim");
        }
    }
}
