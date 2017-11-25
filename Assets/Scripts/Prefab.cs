using UnityEngine;
using Object = UnityEngine.Object;

public class Prefab
{
    private readonly string _path;
    private GameObject _resource;
    
    public Prefab(string path)
    {
        _path = path;
    }

    public GameObject Instantiate()
    {
        if (_resource == null)
        {
            Debug.Log("Resource " + _path + " was null. Loading.");
            _resource = Resources.Load<GameObject>(_path);
        }
        return Object.Instantiate(_resource);
    }

    public override string ToString()
    {
        return _path;
    }
}