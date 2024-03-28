using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentMeshCreator : MonoBehaviour
{
    public GameObject gridPrefab;
    public GameObject gridParent;
    GameObject ob;
    public TalentMesh mesh;
    public void CreateMesh(TalentMesh mesh)
    {

        foreach(TalentGrid grid in mesh.gridData)
        {
            if (grid == null)
                continue;

            //Debug.Log("����Բ��" + gridCircle);
            //if (gridCircle.gridCircle.Count <= 0)
            //    continue;
            CreateObject(grid);
            //CreatCircle(gridCircle.gridCircle.Count, 7.5f * gridCircle.y);
        }
    }
    public void CreateObject(TalentGrid grid)
    {
        ob = Instantiate(gridPrefab);

        ob.SetActive(true);

        ob.transform.parent = gridParent.transform;
        ob.transform.localPosition = new Vector3(grid.position.x * 4.5f, grid.position.y * 4.5f, -1);
        ob.GetComponent<TalentGridMono>().position = grid.position;
        TalentMesh.Instance.gridObjectData[grid.position.x, grid.position.y] = ob;
        ob.gameObject.name = grid.position.ToString();

        if(grid.isLarge)
        {
            ob.GetComponent<TalentGridMono>().LargeObject();
        }
        if (grid.isLocked)
        {
            ob.GetComponent<TalentGridMono>().LockGameObject();
        }
        if(!grid.isVisible)
        {
            ob.GetComponent<TalentGridMono>().HideGameobject();
        }
        if(ob.gameObject.activeSelf)
            Debug.Log("�������" + grid.position); 
        //if (grid.isStartPoint)
        //{
        //    ob.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //}
    }
    ///// <summary> ��Ҫ��ʵ�����Ķ��� </summary>
    //public GameObject obj;

    ///// <summary> �������ĵ�,ͬ��Ҳ�Ǹ����� </summary>
    //public Transform tra;

    ///// <summary> �������� </summary>
    //int iconCount = 100;

    ///// <summary> Բ�뾶 </summary>
    //float fRadius = 20;

    /// <summary>
    /// Χ��һ��������Բ
    /// </summary>
    public void CreatCircle(int iconCount, float fRadius)
    {
        float angle = 360f / iconCount;

        for (int i = 0; i < iconCount; i++)
        {
            GameObject go = Instantiate(gridPrefab);

            go.transform.SetParent(gridParent.transform, false);

            float x = fRadius * Mathf.Sin((angle * i) * (Mathf.PI / 180f));

            float y = fRadius * Mathf.Cos((angle * i) * (Mathf.PI / 180f));

            go.transform.localPosition = new Vector3(x, y, 0);

            //go.transform.localEulerAngles = new Vector3(0, 0, Mathf.Abs(angle * i - 360));

            go.name = i.ToString();

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TalentMesh.Instance.Initialize();
        TalentMesh.Instance.ResetMesh();
        CreateMesh(TalentMesh.Instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
