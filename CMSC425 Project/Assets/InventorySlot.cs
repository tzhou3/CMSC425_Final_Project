using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    public void addItem(Sprite im)
    {
        icon.sprite = im;
        icon.enabled = true;

    }

    public void removeItem()
    {
        icon = null;
        icon.enabled = false;
    }
}
