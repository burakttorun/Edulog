using System;
using System.Collections;
using System.Collections.Generic;
using ThePrototype.Scripts.Base.Interactable;
using ThePrototype.Scripts.Controller;
using UnityEngine;

namespace ThePrototype.Scripts.InteractableEntities
{
    public class FlowerEntity : MonoBehaviour, IInteractable
    {
        public Transform Transform { get; set; }
        public string PromptMessage { get; set; }

        [field: SerializeField] public FlowerSetting Setting { get; set; }

        private void Awake()
        {
            Transform = transform;
            PromptMessage = Setting.PromptMessageOnGround;
        }

        public void Interact()
        {
            PromptMessage = transform.parent != null
                ? Setting.PromptMessageOnHand
                : Setting.PromptMessageOnGround;
        }
    }
}