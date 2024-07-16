using System;
using System.Collections.Generic;
using ThePrototype.Scripts.Base;
using ThePrototype.Scripts.InputHandle;
using ThePrototype.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace ThePrototype.Scripts.Controller
{
    [Serializable]
    public struct InventorySetting
    {
        public int itemCapacity;
    }

    public class InventorySystemController : MonoBehaviour
    {
        public static InventorySystemController Instance;


        private List<ICollectable> _items = new();

        [field: Header("References")]
        [field: SerializeField]
        public GameObject InventoryScreen { get; set; }

        [field: SerializeField] InputReader Input { get; set; }

        [field: Header("Settings")]
        [field: SerializeField]
        public InventorySetting InventorySetting { get; set; }

        [field: SerializeField] public Transform ItemsParent { get; set; }
        [field: SerializeField] public GameObject ItemHolderPrefab { get; set; }

        [field: SerializeField] public Toggle EnableRemove { get; set; }

        public ItemHolder[] ItemHolders { get; set; }
        private void OnEnable()
        {
            Input.Inventory += ActivationInventory;
        }

        private void OnDisable()
        {
            Input.Inventory -= ActivationInventory;
        }

        private void Awake()
        {
            Instance = this;
            InventoryScreen.SetActive(false);
        }

        private void ActivationInventory(bool isPressed)
        {
            if (isPressed)
            {
                InventoryScreen.SetActive(!InventoryScreen.activeSelf);
                ListItems();
            }
        }

        public bool CheckCapacity()
        {
            return (_items.Count < InventorySetting.itemCapacity - 1) ? true : false;
        }

        public void AddItem(ICollectable item)
        {
            _items.Add(item);
        }

        public void RemoveItem(ICollectable item)
        {
            _items.Remove(item);
        }

        public void ListItems()
        {
            foreach (Transform item in ItemsParent)
            {
                Destroy(item.gameObject);
            }

            foreach (ICollectable item in _items)
            {
                ItemHolder createdItemHolder = Instantiate(ItemHolderPrefab, ItemsParent).GetComponent<ItemHolder>();
                createdItemHolder.ItemName.text = item.CollectableSetting.ItemName;
                createdItemHolder.ItemIcon.sprite = item.CollectableSetting.Icon;
                createdItemHolder.RemoveButton.gameObject.SetActive(EnableRemove.isOn);
            }
            SetInventoryItems();
        }

        public void EnableItemRemove()
        {
            foreach (Transform item in ItemsParent)
            {
                item.GetComponent<ItemHolder>().RemoveButton.gameObject.SetActive(EnableRemove.isOn);
            }
        }

        public void SetInventoryItems()
        {
            ItemHolders = ItemsParent.GetComponentsInChildren<ItemHolder>();
            for (int i = 0; i < _items.Count; i++)
            {
                ItemHolders[i].AddItem(_items[i]);
            }
        }
    }
}