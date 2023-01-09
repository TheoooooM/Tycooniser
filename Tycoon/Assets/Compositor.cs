using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Services.Interfaces;
using UnityEngine;
using Attributes;
using Services;

public class Compositor : MonoBehaviour
{
    protected struct DependenceInfos
    {
        public object obj;
        public FieldInfo field;
    }

    protected Dictionary<Type, IService> servicesDictionnary = new();
    protected Dictionary<Type, List<DependenceInfos>> fieldDependenciesDictionnary = new();

    private void Awake()
    {
       InitCompositor().Forget(); 
    }

    private async UniTaskVoid InitCompositor()
    {
        //Debug.Log("Init");
        if(!Compose()) Debug.LogError("Can't Compose");
    }

    bool Compose()
    {
        //Debug.Log("Compose");
        CreateServices();
        ResoleDepencencies();
        return true;
    }

    void CreateServices()
    {
        //Debug.Log("Create Service");
        AddService<IGameService>(new GameService());
        AddService<ITimeService>(new TimeService());
    }

    void AddService<T>(T service) where  T : IService
    {
        Debug.Log($"Add {service}");
        if (servicesDictionnary.ContainsKey(typeof(T))) throw new DuplicateNameException("Double Services");
        servicesDictionnary.Add(typeof(T), service);

        CollectServiceDependencies(service);
        
    }

    void CollectServiceDependencies(object obj)
    {
        Debug.Log($"Collect {obj} dependecies");
        var type = obj.GetType();
        var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        Debug.Log($"Fields of {type} : {fields.Length}");
        foreach (var field in fields)
        {
           var dependenceFields = Attribute.GetCustomAttributes(field, typeof(DependeOnService));
           Debug.Log("Dependencies finds : " + dependenceFields.Length);
           if (dependenceFields.Length == 0) continue;
           foreach (var _ in dependenceFields)
           {
               if (field.FieldType.IsInterface && typeof(IService).IsAssignableFrom(field.FieldType) &&
                   field.FieldType != typeof(IService))
               {
                  var fieldList = fieldDependenciesDictionnary.ContainsKey(field.FieldType)? fieldDependenciesDictionnary[field.FieldType] : new List<DependenceInfos>();
                  fieldList.Add(new DependenceInfos
                  {
                      obj = obj,
                      field = field
                  });
                  fieldDependenciesDictionnary[field.FieldType] = fieldList;
               }
               else Debug.LogError("Can't Assign Field");
           }
        }
    }

    void ResoleDepencencies()
    {
        Debug.Log("Resolve Dependencies");
        foreach (KeyValuePair<Type,List<DependenceInfos>> dependenceInfo in fieldDependenciesDictionnary)
        {
            Type type = dependenceInfo.Key;
            List<DependenceInfos> fieldInfos = dependenceInfo.Value;

            if (servicesDictionnary.ContainsKey(type))
            {
                var service = servicesDictionnary[type];
                foreach (var infos in fieldInfos)
                {
                    infos.field.SetValue(infos.obj, service);
                    Debug.Log("Set Service");
                }
            }
            else throw new MissingMemberException("Missing Service");
        }
    }
}
