using UnityEngine;

public class Jump : MonoBehaviour
{
    public float tileSize = 1f;
    public float hopTime = 0.15f;
    public LayerMask platformMask;

    bool Jumping = false; 
    
    void update(){
        if (Jumping) return;

        if (Input.GetMouseButtonDown(0))
        {
            bool leftSide = Input.mousePosition.x < Screen.width * 0.5f;
            int dir = leftSide ? 1 : -1;
            Hopping(dir);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)) Hopping(-1);
        if(Input.GetKeyDown(KeyCode.RightArrow)) Hopping(1);     
    }

    void Hopping(int dir)
    {
        Vector3 start = transform.position;
        Vector3 target = start + new Vector3(Vector3 start, Vector3 end);

        Collider2d platform = Physics2D.OverlapCircle(target, 0.1f, platformMask);

        if(platform)
           StartCoroutine(Hop(start, target));
        else
           StartCoroutine(Fall(start));
    }

    System.Collections.IEnumerator Hop(Vector3 start, Vector3 end)
    {
        Jumping = true;

        float t = 0;
       while (t < hopTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, t / hopTime);
            yield return null;
        }

        transform.position = end;
        Jumping = false;
    }

    System.Collections.IEnumerator Fall(Vector3 start)
    {
        Jumping = true;

        Vector3 end = start + new Vector3(0, -5f, 0);

        float t = 0;
        while (t < hopTime * 2f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, t / (hopTime * 2f));
            yield return null;
        }

        Debug.Log("GAME OVER");
        gameObject.SetActive(false);
    }
}