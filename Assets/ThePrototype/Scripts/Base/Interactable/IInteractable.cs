using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.Base.Interactable
{
    public interface IInteractable
    {
        public string PromptMessage { get; set; }

        public void Interact();
    }
}