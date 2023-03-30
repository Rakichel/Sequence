using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rigidbody;

    public bool IsDashing = false;     // 대쉬 상태인지 여부

    private void Start()
    {
        if (Rigidbody == null)
            Rigidbody = GetComponent<Rigidbody2D>();
    }
}