using System;
using System.Collections;
using System.Collections.Generic;
using ThePrototype.Scripts.Base.Interactable;
using UnityEngine;

namespace ThePrototype.Scripts.InteractableEntities
{
    public class FlowerEntity : MonoBehaviour, IInteractable
    {
        public string PromptMessage { get; set; }

        private void Awake()
        {
            PromptMessage = "Pick the flower from the ground.";
        }

        public void Interact()
        {
            gameObject.SetActive(false);
            Debug.Log(PromptMessage);
        }
    }
}