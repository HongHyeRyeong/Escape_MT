using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    public SpriteRenderer rend;

    void Update()
    {
        rend.flipY = true;
    }
}
