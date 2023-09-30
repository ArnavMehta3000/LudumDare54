using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private float _shootingSpeed = 1.0f;
    [SerializeField]
    private float _rotationSpeed = 100.0f;

    private float _currentShootTime = 0.0f;


    private void Update()
    {
        Rotate();

        _currentShootTime += Time.deltaTime;
        if (_currentShootTime > _shootingSpeed)
        {
            _currentShootTime = 0.0f;
            Shoot();
        }
    }

    private void Rotate()
    {
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            transform.Rotate(0, 0, -_rotationSpeed * Time.deltaTime);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
    }
}
