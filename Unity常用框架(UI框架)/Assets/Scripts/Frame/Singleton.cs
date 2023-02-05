using System;
using System.Collections;
using System.Collections.Generic;


public class Singleton<T> where T : class
{
    //单例
    private static T instance;

    public static T GetInstance() {
        if (instance==null) {
            instance = Activator.CreateInstance(typeof(T), true) as T;
        }
        return instance;
    }

    protected Singleton() {
        
    }
}
