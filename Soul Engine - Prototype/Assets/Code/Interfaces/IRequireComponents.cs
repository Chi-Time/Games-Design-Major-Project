using System;
using System.Collections.Generic;
using UnityEngine;

namespace SoulEngine
{
	public interface IRequireComponents
    {
        GameObject GameObject { get; }
    
        IEnumerable<Type> RequiredComponents ();
    }
}
