using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public State(Transform transform)
    {
        this.transform = transform;
    }

    protected Transform transform;

    protected GameObject gameObject
    {
        get
        {
            return transform.gameObject;
        }
    }
    public abstract void Update();
}
