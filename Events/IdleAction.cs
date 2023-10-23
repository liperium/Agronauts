using System;
using Godot;

public class IdleAction<T>
{
    private Action<T> action;

    public static IdleAction<T> operator+(IdleAction<T> a, Action<T> b)
    {
        if (b.Target == null)
        {
            GD.PrintErr("Target is null on action!! This should not happen");
            return a;
        }

        a.action += b;
        
        if (b.Target is Node n)
        {
            n.TreeExited += () => a.OnNodeTreeExited(b);
        }
        
        return a;
    }

    public void AddManual(Action<T> actionToAdd)
    {
        action += actionToAdd;
    }

    public void RemoveManual(Action<T> actionToRemove)
    {
        action -= actionToRemove;
    }

    private void OnNodeTreeExited(Action<T> currentAction)
    {
        action -= currentAction;
    }

    public void Invoke(T param)
    {
        action?.Invoke(param);
    }

    public bool Contains(Action actionToCheck)
    {
        if (action == null) return false;
        foreach (Delegate a in action.GetInvocationList())
        {
            if (a == (Delegate)actionToCheck)
            {
                return true;
            }
        }
        
        return false;
    }
}