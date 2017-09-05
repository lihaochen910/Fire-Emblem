using UnityEngine;
using System.Collections.Generic;
using Map;

[RequireComponent(typeof(BoxCollider2D))]
public class s_RangeCheck : MonoBehaviour {

    IEnumerable<Vector2> Neighbors()
    {
        foreach (var dir in SquareGrid.DIRS)
            yield return new Vector2(transform.position.x,transform.position.y)+new Vector2(dir.x,dir.y);
    }
    Ray2D ray;
    void OnEnable()
    {
        if (GetComponentInChildren<SpriteRenderer>().sprite.Equals(s_RangeManager.s_unPassable))
            GetComponent<BoxCollider2D>().enabled = false;
        else GetComponent<BoxCollider2D>().enabled = true;
        //Invoke("check", Time.deltaTime * 100);      
    }
    void check()
    {
        foreach (var neighbor in Neighbors())
        {
            ray = new Ray2D(new Vector2(transform.position.x, transform.position.y), neighbor - new Vector2(transform.position.x, transform.position.y));
            if (Physics2D.Raycast(ray.origin, ray.direction))
                return;
        }

        s_RangeManager._ins.pool.Store(gameObject);
        gameObject.SetActive(false);
    }

}
