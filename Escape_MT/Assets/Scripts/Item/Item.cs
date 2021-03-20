using UnityEngine;

public class Item : MonoBehaviour
{
    public SpriteRenderer item;
    public Sprite[] sprites;

    public int itemType;
    private float moveSpeed;

    private void Awake()
    {
<<<<<<< Updated upstream
        int randomItemType = Random.Range(1, 3);
        SetData(randomItemType);

        moveSpeed = Random.Range(2f, 4f);
=======
        SetData();
>>>>>>> Stashed changes
    }

    public void SetData()
    {
<<<<<<< Updated upstream
        this.itemType = itemType;
        item.sprite = sprites[itemType];
=======
        this.itemType = Random.Range(1, 4);
        item.sprite = sprites[itemType - 1];

        moveSpeed = Random.Range(2f, 4f);
>>>>>>> Stashed changes
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
