using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    //public UIInventory inventory; 만약 인벤토리 사용하면 다시 사용
    public UIHotbar hotbar;

    public Button button;
    public Image icon;
    public TextMeshProUGUI quatityText;
    private Outline outline;

    public int index;
    public bool equipped;
    public int quantity;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    // private void OnEnable()
    // {
    //     outline.enabled = equipped;
    // }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quatityText.text = quantity.ToString();

        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        //inventory.SelectItem(index);
    }
}
