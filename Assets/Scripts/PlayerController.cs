using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public GameObject warningObject;

    [SerializeField] private AudioClip _warningSound;
    [SerializeField] private AudioClip _collideSound;
    [SerializeField] private AudioSource _effectSoundSource;
    [SerializeField] private AudioSource _moveSoundSource;
    [SerializeField] private float _maxSoundSpeed = 20;

    [SerializeField] private Vector3 _warningOffset = new Vector3(0, 0.5f, 0.5f);
    private Camera _camera;
    private bool _lost = false;
    public bool Lost => _lost;
    
    private bool _win = false;
    public bool Win => _win;
    
    [Range(0.2f, 10)]
    public float ForceMultiplier = 1;
    
    [SerializeField]
    private bool _isLeft;

    public bool IsLeft => _isLeft;    
    public bool IsRight => !_isLeft;
    RepairActions controls = null;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        warningObject.transform.parent = null;
    }

    private void OnEnable()
    {
        _lost = false;
        _win = false;
    }

    public void SetRepairActions(RepairActions controls)
    {
        this.controls = controls;
        Debug.Log($"Controls set {controls}");
    }

    void Update()
    {
        var speed = _rigidbody.velocity.magnitude;
        if (speed > 1 && !_moveSoundSource.isPlaying)
        {
            _moveSoundSource.Play();
        }
        else
        {
            _moveSoundSource.Pause();
        }

        _moveSoundSource.volume = Mathf.Clamp01(speed / _maxSoundSpeed);
        if (warningObject && warningObject.activeSelf)
        {
            warningObject.transform.position = transform.position + _warningOffset;
        }
    }

    void FixedUpdate()
    {
        if(controls == null) { return; }

        var move = Vector2.zero;
        if (_isLeft)
        {
            move = controls.Player.MoveLeftCharacter.ReadValue<Vector2>();
        }
        else
        {
            move = controls.Player.MoveRightCharacter.ReadValue<Vector2>();
        }
        Vector3 dir = new Vector3(move.x, 0, move.y) * ForceMultiplier;
        _rigidbody.AddForce( dir , ForceMode.Acceleration);
        _rigidbody.AddTorque( dir , ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger: {other.tag} {other.name}");
        if (other.tag == "LoseTrigger")
        {
            _lost = true;
        }
        else if (other.tag == "WinTrigger")
        {
            _win = true;
        }
        else if (other.tag == "Warning")
        {
            StartWarning();
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "WinTrigger")
        {
            _win = false;
        }
        else if (other.tag == "Warning")
        {
            EndWarning();
        }
    }

    void StartWarning()
    {
        _effectSoundSource.PlayOneShot(_warningSound);
        warningObject?.SetActive(true);
        Debug.Log("Danger");
    }
    void EndWarning()
    {
        warningObject?.SetActive(false);
        Debug.Log("Danger Clear");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Plane")
        {
            return;
        }
        Debug.Log($"Collide {other}");
        _effectSoundSource.PlayOneShot(_collideSound);
    }
}
