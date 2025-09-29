using UnityEngine;
using UnityEngine.InputSystem;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    private InventoryManager inventoryManager;
    private bool playerInRange = false;

    public string ItemName => itemName;
    public int Quantity
    {
        get => quantity;
        set => quantity = value;
    }
    public Sprite Sprite => sprite;

    private void Start()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (inventoryManager != null)
            {
                inventoryManager.AddItem(this);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D: " + other.name);
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D: " + other.name);
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
