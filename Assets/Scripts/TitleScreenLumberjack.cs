using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenLumberjack : MonoBehaviour
{
    [SerializeField] Lumberjack lumberjack;
    [SerializeField] GameObject leftMarker;
    [SerializeField] GameObject rightMarker;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoMove());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AutoMove()
    {
        yield return new WaitForSeconds(0.2f);

        // Move left
        while (Mathf.Abs(transform.position.x - leftMarker.transform.position.x) > 0.1f)
        {
            lumberjack.MoveHorizontalInput(-1);
            yield return new WaitForEndOfFrame();
        }

        lumberjack.MoveHorizontalInput(0);

        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                lumberjack.SwingAxe();
                yield return new WaitForSeconds(1.5f);
            }

            yield return new WaitForSeconds(0.5f);

            // Move right
            while (Mathf.Abs(transform.position.x - rightMarker.transform.position.x) > 0.1f)
            {
                lumberjack.MoveHorizontalInput(1);
                yield return new WaitForEndOfFrame();
            }

            lumberjack.MoveHorizontalInput(0);

            yield return new WaitForSeconds(0.5f);

            lumberjack.GetComponent<SpriteRenderer>().flipX = true;
            for (int i = 0; i < 10; i++)
            {
                lumberjack.SwingAxe();
                yield return new WaitForSeconds(1.5f);
            }
            lumberjack.GetComponent<SpriteRenderer>().flipX = false;

            // Move left
            while (Mathf.Abs(transform.position.x - leftMarker.transform.position.x) > 0.1f)
            {
                lumberjack.MoveHorizontalInput(-1);
                yield return new WaitForEndOfFrame();
            }

            lumberjack.MoveHorizontalInput(0);
        }
    }
}
