using System;
using System.Collections.Generic;
using System.Data;
using Cysharp.Threading.Tasks;
using Services.Interfaces;
using UnityEngine;

public class Compositor : MonoBehaviour
{

    protected Dictionary<Type, IService> servicesDictionnary = new();

    private void Awake()
    {
       InitCompositor().Forget(); 
    }

    private async UniTaskVoid InitCompositor()
    {
        if(!Compose()) Debug.LogError("Can't Compose");
    }

    bool Compose()
    {

        CreateServices();
        
        return true;
    }

    void CreateServices()
    {
        
    }

    void AddService<T>(T service) where  T : IService
    {
        if (servicesDictionnary.ContainsKey(typeof(T))) throw new DuplicateNameException("Double Services");
        servicesDictionnary.Add(typeof(T), service);

        CollectServiceDependencies();
        
    }

    void CollectServiceDependencies()
    {
        
    }
}
