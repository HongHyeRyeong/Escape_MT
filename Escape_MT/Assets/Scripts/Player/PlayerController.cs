using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float Default_MoveSpeed = 3f;
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

    public Animator animator;

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
        if (!GameManager.Instance.isPlay || GameManager.Instance.isMiniGame)
            return;

        Move();
    }

    private void Move()
    {
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

    public void DecreaseAlcoholGauge()
    {
        Debug.Log("DecreaseAlcoholGauge");

        UpdateAlcoholGauge(Mathf.Max(0, alcoholGauge - Decrease_AlcoholGauge));
    }

    public void AttackSenior()
    {
        if (alcoholGauge == 100 && GameManager.Instance.isPlay)
        {
            animator.SetBool("Overreat", true);

            GameManager.Instance.isPlay = false;
            Invoke("GameOver", 0.5f);
        }

        UpdateAlcoholGauge(Mathf.Min(100, alcoholGauge + Attack_AlcoholGauge));
    }

    private void PickupItem(int itemType)
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
        int level = 1;
        moveSpeed = Default_MoveSpeed;
        isReverseKey = false;

        if (30 <= alcoholGauge) // 1단계
        {
            level = 2;
            moveSpeed = Default_MoveSpeed * 0.85f;
        }

        if (60 <= alcoholGauge) // 2단계
        {
            level = 3;
            isReverseKey = true;
        }

        if (100 <= alcoholGauge) // 3단계
        {
            level = 4;
        }

        if (level == 1 || level == 2)
        {
            animator.SetBool("Drunk", false);
            animator.SetBool("Drunk2", false);
        }
        else if (level == 3)
        {
            animator.SetBool("Drunk", true);
            animator.SetBool("Drunk2", false);
        }
        else if (level == 4)
        {
            animator.SetBool("Drunk", true);
            animator.SetBool("Drunk2", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.Instance.isPlay || GameManager.Instance.isMiniGame)
            return;

        if (other.gameObject.CompareTag("Item"))
        {
            SoundManager.Instance.SetEffect("Item");

            PickupItem(other.gameObject.GetComponent<Item>().itemType);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            int random = Random.Range(1, 9);

            if (random == 1)
            {
                GameManager.Instance.StartMinigame(1);
            }
            else if(random == 2)
            {
                GameManager.Instance.StartMinigame(2);
            }
            else
            {
                AttackSenior();
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        GameManager.Instance.GameOver();
    }
}