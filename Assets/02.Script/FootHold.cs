using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHold : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    private float throwPower = 200f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<Rigidbody>(out rb) && other.TryGetComponent<Animator>(out animator))
            {
                rb.AddForce(Vector3.up * throwPower, ForceMode.Impulse);
                animator.SetTrigger("Jump");
            }
        }
    }
}
 
