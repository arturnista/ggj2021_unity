using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = default;
    [Header("Speed")]
    [SerializeField] private float _forwardWalkSpeed = default;
    [SerializeField] private float _forwardRunSpeed = default;
    [SerializeField] private float _backwardMoveSpeed = default;
    [SerializeField] private float _gravity = default;
    private Vector3 _velocity = default;
    [Header("Jump")]
    [SerializeField] private float _jumpHeight = default;
    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck = default;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask = default;
    [Header("Stamina/Run system")]
    [SerializeField] private float _totalStamina = default;
    [SerializeField] private float _staminaRunCost = default;
    [SerializeField] private float _staminaRecoverAmount = default;
    [SerializeField] private float _currentStamina = default;
    [SerializeField] private float _staminaCooldown = default;
    [SerializeField] private bool _isGrounded = default;
    [Header("SFX")]
    [SerializeField] private AudioClip[] _footstepsSfx = default;
    AudioSource _audioSource = default;
    private Vector3 lastPosition;
    private float totalMoved;
    private bool _isRunning = false;
    public bool IsRunning => _isRunning;
    private bool _canRun = true;

    void Start()
    {
        _currentStamina = _totalStamina;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        Vector3 movement = transform.right * horizontalMovement + transform.forward * verticalMovement;
        Vector3 movementNormalized = Vector3.Normalize(movement);

        float forwardSpeed = _isRunning ? _forwardRunSpeed : _forwardWalkSpeed;
        float moveSpeedToUse = (verticalMovement >= 0) ? forwardSpeed : _backwardMoveSpeed;
        _characterController.Move(movementNormalized * moveSpeedToUse * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;
        StaminaDrainCalculation();

        _characterController.Move(_velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }

        RunCheck();
        FootstepsSoundEffects();
        // Debug.Log(_characterController.velocity.magnitude);
    }

    private void Jump()
    {
        _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    }

    private void RunCheck()
    {
        if (_canRun)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                StartRunning();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || _currentStamina <= 0)
            {
                StopRunning();
            }
        }
    }

    private void StartRunning()
    {
        if (_currentStamina <= 0) return;
        _isRunning = true;
    }

    private void StopRunning()
    {
        StartCoroutine(RunningCooldown());
        _isRunning = false;
    }

    private void StaminaDrainCalculation()
    {
        if (_isRunning && _characterController.velocity.magnitude > 0f)
        {
            _currentStamina -= _staminaRunCost * Time.deltaTime;
        }
        else
        {
            if (_currentStamina < _totalStamina && !Input.GetKey(KeyCode.LeftShift))
            {
                _currentStamina += _staminaRecoverAmount * Time.deltaTime;
            }
        }
    }

    IEnumerator RunningCooldown()
    {
        _canRun = false;
        yield return new WaitForSeconds(_staminaCooldown);
        _canRun = true;
    }

    private void FootstepsSoundEffects()
    {
        if (_characterController.velocity.magnitude > 0 && _characterController.isGrounded && !_audioSource.isPlaying)
        {
            float moveFromLastPosition = (lastPosition - _characterController.transform.position).magnitude;
            lastPosition = _characterController.transform.position;
            totalMoved += moveFromLastPosition;

            if (totalMoved >= 2.5f && !_isRunning)
            {
                _audioSource.PlayOneShot(GetRandomFootstepClip());
                totalMoved = 0f;
            }
            else 
            if (totalMoved >=3f && _isRunning)
            {
                _audioSource.PlayOneShot(GetRandomFootstepClip());
                totalMoved = 0f;
            }
        }
    }

    AudioClip GetRandomFootstepClip()
    {
        return _footstepsSfx[Random.Range(0, _footstepsSfx.Length)];
    }
}
