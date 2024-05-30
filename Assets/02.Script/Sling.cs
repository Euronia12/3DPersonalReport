using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Coroutine coroutine;
    public float waitTime = 3.0f;
    public float power = 100f; 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Rigidbody>(out rb) && collision.gameObject.TryGetComponent<Animator>(out animator))
        {
            coroutine = StartCoroutine(Catapult(rb));
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopCoroutine(coroutine);
        }
    }
    IEnumerator Catapult(Rigidbody rb)
    {
        yield return new WaitForSeconds(waitTime);
        rb.AddForce(new Vector3 (-1,2,1).normalized * power, ForceMode.Impulse);
        animator.SetTrigger("Jump");
    }
}
