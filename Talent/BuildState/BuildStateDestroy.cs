using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BuildStateDestroy : BuildStateSample
{
    public override void EnterState()
    {
        BuildManager.Instance.isBuildingMode = true;
        BuildManager.Instance.type = BuildManager.BuildType.DestroyTalentPipe;
        BuildManager.Instance.buildLiner.positionCount = 2;
    }
    public override void UpdateState()
    {
        if (SelectedContain.Count == 2)
        {
            //移除链接
            ContainManager.Instance.ContainDictionary[SelectedContain[0]].connectNodes.Remove(SelectedContain[1]);
            ContainManager.Instance.ContainMonoData[SelectedContain[0]].GetComponent<ContainMono>().RefreshPipePoint();
            //退出建造模式
            BuildManager.Instance.ExchangeState(BuildManager.BuildType.None);
        }
    }
    public override void ExitState()
    {
        BuildManager.Instance.isBuildingMode = false;
        BuildManager.Instance.buildLiner.positionCount = 0;
        BuildManager.Instance.type = BuildManager.BuildType.None;
    }
    //public override BuildStateDestroy GetClone()
    //{
    //    return (BuildStateDestroy)this.MemberwiseClone();
    //}
}
