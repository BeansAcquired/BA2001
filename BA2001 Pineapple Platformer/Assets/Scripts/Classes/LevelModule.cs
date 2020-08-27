using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModule : MonoBehaviour
{
    public LevelModuleBottomType BottomType;
    private static float tileOffest = 1.5f;
    public static LevelModule GetInstance(LevelModuleEdgePosition position, Sprite sprite)
    {
        LevelModule output = new GameObject("LevelModuleInstance").AddComponent<LevelModule>();
        SpriteRenderer edge = new GameObject("Edge").AddComponent<SpriteRenderer>();
        edge.transform.parent = output.transform;
        edge.sprite = sprite;
        edge.transform.position = GetPosition(position, output.transform.position);
        edge.transform.localScale = GetScale(position);

        BoxCollider2D collider = new GameObject("collider").AddComponent<BoxCollider2D>();
        collider.transform.parent = edge.transform;
        collider.size = GetSize(position);
        collider.transform.localPosition = Vector3.zero;

        return output;
    }

    private static Vector3 GetPosition(LevelModuleEdgePosition position, Vector3 parentPosition)
    {
        switch (position)
        {
            case LevelModuleEdgePosition.Top:
                return new Vector3(parentPosition.x, tileOffest, parentPosition.z);
            case LevelModuleEdgePosition.Left:
                return new Vector3(-tileOffest, parentPosition.y, parentPosition.z);
            case LevelModuleEdgePosition.Bottom:
                return new Vector3(parentPosition.x, -tileOffest, parentPosition.z);
            case LevelModuleEdgePosition.Right:
                return new Vector3(tileOffest, parentPosition.y, parentPosition.z);
            case LevelModuleEdgePosition.None:
            default:
                return Vector3.zero;
        }
    }

    private static Vector3 GetScale(LevelModuleEdgePosition position)
    {
        switch (position)
        {
            case LevelModuleEdgePosition.Top:
            case LevelModuleEdgePosition.Bottom:
                return new Vector3(3f, 0.25f, 1f);
            case LevelModuleEdgePosition.Left:
            case LevelModuleEdgePosition.Right:
                return new Vector3(0.25f, 3f, 1f);
            case LevelModuleEdgePosition.None:
            default:
                return Vector3.zero;
        }
    }

    private static Vector2 GetSize(LevelModuleEdgePosition position)
    {
        switch (position)
        {
            case LevelModuleEdgePosition.Top:
            case LevelModuleEdgePosition.Bottom:
                return new Vector2(3f, 0.25f);
            case LevelModuleEdgePosition.Left:
            case LevelModuleEdgePosition.Right:
                return new Vector2(0.25f, 3f);
            case LevelModuleEdgePosition.None:
            default:
                return Vector2.zero;
        }
    }

    //public static void Initialize(this LevelModule levelModule, LevelModuleEdgePosition position, Sprite sprite)
    //{
    //    if(position == LevelModuleEdgePosition.None)
    //    {
    //        // default
    //    }
    //    else
    //    {
    //        if((position & LevelModuleEdgePosition.Top) == LevelModuleEdgePosition.Top)
    //        {
    //            SetTop(sprite);
    //        }

    //        if ((position & LevelModuleEdgePosition.Bottom) == LevelModuleEdgePosition.Bottom)
    //        {
    //            // SetBottom
    //        }

    //        if ((position & LevelModuleEdgePosition.Left) == LevelModuleEdgePosition.Left)
    //        {
    //            // SetLeft
    //        }

    //        if ((position & LevelModuleEdgePosition.Right) == LevelModuleEdgePosition.Right)
    //        {
    //            // SetRight
    //        }
    //    }


    //}

    //public static void SetTop(Sprite sprite)
    //{
    //    // Spawn top border
    //    SpriteRenderer top = new GameObject("Top").AddComponent<SpriteRenderer>();
    //    top.sprite = sprite;
    //    top.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
    //    top.transform.localScale = new Vector3(3f, 0.25f, 1f);        

    //    // Check left for corner
    //    // Check right for corner
    //}


}
