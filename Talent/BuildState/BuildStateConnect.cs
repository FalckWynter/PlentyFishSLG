using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BuildStateConnect : BuildStateSample
{
    public override void EnterState()
    {
        BuildManager.Instance.isBuildingMode = true;
        BuildManager.Instance.type = BuildManager.BuildType.BuildTalentPipe;
        BuildManager.Instance.buildLiner.positionCount = 2;
    }
    public override void UpdateState()
    {
        if (SelectedContain.Count == 1)
        {
            Debug.Log("ID" + SelectedContain[0]);
            Debug.Log("对象" + ContainManager.Instance.ContainMonoData[SelectedContain[0]]);
            BuildManager.Instance.buildLiner.SetPosition(0, ContainManager.Instance.ContainMonoData[SelectedContain[0]].transform.position);
            BuildManager.Instance.buildLiner.SetPosition(1, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -1));
        }
        if (SelectedContain.Count == 2)
        {
            //创造链接
            ContainManager.Instance.ContainDictionary[SelectedContain[0]].TryAddConnectNode(SelectedContain[1]);
            ContainManager.Instance.ContainMonoData[SelectedContain[0]].GetComponent<ContainMono>().RefreshPipePoint();
            Debug.LogWarning("完成节点链接" + SelectedContain[1]);
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
    //public override BuildStateConnect GetClone()
    //{
    //    return (BuildStateConnect)this.MemberwiseClone();
    //}
}
