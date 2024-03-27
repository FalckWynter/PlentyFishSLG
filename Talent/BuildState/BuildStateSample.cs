using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BuildStateSample : object
{
    public List<int> SelectedContain = new List<int>();
    public virtual void EnterState()
    {
        BuildManager.Instance.isBuildingMode = false;
        BuildManager.Instance.type = BuildManager.BuildType.None;
        BuildManager.Instance.buildLiner.positionCount = 0;
    }
    public virtual void UpdateState()
    {

    }
    public virtual void ExitState()
    {

    }
    public void AddBuildPoint(int id)
    {
        if (SelectedContain.Contains(id))
            return;
        SelectedContain.Add(id);
    }
    public virtual BuildStateSample GetClone()
    {
        return (BuildStateSample)this.MemberwiseClone();
    }
}
