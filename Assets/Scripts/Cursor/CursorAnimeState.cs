using UnityEngine;
using System.Collections;

public class CursorAnimeState : MonoBehaviour {

    public Sprite CursorSprite;
    SpriteRenderer sRenderer;

    float timer = 0;
    public float TimeInterval = 0.5f;

    // Use this for initialization
    void Start()
    {
        sRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    int index = 0;
    void Update()
    {

        if (timer > TimeInterval)
        {
            if(index++ % 4 == 0 || index % 4 == 1 || index % 4 == 2)
                sRenderer.sprite = CursorSprite;
            else sRenderer.sprite = null;
            timer = 0;
        }
        timer += Time.deltaTime;

    }
}
