using System;
using System.Collections;
using System.Collections.Generic;
using ThePrototype.Scripts.Controller;
using UnityEngine;
using TMPro;

namespace ThePrototype.Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        [field: Header("References")]
        [field: SerializeField]
        private PlayerController PlayerController { get; set; }

        [field: SerializeField] private TextMeshProUGUI PromptText { get; set; }

        private void OnEnable()
        {
            PlayerController.OnPromptTextUpdate += PromptTextUpdate;
        }

        private void OnDisable()
        {
            PlayerController.OnPromptTextUpdate -= PromptTextUpdate;
        }

        private void PromptTextUpdate(string value)
        {
            PromptText.text = value;
        }
    }
}