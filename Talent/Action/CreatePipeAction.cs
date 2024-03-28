using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePipeAction : AbstractAction
{
    int startIndex, endIndex;
    public CreatePipeAction(int start,int end)
    {
        startIndex = start;
        endIndex = end;
    }

    public override void Update()
    {
        base.Update();

        //生成物体
        GameObject ob = GameObject.Instantiate(TalentData.Instance.pipePrefab);
        //取得参数 *1
        Vector3 position = (ContainManager.Instance.ContainMonoData[startIndex].transform.position + ContainManager.Instance.ContainMonoData[endIndex].transform.position) / 2;
        float width = (ContainManager.Instance.ContainMonoData[startIndex].transform.position - ContainManager.Instance.ContainMonoData[endIndex].transform.position).magnitude;
        //Debug.Log("宽度" + width + "起点" + ContainManager.Instance.ContainMonoData[startIndex].transform.position + "终点" + ContainManager.Instance.ContainMonoData[endIndex].transform.position);
        //设置位置和父类
        ob.transform.position = position;
        ob.transform.parent = TalentData.Instance.talentParent.transform;
        //设置大小和长宽
        //ob.transform.localScale = new Vector3(width, ob.transform.localScale.y, ob.transform.localScale.z);
        ob.GetComponent<SpriteRenderer>().size = new Vector2(width, ob.GetComponent<SpriteRenderer>().size.y);
        //设置光源
        ob.GetComponent<PipeMono>().light2d.gameObject.transform.localScale = new Vector3(width, 1, 1);
        //设置转向 *1
        //ob.transform.LookAt(ContainManager.Instance.ContainMonoData[endIndex].transform.position);
        //ob.transform.LookAt(position);

        //ob.transform.forward = ContainManager.Instance.ContainMonoData[endIndex].transform.position - ContainManager.Instance.ContainMonoData[startIndex].transform.position;
        //ob.transform.localEulerAngles = new Vector3(0, 0, ob.transform.localEulerAngles.x * -1);
        //朴素的轴朝向向量
        ob.transform.right = ContainManager.Instance.ContainMonoData[endIndex].transform.position - ContainManager.Instance.ContainMonoData[startIndex].transform.position;

        //Quaternion quaternion = Quaternion.identity;//定义空的四元数
        //quaternion = Quaternion.Euler(rotate);//欧拉角转四元数
        //ob.transform.localRotation = quaternion;

        //注册管道数据
        ContainManager.Instance.PipeMonoData.Add(new Vector2Int(startIndex, endIndex), ob);

        //int index = ContainManager.Instance.GetContainIndex();
        //ContainManager.Instance.ContainDictionary.Add(index, talentToCreate);
        //ContainManager.Instance.ContainMonoData.Add(index, ob);

        //设置链接ID
        ob.GetComponent<PipeMono>().SetStartID(startIndex);
        ob.GetComponent<PipeMono>().SetEndID(endIndex);
        //ob.GetComponent<ContainMono>()

        isCompleted = true;
    }
}
