using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TalentMesh : Singleton<TalentMesh>
{
    //�������˼·��ͨ���ݹ��������ӽڵ㣬�����������ƣ����ڵ�������
    public int meshWidth = 36, meshHeight = 18;
    public int preWidth = 3, preHeight = 3;
    //�������
    //public int startReProduceCircle = 3;
    //��ʼ�� *����
    //public Vector2Int meshStartPoint = new Vector2Int();
    //ÿ���������ɵĽڵ��� ���ڵ���
    //public int nodeLeastConnectCount = 2,nodeMaxConnectCount = 4;
    //Ҫ������Ľڵ����
    public float nullNodePertange = 0.5f;

    public TalentGrid[,] gridData;
    public TalentGridBlock[,] gridBlockData;
    public GameObject[,] gridObjectData;
    public List<TalentGrid> gridList = new List<TalentGrid>();

    //public List<TalentGridCircle> circleList = new List<TalentGridCircle>();
    //����ͨ·����
    public float createNodeProbably = 0.5f;
    //ƫ���� ����
    //public int widthOffset = 1, HeightOffset = 1;
    //�ȴ������Ľڵ��� ����
    //public List<TalentGrid> waitingGridList = new List<TalentGrid>();
    //���Դ����ڵ�Ĵ��� �������滻
    //public int tryCreateCount = 0,maxTryCreateCount = 3;
    public TalentMesh()
    {

    }
    public void ResetMesh()
    {
        ResetGridData();
        SpawnRandomNode();

        SetStartNode();
    }
    public void ResetGridData()
    {
        gridObjectData = new GameObject[meshWidth, meshHeight];
        gridData = new TalentGrid[meshWidth, meshHeight];
        for(int j = 0;j < meshHeight;j++)
        {
            for(int i = 0;i< meshWidth;i++)
            {
                gridData[i, j] = new TalentGrid(new Vector2Int(i, j));
                gridList.Add(gridData[i, j]);
            }
        }
    }

    public void SpawnRandomNode()
    {
        int count = (int)(preWidth * preHeight * nullNodePertange);


        int widCount = meshWidth / preWidth;
        int heiCount = meshHeight / preHeight;
        //gridBlockData = new TalentGridBlock[widCount, heiCount];
        //for()
        for (int i = 0; i < widCount; i++)
        {
            for (int j = 0; j < heiCount; j++)
            {
                SetRandomBlock(preWidth, preHeight, i * preWidth, j * preHeight, count);
            }
        }

            //�����������λ��Ϊ��

        
    }
    int place = 0;
    TalentGrid grid;
    public void SetRandomBlock(int widCount,int heiCount,int offsetX,int offsetY,int deleteCount)
     {

        List<TalentGrid> temp = new List<TalentGrid>();
        for(int i = 0;i<widCount;i++)
        {
            for(int j = 0;j<heiCount;j++)
            {
                temp.Add(gridData[i + offsetX, j + offsetY]);
            }
        }
        for(int i = 0; i< deleteCount;i++)
        {
            place = UnityEngine.Random.Range(0, temp.Count);
            grid = temp[place];
            gridData[grid.position.x, grid.position.y] = null;
            temp.RemoveAt(place);
        }
     }
    public void SetStartNode()
    {
        //������ ������ɱ��

        for(int i =-2;i<=2;i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                if (i != 0 && j != 0)
                    gridData[meshWidth / 2 + i, meshHeight / 2 + j] = null;
                else
                {
                    gridData[meshWidth / 2 + i, meshHeight / 2 + j] = new TalentGrid(new Vector2Int(meshWidth / 2 + i, meshHeight / 2 + j));
                    gridData[meshWidth / 2 + i, meshHeight / 2 + j].isVisible = true;
                }
            }
        }
        gridData[meshWidth / 2 , meshHeight / 2 ] = new TalentGrid(new Vector2Int(meshWidth / 2 , meshHeight / 2 ));
        gridData[meshWidth / 2, meshHeight / 2].isStartPoint = true;
        gridData[meshWidth / 2, meshHeight / 2].isLocked = false;
        gridData[meshWidth / 2, meshHeight / 2].isVisible = true;

    }
    public void UnlockNodes(Vector2Int pos, int radius = 1)
    {
        for (int i = -1 * radius; i <= radius; i++)
        {
            if (pos.x + i < 0 || pos.x + i >= meshWidth)
                continue;
            for (int j = -1 * radius; j <= radius; j++)
            {
                if (pos.y + j < 0 || pos.y + j >= meshHeight)
                    continue;
                if (gridObjectData[pos.x + i, pos.y + j] == null)
                {
                    //Debug.Log("���Լ���" + i + "," + j + "ʧ��");
                    continue;
                }
                    gridObjectData[pos.x + i, pos.y + j].SetActive(true);
                gridObjectData[pos.x + i, pos.y + j].GetComponent<TalentGridMono>().UnLockGameObject();
            }
        }
    }
    public void VisibleNodes(Vector2Int pos, int radius = 2)
    {
        for (int i = -1 * radius; i <= radius; i++)
        {
            if (pos.x + i < 0 || pos.x + i >= meshWidth)
                continue;
            for (int j = -1 * radius; j <= radius; j++)
            {
                if (pos.y + j < 0 || pos.y + j >= meshHeight)
                    continue;
                if (gridObjectData[pos.x + i, pos.y+ j] == null)
                    continue;
                gridObjectData[pos.x + i, pos.y + j].SetActive(true);

            }
        }
    }
}


#region ������
//    public void ResetMesh()
//    {
//        ResetParameter();
//        SetParameter();

//        ReProduceNode();
//        //CreateStartCircle();
//        //SetStartPoint();
//        //ReProduceCircle();

//        Debug.Log("�������");
//        CreateBasicGrid();
//    }
//    public void ResetParameter()
//    {
//        gridData = new TalentGrid[meshWidth, meshHeight];
//        //meshStartPoint = new Vector2Int(meshWidth / 2, meshHeight / 2);
//    }
//    public void SetParameter()
//    {
//        gridData[meshWidth/2, meshHeight/2] = new TalentGrid(new Vector2Int(meshWidth / 2, meshHeight / 2));
//        gridData[meshWidth / 2 + 1, meshHeight / 2] = new TalentGrid(new Vector2Int(meshWidth / 2 + 1, meshHeight / 2));
//        gridData[meshWidth / 2, meshHeight / 2 + 1] = new TalentGrid(new Vector2Int(meshWidth / 2, meshHeight / 2 + 1));
//        gridData[meshWidth / 2 - 1, meshHeight / 2] = new TalentGrid(new Vector2Int(meshWidth / 2 - 1, meshHeight / 2));
//        gridData[meshWidth / 2, meshHeight / 2 - 1] = new TalentGrid(new Vector2Int(meshWidth / 2, meshHeight / 2 - 1));
//    }
//    public void ReProduceNode()
//    {
//        GetNextNode(gridData[meshWidth / 2, meshHeight / 2],true);
//;    }

//    TalentGrid tempGrid;
//    int minProtect = 2;
//public void GetNextNode(TalentGrid node,bool isParse = false)
//{
//    Debug.Log("���Եݹ�" + node.position);
//    List<TalentGrid> temp = new List<TalentGrid>();
//    int x, y,offset =0 ;

//    int count = (int)UnityEngine.Random.Range(1f, 2.25f);
//    if (count <= 1 && (isParse || minProtect > 0))
//    {
//        count = 2;
//        minProtect--;
//    }
//    Debug.Log("����ݹ�" + count);
//    for (int i = 0; i < count; i++)
//    {
//        x = UnityEngine.Random.Range(-2, 3);
//        y = UnityEngine.Random.Range(1, 3);
//        Vector2Int pos = new Vector2Int(node.position.x + x, node.position.y + y);
//        if (node.position.x + x < 0 || node.position.x + x >= meshWidth || node.position.y + y < 0 || node.position.y + y >= meshHeight)
//            return;
//        if (gridData[node.position.x + x, node.position.y + y] == null)
//        {
//            Debug.Log("�ݹ����" );
//            tempGrid = new TalentGrid(pos);
//            temp.Add(tempGrid);
//            gridData[node.position.x + x, node.position.y + y] = tempGrid;
//        }
//        if (!node.connectNodeList.Contains(pos))
//            node.connectNodeList.Add(pos);
//        foreach (TalentGrid gr in temp)
//        {
//            GetNextNode(gr);
//        }
//    }

//}
//public void CreateStartCircle()
//{
//for(int i =0;i<meshCircleMax;i++)
//{
//    circleList.Add(new TalentGridCircle(i, false));
//}
////����ǰ����
////circleList.Add(new TalentGridCircle(0, false));
////������ʼ�ڵ�
//circleList[0].AddNode(new TalentGrid());
//circleList[1].AddNode(new TalentGrid(),4);
//circleList[2].AddNode(new TalentGrid(),8);
//circleList[0].AddNode(new TalentGrid());
//}
//public void SetStartPoint()
//{
//����ԭʼ�ڵ� 0/1 - 1/4 - 2/4 - 3/8
//ParseProduceCircle(0, 4);
//ParseProduceCircle(1, 1);
//ParseProduceCircle(2, 2);

//}
//ǿ����ÿ���ڵ㶼����ָ�������Ľڵ�
//public void ParseProduceCircle(int height,int count,bool isAver = true)
//{
//    TalentGrid grid;
//    if(height >= circleList.Count)
//    {
//        Debug.LogWarning("�����ò����ڵĲ������ɽڵ�");
//            return;
//    }
//    circleList.Add(new TalentGridCircle(height + 1, isAver));
//    //����Բ����ÿ����
//    for(int i = 0;i<circleList[height].gridCircle.Count;i++)
//    {
//        //����n���ڵ�
//        for(int j = 0; j < count;j++)
//        {
//            //���ɽڵ�
//            grid = new TalentGrid();
//            //����λ��
//            grid.SetPosition(new Vector2Int(height + 1, circleList[height + 1].gridCircle.Count));
//            //��ӵ���̽ڵ�
//            circleList[height].gridCircle[i].AddConnectNode(grid.position);
//            //��ӵ�Բ��
//            circleList[height + 1].AddNode(grid);
//        }
//    }
//}
//public void ReProduceCircle()
//{
//    Debug.Log("׼���ݹ�" + circleList[2].gridCircle.Count);
//    for (int currentNode = 0; currentNode < circleList[2].gridCircle.Count; currentNode++)
//        {
//        GetNextNodes(circleList[2].gridCircle[currentNode], true);
//        }
//        Debug.Log("�ݹ����" + sortTime);
//}
//int sortTime = 0;
//TalentGrid tempG;
//public List<TalentGrid> GetNextNodes(TalentGrid node,bool isParse = false)
//{
//    Debug.Log("����ʼ" + node.position.y);
//    if (node.position.y > 10)
//        return null;
//    sortTime++;
//    Debug.Log("��������");
//    List<TalentGrid> returnNodes = new List<TalentGrid>();
//    int count = (int)UnityEngine.Random.Range(0f, 3.25f);
//    if(isParse)
//    {
//        if (count <= 0)
//        {
//            Debug.Log("ǿ�����");
//            count = 1;
//        }
//    }
//    Debug.Log("Ŀ�����" + count);
//    for (int i = 0; i < count; i++)
//    {
//        Debug.Log("����ӽڵ�" + i);
//        tempG = new TalentGrid();
//        tempG.position.y = node.position.y + 1;
//        returnNodes.Add(tempG);
//        circleList[tempG.position.y].AddNode(tempG);
//    }
//    foreach(TalentGrid childNode in returnNodes)
//    {
//        Debug.Log("�ݹ��ӽڵ�");
//        GetNextNodes(childNode);
//    }
//    return null;
//}







//public void CreateBasicGrid()
//{
//    //���Ժ���


//    RefreshPoint();
//}
//public void RefreshPoint()
//{
//    //List<TalentGrid> tempgrid = waitingGridList;
//    //while(tryCreateCount < maxTryCreateCount)
//    //{

//    //    tempgrid = waitingGridList;

//    //    waitingGridList = new List<TalentGrid>();

//    //    foreach (TalentGrid grid in tempgrid)
//    //    {
//    //        SpawnConnectNode(grid);
//    //    }
//    //    tryCreateCount++;
//    //}
//}
//public void SpawnConnectNode(TalentGrid grid,bool isForce = false)
//{
//    ////����ڵ㴴��
//    //List<TalentGrid> gridList = GetNullNode(grid);
//    //TalentGrid tempgrid;
//    //int pos = 0,counter = gridList.Count ;
//    //for(int i = 0; i< counter;i++)
//    //{
//    //    //Debug.Log("�������" + gridList.Count);
//    //    //���һ���ڵ�
//    //    pos = UnityEngine.Random.Range(0, gridList.Count);
//    //    //������ʣ��ɹ������ڵ�
//    //    if(UnityEngine.Random.Range(0f,1f) < createNodeProbably)
//    //    {
//    //        tempgrid = gridList[pos];
//    //        gridList.Remove(tempgrid);
//    //        //���ø�λ��Ϊ���ýڵ�
//    //        gridMesh[tempgrid.position.x, tempgrid.position.y] = new TalentGrid(tempgrid.position, true, grid.position);
//    //        //���ýڵ����õ���һ�μ�����
//    //        waitingGridList.Add(gridMesh[tempgrid.position.x, tempgrid.position.y]);
//    //    }
//    //}
//}
//public List<TalentGrid> GetNullNode(TalentGrid grid)
//{
//    //List<TalentGrid> gridList = new List<TalentGrid>();
//    ////��ȡ�˷���ĸ���
//    //for(int i = widthOffset * -1;i<= widthOffset;i++)
//    //{
//    //    if (grid.position.x + i < 0 || grid.position.x + i >= meshWidth)
//    //        continue;
//    //    for (int j = HeightOffset * -1; j <= HeightOffset;j++)
//    //    {
//    //        //�߶ȺϷ�
//    //        if (grid.position.y+j < 0 || grid.position.y+j >= meshHeight)
//    //            continue;
//    //        //�˸�Ϊ���ҿ����Ƴ�
//    //        if (gridMesh[grid.position.x + i, grid.position.y + j].isNull == false || gridMesh[grid.position.x + i, grid.position.y + j].isRemoveAble == false)
//    //            continue;
//    //        //������ʼ��
//    //        if (grid.sourceNode == new Vector2Int(grid.position.x + i, grid.position.y + j))
//    //            continue;
//    //        //������ӵ������б�
//    //            gridList.Add(gridMesh[grid.position.x + i, grid.position.y + j]);

//    //    }
//    //}
//    //Debug.Log("ץȡ���ĸ���");
//    return null;
//}
//// Start is called before the first frame update
//void Start()
//{

//}

//// Update is called once per frame
//void Update()
//{

//}
#endregion