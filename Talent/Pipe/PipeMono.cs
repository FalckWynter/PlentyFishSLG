using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PipeMono : MonoHasElement
{
    public int startIndex, endIndex;
    public Light2D light2d;

    public void SetStartID(int id)
    {
        startIndex = id;
    }
    public void SetEndID(int id)
    {
        endIndex = id;
    }
    public override void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }
}
