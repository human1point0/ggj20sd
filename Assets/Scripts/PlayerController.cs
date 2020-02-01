using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetRepairActions(RepairActions controls)
    {
        this.controls = controls;
        Debug.Log($"Controls set {controls}");
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
