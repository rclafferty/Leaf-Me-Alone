using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    const int START_DURABILITY = 10;
    int durability;

    [SerializeField] Sprite[] treeSprites;

    [SerializeField] GameObject[] childComponents;

    // Start is called before the first frame update
    void Start()
    {
        durability = START_DURABILITY;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hit()
    {
        durability--;

        double durabilityPercentage = (double)durability / START_DURABILITY;

        int spriteIndex = (int)(treeSprites.Length * durabilityPercentage);

        // if 0
        if (durability == 0)
        {
            // fall over
            FallOver();
        }
        else
        {
            spriteRenderer.sprite = treeSprites[treeSprites.Length - spriteIndex - 1];
        }
    }

    void FallOver()
    {
        Debug.Log("Falling over");
        // Destroy(gameObject);
        StartCoroutine(CutDownAndRespawn(gameObject));
    }

    IEnumerator CutDownAndRespawn(GameObject g)
    {
        g.GetComponent<SpriteRenderer>().enabled = false; //.SetActive(false);
        g.GetComponent<BoxCollider2D>().enabled = false; //.SetActive(false);

        // foreach (GameObject child in childComponents)
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            child.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(1);

        spriteRenderer.sprite = treeSprites[0];
        durability = START_DURABILITY;

        g.GetComponent<SpriteRenderer>().enabled = true; //.SetActive(true);
        g.GetComponent<BoxCollider2D>().enabled = true; //.SetActive(false);

        //foreach (GameObject child in childComponents)
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            child.gameObject.SetActive(true);
        }
    }
}
