using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid2D : IEnumerable<Vector2>
{
    private List<Vector2> gridPoints = new List<Vector2>();

    public IEnumerator<Vector2> GetEnumerator() => gridPoints.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)gridPoints).GetEnumerator();

    // Rows
    public int Rows { get; }
    public float RowHeight { get; }

    public IEnumerable<Vector2> RowTop => gridPoints.Where(p => p.y == YMax);
    public IEnumerable<Vector2> RowBottom => gridPoints.Where(p => p.y == YMin);

    
    // Cols
    public int Columns { get; }
    public float ColumnWidth { get; }

    public IEnumerable<Vector2> ColumnLeft => gridPoints.Where(p => p.x == XMin);
    public IEnumerable<Vector2> ColumnRight => gridPoints.Where(p => p.x == XMax);

    // Y
    public float YMax => gridPoints.Select(p => p.y).Max();
    public float YMin => gridPoints.Select(p => p.y).Min();

    // X
    public float XMax => gridPoints.Select(p => p.x).Max();
    public float XMin => gridPoints.Select(p => p.x).Min();


    // Dimensions
    public float Width => Columns * ColumnWidth;
    public float Height => Rows * RowHeight;

    // Points
    public int Points => gridPoints.Count;
    public Vector2 TopLeft => gridPoints.Single(p => p.x == XMin && p.y == YMax);
    public Vector2 TopRight => gridPoints.Single(p => p.x == XMax && p.y == YMax);
    public Vector2 BottomRight => gridPoints.Single(p => p.x == XMax && p.y == YMin);
    public Vector2 BottomLeft => gridPoints.Single(p => p.x == XMin && p.y == YMin);
    public Vector2 TopLeftCorner => new Vector2(XMin - (ColumnWidth / 2), YMax + (RowHeight / 2));
    public Vector2 TopRightCorner => new Vector2(XMax + (ColumnWidth / 2), YMax + (RowHeight / 2));
    public Vector2 BottomRightCorner => new Vector2(XMax + (ColumnWidth / 2), YMin - (RowHeight / 2));
    public Vector2 BottomLeftCorner => new Vector2(XMin - (ColumnWidth / 2), YMin - (RowHeight / 2));

    public Grid2D(int rows, int cols, float rowHeight = 1f, float colWidth = 1f, bool centerX = true, bool centerY = true)
    {
        Rows = rows;
        Columns = cols;
        RowHeight = rowHeight;
        ColumnWidth = colWidth;

        PopulateGridPoints(centerX, centerY);
    }

    private void PopulateGridPoints(bool centerX, bool centerY)
    {
        float colOffset = centerX ? (Width / 2) - (ColumnWidth /2) : 0;
        float rowOffset = centerY ? (Height / 2) - (RowHeight / 2) : 0;

        for (var i = 0; i < Rows * Columns; i++)
        {
            var col = i % Columns * ColumnWidth;
            var row = i / Columns * RowHeight;

            gridPoints.Add(new Vector2(col - colOffset, row - rowOffset));
        }
    }

    public IEnumerable<Vector2> Row(int index)
    {
        if (index < 0 || index >= Rows) return new List<Vector2>();

        return gridPoints
            .GroupBy(p => p.y)
            .OrderBy(p => p.Key)
            .ElementAt(index)
            .AsEnumerable();

    }

    public IEnumerable<Vector2> Column(int index)
    {
        if (index < 0 || index >= Columns) return new List<Vector2>();

        return gridPoints
            .GroupBy(p => p.x)
            .OrderBy(p => p.Key)
            .ElementAt(index)
            .AsEnumerable();

    }

}
