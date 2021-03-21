﻿using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EndPos"))
        {
            Destroy(this.gameObject);
        }
    }
}
