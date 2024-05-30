using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public CharacterCondition condition;
    public CharacterController controller;

    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;
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
