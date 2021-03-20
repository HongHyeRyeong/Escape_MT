using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float Default_MoveSpeed = 4f;
    private readonly int Attack_AlcoholGauge = 20;
    private readonly int Decrease_AlcoholGauge = 5;
    private readonly float AddScore_ArrowItem = 100;

    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
                instance = (PlayerController)FindObjectOfType(typeof(PlayerController));
            return instance;
        }
    }

    private float moveSpeed;
    private float alcoholGauge;
    private bool isReverseKey;

    public void Init()
    {
        moveSpeed = Default_MoveSpeed;
        UpdateAlcoholGauge(0);
        isReverseKey = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!GameManager.Instance.isPlay)
            return;

        int reverse = isReverseKey ? -1 : 1;

        float horizontal = Input.GetAxisRaw("Horizontal") * reverse;
        if (0 < horizontal || horizontal < 0)
        {
            transform.Translate(new Vector3(horizontal * moveSpeed * Time.deltaTime, 0f, 0f));
        }

        float vertical = Input.GetAxisRaw("Vertical") * reverse;
        if (0 < vertical || vertical < 0)
        {
            transform.Translate(new Vector3(0f, vertical * moveSpeed * Time.deltaTime, 0f));
        }
    }

    public void AttackSenior()
    {
        if (alcoholGauge == 100)
        {
            GameManager.Instance.GameOver();
        }

        UpdateAlcoholGauge(Mathf.Min(100, alcoholGauge + Attack_AlcoholGauge));
    }

    public void DecreaseAlcoholGauge()
    {
        Debug.Log("DecreaseAlcoholGauge");

        UpdateAlcoholGauge(Mathf.Max(0, alcoholGauge - Decrease_AlcoholGauge));
    }

    public void PickupItem(int itemType)
    {
        Debug.Log("PickupItem itemType : " + itemType);

        switch (itemType)
        {
            case 1: // 숙취해소제 소
                UpdateAlcoholGauge(Mathf.Max(0, alcoholGauge - Attack_AlcoholGauge));
                break;
            case 2: // 숙취해소제 대
                UpdateAlcoholGauge(0);
                break;
            case 3: // 화살표
                ScoreManager.Instance.AddScore(AddScore_ArrowItem);
                break;
            default:
                break;
        }
    }

    private void UpdateAlcoholGauge(float gauge)
    {
        alcoholGauge = gauge;
        UIManager.Instance.UpdateAlcoholGauge(alcoholGauge);

        // 기본
        moveSpeed = Default_MoveSpeed;
        isReverseKey = false;

        if (30 <= alcoholGauge) // 1단계
        {
            moveSpeed = Default_MoveSpeed * 0.85f;
        }

        if (60 <= alcoholGauge) // 2단계
        {
            isReverseKey = true;
        }

        if (100 <= alcoholGauge) // 3단계
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            PickupItem(other.gameObject.GetComponent<Item>().itemType);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            AttackSenior();
        }
    }
}