using System;
using System.Collections;
using System.Collections.Generic;
using ThePrototype.Scripts.Base.Interactable;
using ThePrototype.Scripts.Controller;
using UnityEngine;

namespace ThePrototype.Scripts.InteractableEntities
{
    public abstract class BaseHoldable : MonoBehaviour, IInteractable
    {
        public Transform Transform { get; set; }
        public string PromptMessage { get; set; }

        [field: SerializeField] public HoldableSetting HoldableSetting { get; set; }

        private void Awake()
        {
            Transform = transform;
            PromptMessage = HoldableSetting.PromptMessageOnGround;
        }

        public virtual void Interact()
        {
            PromptMessage = transform.parent != null
                ? HoldableSetting.PromptMessageOnHand
                : HoldableSetting.PromptMessageOnGround;
        }
    }
}