using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStation : MonoBehaviour
{
    public Vector3 firstPos;
    public Vector3 secondPos;
    public Vector3 pre_Pos;
    public Vector3 pos;
    private float time;
    public Vector3 prePos;
    public Vector3 addPos;
    public Vector3 nowPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnCarry());
        prePos = transform.position;
    }

    IEnumerator OnCarry()
    {
        while (true) 
        {
            yield return new WaitUntil(() => MoveShuttle(firstPos, secondPos));
            yield return new WaitUntil(() => MoveShuttle(secondPos, firstPos));
        }
    }

    bool MoveShuttle(Vector3 startPos, Vector3 endPos)
    {
        time += Time.deltaTime;
        float pos = time / 5f;
        nowPos = Vector3.Lerp(startPos, endPos, pos);
        addPos = nowPos - prePos;
        transform.position = nowPos;
        if (pos >= 1f)
        {
            time = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
