using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputReader
{
    public interface IEntityInputSource 
    {
        float HorizontalDirection { get; }
        float VerticalDirection { get; }
        bool Jump { get; }
        bool Attack { get; }

        void ResetOneTimeActions();
    
    }
}

