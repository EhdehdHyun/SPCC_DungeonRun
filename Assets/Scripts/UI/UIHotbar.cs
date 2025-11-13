using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class UIHotbar : MonoBehaviour
{
    public ItemSlot[] slots;
    public Transform slotPanel;
    public int selectedIndex = -1;

    [Header("FPV 카메라")]
    public Transform equipHolder;
    public GameObject currentEquipObject;
    public bool isFPS = false;

    private Outline selectedOutline; //선택한 아이템을 확인하기 위한 아웃라인

    void Start()
    {
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].hotbar = this;
            slots[i].Clear();
        }

        CharacterManager.Instance.Player.addItem += AddToHotbar;
    }

    public void AddToHotbar()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;
        if (data == null) return;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].item = data;
                slots[i].quantity = 1;
                slots[i].Set();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        CharacterManager.Instance.Player.itemData = null;
    }

    public void RemoveFromHotbar(int index)
    {
        slots[index].Clear();
    }

    void Update() //숫자키 처리 메써드
    {
        for (int i = 0; i < Mathf.Min(5, slots.Length); i++)
        {
            if (Keyboard.current[(Key)(Key.Digit1 + i)].wasPressedThisFrame)
            {
                SelectSlot(i);
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            UseSelectedItem();
            if (selectedIndex != -1 &&
            slots[selectedIndex].item == null &&

            currentEquipObject != null)
            {
                Destroy(currentEquipObject);
            }
        }
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= slots.Length) return;
        selectedIndex = index;

        if (selectedOutline != null)
            selectedOutline.enabled = false;

        selectedOutline = slots[index].GetComponent<Outline>();
        if (selectedOutline != null)
            selectedOutline.enabled = true;

        if (currentEquipObject != null)
            Destroy(currentEquipObject);

        ItemData data = slots[index].item;

        if (data == null || data.equipPrefab == null)
        {
            currentEquipObject = null;
            return;
        }

        if (isFPS)
        {
            currentEquipObject = Instantiate(data.equipPrefab, equipHolder);
            currentEquipObject.transform.localPosition = Vector3.zero;
            currentEquipObject.transform.localRotation = Quaternion.identity;
        }

    }

    public void UseSelectedItem()
    {
        if (selectedIndex == -1) return;
        ItemSlot slot = slots[selectedIndex];
        if (slot.item == null) return;

        ItemData data = slot.item;

        if (data.type == ItemType.Consumable)
        {
            foreach (var con in data.consumables)
            {
                switch (con.type)
                {
                    case ConsumableType.Health:
                        CharacterManager.Instance.Player.condition.Heal(con.value);
                        break;
                    case ConsumableType.Hunger:
                        CharacterManager.Instance.Player.condition.Eat(con.value);
                        break;
                }
            }
            RemoveFromHotbar(selectedIndex);
        }
    }
    private void RemoveAndUpdate(int index)
    {
        slots[index].Clear();
        if (index == selectedIndex && currentEquipObject != null)
        {
            Destroy(currentEquipObject);
            currentEquipObject = null;
        }
        SelectSlot(index);
    }
}
