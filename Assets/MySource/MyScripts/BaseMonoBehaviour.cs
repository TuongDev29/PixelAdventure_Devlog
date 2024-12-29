using UnityEngine;

public abstract class BaseMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.ControllerLoader();
    }

    protected virtual void Awake()
    {
        this.ControllerLoader();
    }

    protected void ControllerLoader()
    {
        this.Begin();
        this.LoadComponent();
        this.ResetValue();
        this.EndLoaded();
    }

    protected virtual void Begin() { }

    protected virtual void LoadComponent() { }

    protected virtual void ResetValue() { }

    protected virtual void EndLoaded() { }
}
