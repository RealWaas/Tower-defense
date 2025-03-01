using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PoolManager
{
    public static Dictionary<GameObject, List<GameObject>> objectPools =
        new Dictionary<GameObject, List<GameObject>>();

    /// <summary>
    /// Return the first disabled object from the correct pool.
    /// <br></br>
    /// Handle the creation of a pool if the key is not found.
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public static GameObject GetAvailableObjectFromPool(GameObject _key)
    {
        // Create a pool of the given key if not existing
        if (!objectPools.ContainsKey(_key))
        {
            CreateNewPool(_key);
            return CreateInstance(_key);
        }

        // Create an instance if the pool is empty
        if (objectPools[_key].Count == 0)
        {
            return CreateInstance(_key);
        }

        // Find the first element inactive
        GameObject availableObject = objectPools[_key].Where(element =>
        {
            return element.gameObject.activeSelf == false;
        }).FirstOrDefault();

        // Create an instance if none is available
        if (availableObject == null)
            return CreateInstance(_key);

        // Activate and give the first available
        availableObject.gameObject.SetActive(true);
        return availableObject;
    }

    /// <summary>
    /// Create a new pool with a prefab as key.
    /// </summary>
    /// <param name="_key"></param>
    private static void CreateNewPool(GameObject _key) => objectPools.Add(_key, new List<GameObject>());

    /// <summary>
    /// Create a new instance of a key and add it to the correct pool.
    /// </summary>
    /// <param name="_key"></param>
    /// <param name="_transform"></param>
    /// <returns></returns>
    private static GameObject CreateInstance(GameObject _key, Transform _transform = null)
    {
        GameObject newInstance = GameObject.Instantiate(_key);
        AddToPool(_key, newInstance);
        return newInstance;
    }

    /// <summary>
    /// Add an object to its pool.
    /// </summary>
    /// <param name="_key"></param>
    /// <param name="_value"></param>
    private static void AddToPool(GameObject _key, GameObject _value) => objectPools[_key].Add(_value);

    /// <summary>
    /// Reset all elements in pools to restart the game.
    /// </summary>
    public static void ResetPools()
    {
        // For each pool
        foreach (List<GameObject> instances in objectPools.Values)
        {
            // For each element
            foreach (GameObject instance in instances)
                instance.SetActive(false);
        }
    }
}
