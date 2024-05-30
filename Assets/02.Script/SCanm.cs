using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SCanm : MonoBehaviour
{
    [SerializeField]
    private Transform rayPos;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    float distance = 5f;
    [SerializeField]
    GameObject caution;
    Ray ray;
    private void Start()
    {
        ray = new Ray(rayPos.position, Vector3.forward);
        StartCoroutine(CheckRay());
    }

    IEnumerator CheckRay()
    {
        while (true) 
        {
            Debug.DrawRay(rayPos.position, Vector3.forward * distance);
            if (Physics.Raycast(ray, distance, layerMask))
            {
                caution.SetActive(true);
                yield return new WaitForSeconds(3.0f);
                caution.SetActive(false);
            }
            yield return null;
        }
    }
}
