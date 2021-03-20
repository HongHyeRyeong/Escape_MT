using UnityEngine;

public class Item : MonoBehaviour
{
    public SpriteRenderer item;
    public Sprite[] sprites;

    public int itemType;
    private float moveSpeed;

    private void Awake()
    {
        int randomItemType = Random.Range(1, 3);
        SetData(randomItemType);

        moveSpeed = Random.Range(2f, 4f);
    }

    public void SetData(int itemType)
    {
        this.itemType = itemType;
        item.sprite = sprites[itemType];
    }

    private void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EndPos"))
        {
            Destroy(this.gameObject);
        }
    }
}
