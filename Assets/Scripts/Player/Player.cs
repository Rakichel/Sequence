using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rigidbody;

    public bool IsDashing = false;     // �뽬 �������� ����

    private void Start()
    {
        if (Rigidbody == null)
            Rigidbody = GetComponent<Rigidbody2D>();
    }
}