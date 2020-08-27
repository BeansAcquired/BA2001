using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Size")]
    [SerializeField] private int rows = 3;
    [SerializeField] private int cols = 3;
    [SerializeField] private float tileWidth = 3;
    [SerializeField] private float tileHeight = 3;
    

    [Header("Objects")]
    [SerializeField] private Sprite tile;

    private GameObject gridContainer;

    private Grid2D grid;
    
    void Start()
    {
        gridContainer = new GameObject("ModuleGrid");
        gridContainer.transform.position = Vector3.zero;

        grid = new Grid2D(
            rows,
            cols,
            tileHeight,
            tileWidth);

        PopulateGrid();
        // OutlineGrid();
        // RowLines();
        // ColLines();

    }


    private void PopulateGrid()
    {
        //foreach (Vector2 point in grid)
        //{
        //    GameObject newTile = Instantiate(levelModule, gridContainer.transform);
        //    newTile.transform.position = point;
        //}

        // Populate top row
        foreach (Vector2 point in grid.RowTop)
        {
            LevelModule levelModule = LevelModule.GetInstance(LevelModuleEdgePosition.Top, tile);
            levelModule.transform.position = point;
        }

        foreach (Vector2 point in grid.RowBottom)
        {
            LevelModule levelModule = LevelModule.GetInstance(LevelModuleEdgePosition.Bottom, tile);
            levelModule.transform.position = point;
        }
    }

    //private void OutlineGrid()
    //{
    //    LineRenderer outline = new GameObject("GridOutline").AddComponent<LineRenderer>();
    //    outline.material = lineMaterial;
    //    outline.positionCount = 5;
    //    outline.startWidth = 0.25f;
    //    outline.endWidth = 0.25f;
    //    outline.useWorldSpace = true;
    //    outline.SetPositions(new Vector3[]{
    //        new Vector3(grid.TopLeftCorner.x, grid.TopLeftCorner.y,-1),
    //        new Vector3(grid.TopRightCorner.x, grid.TopRightCorner.y),
    //        new Vector3(grid.BottomRightCorner.x, grid.BottomRightCorner.y,-1),
    //        new Vector3(grid.BottomLeftCorner.x, grid.BottomLeftCorner.y,-1),
    //        new Vector3(grid.TopLeftCorner.x, grid.TopLeftCorner.y,-1),
    //    });

    //}

    //private void RowLines()
    //{
    //    for (int i = 0; i < grid.Rows; i++)
    //    {
    //        RowLine(i);
    //    }
    //}

    //private void ColLines()
    //{
    //    for (int i = 0; i < grid.Columns; i++)
    //    {
    //        ColLine(i);
    //    }
    //}

    //private void RowLine(int index)
    //{
    //    List<Vector2> rowOne = grid.Row(index).ToList();
    //    LineRenderer outline = new GameObject("RowLine").AddComponent<LineRenderer>();
    //    outline.material = lineMaterial;
    //    outline.positionCount = rowOne.Count();
    //    outline.startWidth = 0.25f;
    //    outline.endWidth = 0.25f;
    //    outline.useWorldSpace = true;
        
    //    foreach (Vector2 point in rowOne)
    //    {
    //        outline.SetPosition(rowOne.IndexOf(point), new Vector3(point.x, point.y, -1));
    //    }
    //}

    //private void ColLine(int index)
    //{
    //    List<Vector2> rowOne = grid.Column(index).ToList();
    //    LineRenderer outline = new GameObject("ColLine").AddComponent<LineRenderer>();
    //    outline.material = lineMaterial;
    //    outline.positionCount = rowOne.Count();
    //    outline.startWidth = 0.25f;
    //    outline.endWidth = 0.25f;
    //    outline.useWorldSpace = true;

    //    foreach (Vector2 point in rowOne)
    //    {
    //        outline.SetPosition(rowOne.IndexOf(point), new Vector3(point.x, point.y, -1));
    //    }
    //}


}
