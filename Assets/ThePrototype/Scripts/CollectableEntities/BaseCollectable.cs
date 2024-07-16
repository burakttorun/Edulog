using System.Collections;
using System.Collections.Generic;
using ThePrototype.Scripts.Base;
using ThePrototype.Scripts.Controller;
using ThePrototype.Scripts.InteractableEntities;
using UnityEngine;

namespace ThePrototype.Scripts.CollectableEntities
{
    public abstract class BaseCollectable : BaseHoldable, ICollectable
    {
        [field: SerializeField] public CollectableSetting CollectableSetting { get; set; }

        public virtual void Collected()
        {
            if (InventorySystemController.Instance.CheckCapacity())
            {
                InventorySystemController.Instance.AddItem(this);
                Destroy(gameObject);
            }
            else
            {
                //noop
                Debug.Log("Inventory is full.");
            }
        }
    }
}