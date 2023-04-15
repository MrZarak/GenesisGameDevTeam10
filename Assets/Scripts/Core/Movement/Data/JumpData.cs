using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Enums;


namespace Core.Movement.Data
{
    [Serializable]
    public class JumpData
    {
    
        [field: SerializeField] public float JumpForce;
        [field: SerializeField] public float GravityScale;

    }
}