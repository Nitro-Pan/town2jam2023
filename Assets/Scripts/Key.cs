using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    public bool _hasKey;
    [SerializeField]
    private bool _inRange;
    private void Awake()
    {
        _hasKey = false;
    }
    private void Update()
    {
        if (_inRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.L))
            {
                _hasKey = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _inRange = false;
    }
}
