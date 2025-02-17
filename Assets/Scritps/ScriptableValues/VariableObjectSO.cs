using System;
using UnityEngine;

public abstract class VariableObjectSO<T> : ScriptableObject
{
    //[SerializeField] private T initialValue;

    [SerializeField] protected T value;

    public event Action<T> onValueChange;

    public T Value
    {
        get => value;
        set
        {
            this.value = value;
            onValueChange?.Invoke(this.value);
        }
    }
}
