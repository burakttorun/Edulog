using System.Collections;
using System.Collections.Generic;
using ThePrototype.Scripts.CollectableEntities;
using UnityEngine;

namespace ThePrototype.Scripts.Base
{
    public interface ICollectable
    {
        public CollectableSetting CollectableSetting { get; set; }
        public void Collected();
    }
}