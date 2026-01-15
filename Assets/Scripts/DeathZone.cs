using System;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health _health = other.GetComponent<Health>();
        if (_health != null)
        {
            //instant die
            _health.TakeDamage(99);
        }
    }
}
