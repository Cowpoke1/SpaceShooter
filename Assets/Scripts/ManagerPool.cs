using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerPool : MonoBehaviour
{

    [SerializeField]
    Transform parent;

    public void InitPool(int size, GameObject prefab, List<GameObject> list)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject go = Instantiate(prefab);
            go.transform.parent = parent;
            go.SetActive(false);
            list.Add(go);
        }
    }

    public void ClearPool(List<GameObject> list)
    {
        list.ForEach((x) => Destroy(x));
        list.Clear();
    }

}
