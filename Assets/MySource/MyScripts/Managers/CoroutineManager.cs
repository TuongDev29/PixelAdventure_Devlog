using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CoroutineManager : Singleton<CoroutineManager>
{
    [SerializeField] private List<ManagedCoroutineInfo> activeCoroutines = new List<ManagedCoroutineInfo>();
    private Dictionary<IEnumerator, Coroutine> runningCoroutines = new Dictionary<IEnumerator, Coroutine>();

    public Coroutine StartManagedCoroutine(IEnumerator coroutine, string description = "")
    {
        ManagedCoroutineInfo managedCoroutineInfo = GenerateCoroutineInfo(coroutine, description);
        activeCoroutines.Add(managedCoroutineInfo);

        Coroutine startedCoroutine = StartCoroutine(TrackCoroutine(coroutine, managedCoroutineInfo));
        runningCoroutines[coroutine] = startedCoroutine;

        return startedCoroutine;
    }

    public void StopManagedCoroutine(IEnumerator coroutine)
    {
        if (runningCoroutines.TryGetValue(coroutine, out Coroutine startedCoroutine))
        {
            StopCoroutine(startedCoroutine);
            runningCoroutines.Remove(coroutine);
        }
    }

    public void StopAllManagedCoroutines()
    {
        StopAllCoroutines();
        activeCoroutines.Clear();
        runningCoroutines.Clear();
    }

    private IEnumerator TrackCoroutine(IEnumerator coroutine, ManagedCoroutineInfo managedCoroutineInfo)
    {
        yield return StartCoroutine(coroutine);

        activeCoroutines.Remove(managedCoroutineInfo);
        runningCoroutines.Remove(coroutine);
    }

    private ManagedCoroutineInfo GenerateCoroutineInfo(IEnumerator coroutine, string description)
    {
        StackTrace stackTrace = new StackTrace();
        string methodName = "UnknownMethod";
        string className = "UnknownClass";

        for (int i = 2; i < stackTrace.FrameCount; i++)
        {
            var frame = stackTrace.GetFrame(i);
            var method = frame.GetMethod();

            if (method != null && method.Name != ".ctor" && method.Name != "MoveNext")
            {
                methodName = method.Name;
                className = method.DeclaringType?.Name ?? "UnknownClass";
                break;
            }
        }

        return new ManagedCoroutineInfo(className, methodName, coroutine.GetHashCode(), description);
    }
}

[System.Serializable]
public class ManagedCoroutineInfo
{
    public string ClassName;
    public string MethodName;
    public int HashCode;
    public string DebugMessage;

    public ManagedCoroutineInfo(string className, string methodName, int hashCode, string debugMessage)
    {
        ClassName = className;
        MethodName = methodName;
        HashCode = hashCode;
        DebugMessage = debugMessage;
    }
}