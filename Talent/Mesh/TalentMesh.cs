using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TalentMesh : Singleton<TalentMesh>
{
    //核心设计思路：通过递归向外蔓延节点，有最大层数限制，最大节点数限制
    public int meshWidth = 36, meshHeight = 18;
    public int preWidth = 3, preHeight = 3;
    //迭代起点
    //public int startReProduceCircle = 3;
    //起始点 *废弃
    //public Vector2Int meshStartPoint = new Vector2Int();
    //每次最少生成的节点数 最多节点数
    //public int nodeLeastConnectCount = 2,nodeMaxConnectCount = 4;
    //要随机出的节点比例
    public float nullNodePertange = 0.5f;

    public TalentGrid[,] gridData;
    public TalentGridBlock[,] gridBlockData;
    public GameObject[,] gridObjectData;
    public List<TalentGrid> gridList = new List<TalentGrid>();

    //public List<TalentGridCircle> circleList = new List<TalentGridCircle>();
    //创建通路概率
    public float createNodeProbably = 0.5f;
    //偏移量 废弃
    //public int widthOffset = 1, HeightOffset = 1;
    //等待迭代的节点数 废弃
    //public List<TalentGrid> waitingGridList = new List<TalentGrid>();
    //尝试创建节点的次数 被层数替换
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

            //设置随机到的位置为空

        
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
        //懒狗了 暴力开杀吧

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
                    //Debug.Log("尝试激活" + i + "," + j + "失败");
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


#region 垃圾箱
//    public void ResetMesh()
//    {
//        ResetParameter();
//        SetParameter();

//        ReProduceNode();
//        //CreateStartCircle();
//        //SetStartPoint();
//        //ReProduceCircle();

//        Debug.Log("创建完毕");
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
//    Debug.Log("尝试递归" + node.position);
//    List<TalentGrid> temp = new List<TalentGrid>();
//    int x, y,offset =0 ;

//    int count = (int)UnityEngine.Random.Range(1f, 2.25f);
//    if (count <= 1 && (isParse || minProtect > 0))
//    {
//        count = 2;
//        minProtect--;
//    }
//    Debug.Log("进入递归" + count);
//    for (int i = 0; i < count; i++)
//    {
//        x = UnityEngine.Random.Range(-2, 3);
//        y = UnityEngine.Random.Range(1, 3);
//        Vector2Int pos = new Vector2Int(node.position.x + x, node.position.y + y);
//        if (node.position.x + x < 0 || node.position.x + x >= meshWidth || node.position.y + y < 0 || node.position.y + y >= meshHeight)
//            return;
//        if (gridData[node.position.x + x, node.position.y + y] == null)
//        {
//            Debug.Log("递归添加" );
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
////生成前三环
////circleList.Add(new TalentGridCircle(0, false));
////设置起始节点
//circleList[0].AddNode(new TalentGrid());
//circleList[1].AddNode(new TalentGrid(),4);
//circleList[2].AddNode(new TalentGrid(),8);
//circleList[0].AddNode(new TalentGrid());
//}
//public void SetStartPoint()
//{
//生成原始节点 0/1 - 1/4 - 2/4 - 3/8
//ParseProduceCircle(0, 4);
//ParseProduceCircle(1, 1);
//ParseProduceCircle(2, 2);

//}
//强制让每个节点都生成指定数量的节点
//public void ParseProduceCircle(int height,int count,bool isAver = true)
//{
//    TalentGrid grid;
//    if(height >= circleList.Count)
//    {
//        Debug.LogWarning("尝试让不存在的层数生成节点");
//            return;
//    }
//    circleList.Add(new TalentGridCircle(height + 1, isAver));
//    //遍历圆环中每个点
//    for(int i = 0;i<circleList[height].gridCircle.Count;i++)
//    {
//        //生成n个节点
//        for(int j = 0; j < count;j++)
//        {
//            //生成节点
//            grid = new TalentGrid();
//            //设置位置
//            grid.SetPosition(new Vector2Int(height + 1, circleList[height + 1].gridCircle.Count));
//            //添加到后继节点
//            circleList[height].gridCircle[i].AddConnectNode(grid.position);
//            //添加到圆环
//            circleList[height + 1].AddNode(grid);
//        }
//    }
//}
//public void ReProduceCircle()
//{
//    Debug.Log("准备递归" + circleList[2].gridCircle.Count);
//    for (int currentNode = 0; currentNode < circleList[2].gridCircle.Count; currentNode++)
//        {
//        GetNextNodes(circleList[2].gridCircle[currentNode], true);
//        }
//        Debug.Log("递归次数" + sortTime);
//}
//int sortTime = 0;
//TalentGrid tempG;
//public List<TalentGrid> GetNextNodes(TalentGrid node,bool isParse = false)
//{
//    Debug.Log("排序开始" + node.position.y);
//    if (node.position.y > 10)
//        return null;
//    sortTime++;
//    Debug.Log("测试排序");
//    List<TalentGrid> returnNodes = new List<TalentGrid>();
//    int count = (int)UnityEngine.Random.Range(0f, 3.25f);
//    if(isParse)
//    {
//        if (count <= 0)
//        {
//            Debug.Log("强制添加");
//            count = 1;
//        }
//    }
//    Debug.Log("目标次数" + count);
//    for (int i = 0; i < count; i++)
//    {
//        Debug.Log("添加子节点" + i);
//        tempG = new TalentGrid();
//        tempG.position.y = node.position.y + 1;
//        returnNodes.Add(tempG);
//        circleList[tempG.position.y].AddNode(tempG);
//    }
//    foreach(TalentGrid childNode in returnNodes)
//    {
//        Debug.Log("递归子节点");
//        GetNextNodes(childNode);
//    }
//    return null;
//}







//public void CreateBasicGrid()
//{
//    //测试函数


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
//    ////进入节点创建
//    //List<TalentGrid> gridList = GetNullNode(grid);
//    //TalentGrid tempgrid;
//    //int pos = 0,counter = gridList.Count ;
//    //for(int i = 0; i< counter;i++)
//    //{
//    //    //Debug.Log("随机次数" + gridList.Count);
//    //    //随机一个节点
//    //    pos = UnityEngine.Random.Range(0, gridList.Count);
//    //    //随机概率：成功创建节点
//    //    if(UnityEngine.Random.Range(0f,1f) < createNodeProbably)
//    //    {
//    //        tempgrid = gridList[pos];
//    //        gridList.Remove(tempgrid);
//    //        //设置该位置为可用节点
//    //        gridMesh[tempgrid.position.x, tempgrid.position.y] = new TalentGrid(tempgrid.position, true, grid.position);
//    //        //将该节点设置到下一次计算中
//    //        waitingGridList.Add(gridMesh[tempgrid.position.x, tempgrid.position.y]);
//    //    }
//    //}
//}
//public List<TalentGrid> GetNullNode(TalentGrid grid)
//{
//    //List<TalentGrid> gridList = new List<TalentGrid>();
//    ////获取八方向的格子
//    //for(int i = widthOffset * -1;i<= widthOffset;i++)
//    //{
//    //    if (grid.position.x + i < 0 || grid.position.x + i >= meshWidth)
//    //        continue;
//    //    for (int j = HeightOffset * -1; j <= HeightOffset;j++)
//    //    {
//    //        //高度合法
//    //        if (grid.position.y+j < 0 || grid.position.y+j >= meshHeight)
//    //            continue;
//    //        //此格不为空且可以移除
//    //        if (gridMesh[grid.position.x + i, grid.position.y + j].isNull == false || gridMesh[grid.position.x + i, grid.position.y + j].isRemoveAble == false)
//    //            continue;
//    //        //不是起始点
//    //        if (grid.sourceNode == new Vector2Int(grid.position.x + i, grid.position.y + j))
//    //            continue;
//    //        //将它添加到可用列表
//    //            gridList.Add(gridMesh[grid.position.x + i, grid.position.y + j]);

//    //    }
//    //}
//    //Debug.Log("抓取到的格子");
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