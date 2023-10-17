using System;
using System.Collections.Generic;

public class IdleAction
{
    private List<Delegate> invocationList;

    public IdleAction()
    {
        invocationList = new List<Delegate>();
    }
    
    private static void CheckInit(IdleAction a)
    {
        if (a.invocationList == null)
        {
            a.invocationList = new List<Delegate>();
        }
    }

    public static IdleAction operator+(IdleAction a, Action b)
    {
        CheckInit(a);
        a.invocationList.Add(b);
        return a;
    }
    
    public static IdleAction operator-(IdleAction a, Action b)
    {
        CheckInit(a);
        a.invocationList.Remove(b);
        return a;
    }

    public void Invoke()
    {
        CheckInit(this);
        
        if (invocationList == null)
        {
            invocationList = new List<Delegate>();
        }
        
        //invoke valid delegates
        foreach (Delegate del in invocationList.ToArray())
        {
            try
            {
                del.DynamicInvoke();
            }
            catch (ObjectDisposedException ex)
            {
                invocationList.Remove(del);
            }
        }
    }

    public bool Contains(Action action)
    {
        if (invocationList == null) return false;
        return invocationList.Contains(action);
    }
}

public class IdleAction<T>
{
    private List<Delegate> invocationList;

    public IdleAction()
    {
        invocationList = new List<Delegate>();
    }
    
    private static void CheckInit(IdleAction<T> a)
    {
        if (a.invocationList == null)
        {
            a.invocationList = new List<Delegate>();
        }
    }

    public static IdleAction<T> operator+(IdleAction<T> a, Action<T> b)
    {
        CheckInit(a);
        a.invocationList.Add(b);
        return a;
    }
    
    public static IdleAction<T> operator-(IdleAction<T> a, Action<T> b)
    {
        CheckInit(a);
        a.invocationList.Remove(b);
        return a;
    }
    
    public void Invoke(T param1)
    {
        CheckInit(this);
        
        //invoke valid delegates
        foreach (Delegate del in invocationList.ToArray())
        {
            try
            {
                del.DynamicInvoke(param1);
            }
            catch (ObjectDisposedException ex)
            {
                invocationList.Remove(del);
            }
        }
    }

    public bool Contains(Action<T> action)
    {
        if (invocationList == null) return false;
        return invocationList.Contains(action);
    }
}