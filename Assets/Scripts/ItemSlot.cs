using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image icon;
    public TMPro.TextMeshProUGUI quantityText;

    public void SetItem(Item item)
    {
        if (item != null)
        {
            icon.sprite = item.Sprite;
            icon.enabled = true;
            quantityText.text = item.Quantity.ToString();
        }
        else
        {
            icon.enabled = false;
            quantityText.text = "";
        }
    }
}
