using UnityEngine;

public class PairingObjectLauncher : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    
    private Vector3 _startPos;
    [SerializeField]
    private Vector3 _endPosition = Vector3.up * 4;

    private float _t = 0;
    private void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        _t += Time.deltaTime * _speed;
        if (_t >= 1)
        {
            Debug.Log($"Failed to trigger.. killing anyway");
            Destroy(gameObject);
            
        }
        _t = Mathf.Clamp01(_t);
        var pos = Bezier2(_startPos, _startPos + Vector3.up * 4, _endPosition, _t);
        transform.position = pos;
        
    }
    
    
    public static Vector2 Bezier2(Vector2 s, Vector2 p, Vector2 e, float t)
    {
        float rt = 1 - t;
        return rt * rt * s + 2 * rt * t * p + t * t * e;
    }
    
}
