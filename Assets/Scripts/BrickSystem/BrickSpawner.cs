using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    // 
    // Grid parameters
    // 

    [Header("Grid Data")]
    [SerializeField] private GameObject brick;
    [SerializeField] private GameObject invBrick;
    [SerializeField] private BrickLevelCollection lvlCollection;

    [Header("Grid Size")]
    [SerializeField] private int row = 70;
    [SerializeField] private int col = 40;

    private BrickLevel brickLevel;

    // Start is called before the first frame update
    void Start()
    {
        SelectLevel();
        BuildLevel();
    }

    void SelectLevel()
    {
        foreach( BrickLevel brickLvl in lvlCollection.LvlCollection )
        {
            if (lvlCollection.SelectedLvl == brickLvl.name)
            {
                brickLevel = brickLvl;
                break;
            }
        }
    }

    void BuildLevel()
    {
        // Builds level
        string[] levelData = brickLevel.levelData;

        bool rowExact = false;
        bool colExact = false;

        int arrCount = 0;

        if (levelData.Length == row)
        {
            rowExact = true;
        }

        for (int i = 0; i < row; i++)
        {
            if (rowExact || i == 0)
            {
                arrCount = i;
            }
            else
            {
                arrCount = i % levelData.Length;
            }

            string currRow = levelData[arrCount];

            if (currRow.Length == col)
            {
                colExact = true;
            }
            else
            {
                colExact = false;
            }

            int strCount = 0;

            for (int j = 0; j < col; j++)
            {
                if (colExact || j == 0)
                {
                    strCount = j;
                }
                else
                {
                    strCount = j % currRow.Length;
                }

                Vector3 spawnPos = new Vector3(j - (col / 2) + 0.5f, i, 0);

                if (currRow[strCount] == 'B')
                {
                    Instantiate(brick, spawnPos, Quaternion.identity);
                }
                else if (currRow[strCount] == 'I')
                {
                    Instantiate(invBrick, spawnPos, Quaternion.identity);
                }
            }
        }

        
    }
}
