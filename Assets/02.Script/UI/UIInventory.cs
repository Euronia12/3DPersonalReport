using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public GameObject inventoryWindow;
    public Transform slotPanel;

    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedStatName;
    public TextMeshProUGUI selectedStatValue;
    public GameObject useBtn;
    public GameObject equipBtn;
    public GameObject unequipBtn;
    public GameObject dropBtn;

    private CharacterController controller;
    private CharacterCondition condition;
    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.GetComponent<CharacterController>();
        condition = CharacterManager.Instance.GetComponent<CharacterCondition>();

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
        ClearSelectedItemWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearSelectedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedStatName.text = string.Empty;
        selectedStatValue.text = string.Empty;

        useBtn.SetActive(false);
        equipBtn.SetActive(false);
        unequipBtn.SetActive(false);
        dropBtn.SetActive(false);
    }

    public void Toggle()
    {

    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }
}
