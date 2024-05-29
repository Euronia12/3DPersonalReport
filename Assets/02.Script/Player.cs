using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterCondition condition;
    private CharacterController controller;
    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        condition = GetComponent<CharacterCondition>();
        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
