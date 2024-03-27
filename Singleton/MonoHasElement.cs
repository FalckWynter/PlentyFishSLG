using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public  class MonoHasElement : MonoBehaviour
{
    //public Dictionary<string, List<TextMeshPro>> textComponent = new Dictionary<string, List<TextMeshPro>>();
    //public Dictionary<string, List<SpriteRenderer>> spriteComponent = new Dictionary<string, List<SpriteRenderer>>();
    public Dictionary<string, List<Object>> controlDic = new Dictionary<string, List<Object>>();
    public bool isInitlized = false;
    public virtual void Awake()
    {
        GetChildrenComponent<SpriteRenderer>();
        GetChildrenComponent<TextMeshPro>();

    }
    public virtual void Start()
    {
        Initialize();
    }
    public virtual void Initialize()
    {
        GetChildrenComponent<SpriteRenderer>();
        GetChildrenComponent<TextMeshPro>();
    }

    public void GetChildrenComponent<T>() where T : Object
    {
        T[] components = GetComponentsInChildren<T>();
  
        foreach (T control in components)
        {
            if (controlDic.ContainsKey(control.name))
            {
                controlDic[control.name].Add(control);
            }
            else
            {
                controlDic.Add(control.name, new List<Object>() { control });
            }
        }

    }
    protected T GetControl<T>(string controlName) where T : Object
    {
        if (controlDic.ContainsKey(controlName))
        {
            foreach (T control in controlDic[controlName])
            {
                if (control.name == controlName)
                {
                    return control as T;
                }
            }
        }

        return null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
