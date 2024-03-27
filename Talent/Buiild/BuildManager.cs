using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
public class BuildManager :Singleton<BuildManager>
{
    //可以考虑上状态机
    public bool isBuildingMode = false;

    public BuildType type;
    public LineRenderer buildLiner;

    //简易版状态机
    public BuildStateSample buildState = new BuildStateSample();

    public BuildManager()
    {
        buildLiner = GameObject.Find("Builder").GetComponent<LineRenderer>();
        buildLiner.positionCount = 2;
    }
    public void ExitBuildMode()
    {
        //isBuildingMode = false;
        //buildLiner.positionCount = 0;
        //type = BuildType.None;
    }
    public void EnterBuildMode()
    {
        Debug.Log("进入建筑模式");
        //isBuildingMode = true;
        //type = BuildType.BuildTalentPipe;
        //buildLiner.positionCount = 2;
    }
    public void AddBuildPoint(int id)
    {
        buildState.AddBuildPoint(id);
    }
    public void ExchangeState(BuildType type)
    {
        buildState.ExitState();
        buildState = (BuildStateSample)Activator.CreateInstance(buildStateDictionary[type].GetType());
        buildState.EnterState();
         
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        UpdateBuildState();
    }
    public void UpdateBuildState()
    {
        buildState.UpdateState();
    }
    public Dictionary<BuildType, BuildStateSample> buildStateDictionary = new Dictionary<BuildType, BuildStateSample>()
    {
        { BuildType.None,new BuildStateSample() },
        { BuildType.BuildTalentPipe, new BuildStateConnect() },
        { BuildType.DestroyTalentPipe,new BuildStateDestroy() }
    };
    public enum BuildType
    {
        None,BuildTalentPipe,DestroyTalentPipe
    }

}
