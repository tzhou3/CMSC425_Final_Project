using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    public void addItem(Image im)
    {
        icon = im;
        icon.enabled = true;

    }

    public void removeItem()
    {
        icon = null;
        icon.enabled = false;
    }
}
