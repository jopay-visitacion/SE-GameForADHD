using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float tileSize = 1f;      
    public float hopTime = 0.15f;    
    public LayerMask platformMask;   

    bool jumping = false;

    void Update()
    {
        if (jumping) return;

        
        if (Input.GetMouseButtonDown(0))
        {
            bool leftSide = Input.mousePosition.x < Screen.width * 0.5f;
            int dir = leftSide ? -1 : 1;
            TryHop(dir);
        }

        
        if (Input.GetKeyDown(KeyCode.LeftArrow)) TryHop(-1);
        if (Input.GetKeyDown(KeyCode.RightArrow)) TryHop(1);
    }

    void TryHop(int dir)
    {
        Vector2 start = transform.position;

        
        Vector2 target = start + new Vector2(dir * tileSize, tileSize);

        
        Collider2D platform = Physics2D.OverlapCircle(target, 0.1f, platformMask);

        if (platform)
            StartCoroutine(Hop(start, target));
        else
            StartCoroutine(Fall(start));
    }

    System.Collections.IEnumerator Hop(Vector2 start, Vector2 end)
    {
        jumping = true;

        float t = 0;
        while (t < hopTime)
        {
            t += Time.deltaTime;
            transform.position = Vector2.Lerp(start, end, t / hopTime);
            yield return null;
        }

        transform.position = end;
        jumping = false;
    }

    System.Collections.IEnumerator Fall(Vector2 start)
    {
        jumping = true;

        Vector2 end = start + new Vector2(0, -5f);

        float t = 0;
        while (t < hopTime * 2f)
        {
            t += Time.deltaTime;
            transform.position = Vector2.Lerp(start, end, t / (hopTime * 2f));
            yield return null;
        }

        Debug.Log("GAME OVER");
        gameObject.SetActive(false);
    }
}
