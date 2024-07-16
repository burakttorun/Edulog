using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using ThePrototype.Scripts.Base;
using ThePrototype.Scripts.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace ThePrototype.Scripts.UI
{
    public class ItemHolder : MonoBehaviour
    {
        public ICollectable Item { get; set; }

        [field: Header("References")]
        [field: SerializeField]
        public Text ItemName { get; set; }

        [field: SerializeField] public Image ItemIcon { get; set; }
        [field: SerializeField] public Button RemoveButton { get; set; }

        public void RemoveItem()
        {
            InventorySystemController.Instance.RemoveItem(Item);
            Destroy(gameObject);
        }

        public void AddItem(ICollectable item)
        {
            Item = item;
        }

        public void UseItem()
        {
            //noop
            Debug.Log("Item used.");
            RemoveItem();   
        }
    }
}