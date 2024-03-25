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

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private GameObject _portal;

    private bool portalPlaced;

    // Start is called before the first frame update
    void Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];
        portalPlaced = false;
        for (int x = 0; x < _mazeWidth; x++){
            for (int z = 0; z < _mazeDepth; z++){
                _mazeGrid[x,z] = Instantiate(_mazeCellPrefab, new Vector3(x,0,z), Quaternion.identity);
                _mazeGrid[x,z].transform.SetParent(transform);
            }
        }
        GenerateMaze(null,_mazeGrid[0,0]);
        Vector3 spawnPosition = new Vector3(_mazeGrid[0,0].transform.position.x, _mazeGrid[0,0].transform.position.y +1f ,_mazeGrid[0,0].transform.position.z);
        Instantiate(_player, spawnPosition, Quaternion.identity);
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell){
        currentCell.Visit();
        ClearWalls(previousCell,currentCell);

        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;


        if(!portalPlaced && (z == _mazeDepth-1)){
            Vector3 spawnPositionPortal = new Vector3(currentCell.transform.position.x,currentCell.transform.position.y,currentCell.transform.position.z+0.3f);
            GameObject portal = Instantiate(_portal, spawnPositionPortal, Quaternion.Euler(new Vector3(0,90,0)));
            portal.transform.SetParent(transform);
            portalPlaced=true;

        }else if(previousCell!=null){
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

                Vector3 spawnPositionGem = new Vector3(currentCell.transform.position.x,currentCell.transform.position.y +0.2f,currentCell.transform.position.z);
                GameObject gem = Instantiate(_collectables[gemIndex], spawnPositionGem, Quaternion.Euler(new Vector3(-90,0,0)));
                gem.transform.SetParent(transform);
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
