using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;
    
    [SerializeField]
    private int _mazeDepth;

    private MazeCell[,] _mazeGrid;

    [SerializeField]
    private GameObject[] _collectables;

    // Start is called before the first frame update
    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int x = 0; x < _mazeWidth; x++){
            for (int z = 0; z < _mazeDepth; z++){
                _mazeGrid[x,z] = Instantiate(_mazeCellPrefab, new Vector3(x,0,z), Quaternion.identity);
            }
        }
        GenerateMaze(null,_mazeGrid[0,0]);
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell){
        currentCell.Visit();
        ClearWalls(previousCell,currentCell);

        if(previousCell!=null){
            
            int spawnChance = Random.Range(0,101);
            if(spawnChance >= 80){
                int gemChance = Random.Range(0,101);
                int gemIndex = 0;
                Debug.Log("spawnChance: "+spawnChance+", gemChance: "+gemChance);
                if(50 < gemChance && gemChance < 70){
                    gemIndex = 1;
                }else if(70 < gemChance && gemChance < 90){
                    gemIndex = 2;
                }else if(90 < gemChance){
                    gemIndex = 3;
                }

                Instantiate(_collectables[gemIndex], currentCell.transform.position, Quaternion.Euler(new Vector3(-90,0,0)));
            }
            
        }

        //yield return new WaitForSeconds(0.05f);

        MazeCell nextCell;

        do{
            nextCell = GetNextUnvisitedCell(currentCell);

            if(nextCell != null){
                GenerateMaze(currentCell, nextCell);
            }
        }while(nextCell != null);

    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell){
        var unvisitedCells = GetUnivistedCells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1,10)).FirstOrDefault();

    }

    private IEnumerable<MazeCell> GetUnivistedCells(MazeCell currentCell){
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if(x+1 < _mazeWidth){
            var cellToRight = _mazeGrid[x+1,z];

            if(cellToRight.IsVisited == false){
                yield return cellToRight;
            }
        }

        if(x-1 >= 0 ){
            var cellToLeft = _mazeGrid[x-1,z];
            if(cellToLeft.IsVisited == false){
                yield return cellToLeft;
            }
        }

        if(z+1 < _mazeDepth){
            var cellToFront = _mazeGrid[x,z+1];

            if(cellToFront.IsVisited == false){
                yield return cellToFront;
            }
        }

        if(z-1 >= 0 ){
            var cellToBack = _mazeGrid[x,z-1];
            if(cellToBack.IsVisited == false){
                yield return cellToBack;
            }
        }
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell){
        if(previousCell == null){
            return;
        }

        if(previousCell.transform.position.x < currentCell.transform.position.x){
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }
        if(previousCell.transform.position.x > currentCell.transform.position.x){
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }
        if(previousCell.transform.position.z < currentCell.transform.position.z){
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }
        if(previousCell.transform.position.z > currentCell.transform.position.z){
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
