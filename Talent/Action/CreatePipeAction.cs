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

        //��������
        GameObject ob = GameObject.Instantiate(TalentData.Instance.pipePrefab);
        //ȡ�ò��� *1
        Vector3 position = (ContainManager.Instance.ContainMonoData[startIndex].transform.position + ContainManager.Instance.ContainMonoData[endIndex].transform.position) / 2;
        float width = (ContainManager.Instance.ContainMonoData[startIndex].transform.position - ContainManager.Instance.ContainMonoData[endIndex].transform.position).magnitude;
        //Debug.Log("���" + width + "���" + ContainManager.Instance.ContainMonoData[startIndex].transform.position + "�յ�" + ContainManager.Instance.ContainMonoData[endIndex].transform.position);
        //����λ�ú͸���
        ob.transform.position = position;
        ob.transform.parent = TalentData.Instance.talentParent.transform;
        //���ô�С�ͳ���
        //ob.transform.localScale = new Vector3(width, ob.transform.localScale.y, ob.transform.localScale.z);
        ob.GetComponent<SpriteRenderer>().size = new Vector2(width, ob.GetComponent<SpriteRenderer>().size.y);
        //���ù�Դ
        ob.GetComponent<PipeMono>().light2d.gameObject.transform.localScale = new Vector3(width, 1, 1);
        //����ת�� *1
        //ob.transform.LookAt(ContainManager.Instance.ContainMonoData[endIndex].transform.position);
        //ob.transform.LookAt(position);

        //ob.transform.forward = ContainManager.Instance.ContainMonoData[endIndex].transform.position - ContainManager.Instance.ContainMonoData[startIndex].transform.position;
        //ob.transform.localEulerAngles = new Vector3(0, 0, ob.transform.localEulerAngles.x * -1);
        //���ص��ᳯ������
        ob.transform.right = ContainManager.Instance.ContainMonoData[endIndex].transform.position - ContainManager.Instance.ContainMonoData[startIndex].transform.position;

        //Quaternion quaternion = Quaternion.identity;//����յ���Ԫ��
        //quaternion = Quaternion.Euler(rotate);//ŷ����ת��Ԫ��
        //ob.transform.localRotation = quaternion;

        //ע��ܵ�����
        ContainManager.Instance.PipeMonoData.Add(new Vector2Int(startIndex, endIndex), ob);

        //int index = ContainManager.Instance.GetContainIndex();
        //ContainManager.Instance.ContainDictionary.Add(index, talentToCreate);
        //ContainManager.Instance.ContainMonoData.Add(index, ob);

        //��������ID
        ob.GetComponent<PipeMono>().SetStartID(startIndex);
        ob.GetComponent<PipeMono>().SetEndID(endIndex);
        //ob.GetComponent<ContainMono>()

        isCompleted = true;
    }
}
