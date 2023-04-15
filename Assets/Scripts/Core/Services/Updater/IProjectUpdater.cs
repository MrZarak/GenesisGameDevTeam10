using System;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectUpdater
{
    event Action UpdateCalled;
    event Action FixedUpdateCalled;
    event Action LateUpdateCalled;
}
