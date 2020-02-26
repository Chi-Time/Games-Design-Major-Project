using System;
using System.Collections.Generic;
using UnityEngine;

public interface IRequireComponents
{
    GameObject GameObject { get; }

    IEnumerable<Type> RequiredComponents ();
}
