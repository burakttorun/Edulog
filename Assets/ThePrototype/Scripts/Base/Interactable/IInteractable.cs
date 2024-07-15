using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.Base.Interactable
{
    public interface IInteractable
    {
        public Transform Transform { get; set; }
        public string PromptMessage { get; set; }

        public void Interact();
    }
}