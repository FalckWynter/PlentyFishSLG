using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentGridBlock
{
    public TalentGrid[,] gridBlock;
    public TalentGridBlock(int x,int y)
    {
        gridBlock = new TalentGrid[x, y];
        for(int i = 0; i < x;i++)
        {
            for(int j = 0; j < y;j++)
            {
                gridBlock[i, j] = new TalentGrid();
            }
        }
    }
}
