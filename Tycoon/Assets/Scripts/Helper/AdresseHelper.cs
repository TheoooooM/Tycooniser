using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.AddressableAssets.Addressables;

public class AdresseHelper : MonoBehaviour
{
    public static void LoadAssetWithCallback<T>(string adress, Action<T> callbackAction)
    {
        var callback  = LoadAssetAsync<T>(adress);
        callback.Completed += (_) => FinishAssetLoading(adress,)
    }

    static void FinishAssetLoading(string key, AsyncOperationHandle<>)
}
