using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        rend.flipY = true;
    }
}
