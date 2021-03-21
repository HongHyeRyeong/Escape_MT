using UnityEngine;

public class Item : MonoBehaviour
{
    public SpriteRenderer item;
    public Sprite[] sprites;

    public int itemType;
    private float moveSpeed;

    private void Awake()
    {
        if (!GameManager.Instance.isPlay || GameManager.Instance.isMiniGame)
            Destroy(gameObject);

        SetData();
    }

    public void SetData()
    {
        this.itemType = Random.Range(1, 4);
        item.sprite = sprites[itemType - 1];

        moveSpeed = Random.Range(2f, 4f);
    }

    private void Update()
    {
        if (!GameManager.Instance.isPlay || GameManager.Instance.isMiniGame)
            return;

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
