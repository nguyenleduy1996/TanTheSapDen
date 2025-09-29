using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> items = new List<Item>();

    [SerializeField]
    private Transform itemSlotParent; // Kéo InventorySlot vào đây trong Inspector
    private ItemSlot[] itemSlots;

    public GameObject inventoryCanvas;
    private PlayerControls controls;

    private void Update() { }

    private void Awake()
    {
        controls = new PlayerControls();
        itemSlots = itemSlotParent.GetComponentsInChildren<ItemSlot>();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Inventory.performed += OnInventoryPerformed;
    }

    private void OnDisable()
    {
        controls.Player.Inventory.performed -= OnInventoryPerformed;
        controls.Disable();
    }

    private void OnInventoryPerformed(InputAction.CallbackContext ctx)
    {
        if (inventoryCanvas != null)
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
    }

    // Thêm item vào inventory
    public void AddItem(Item item)
    {
        // Nếu đã có item này thì tăng số lượng
        Item existingItem = items.Find(i => i.ItemName == item.ItemName);
        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
        }
        else
        {
            items.Add(item);
        }
        UpdateInventoryUI();
    }

    // Xóa item khỏi inventory
    public void RemoveItem(Item item)
    {
        items.Remove(item);
        UpdateInventoryUI();
    }

    // Lấy danh sách item
    public List<Item> GetItems()
    {
        return items;
    }

    // Cập nhật UI (bạn tự hiện thực theo UI của bạn)
    private void UpdateInventoryUI()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < items.Count)
                itemSlots[i].SetItem(items[i]);
            else
                itemSlots[i].SetItem(null);
        }
    }
}
