using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private Key _key;
    [SerializeField]
    private bool _enter;
    [SerializeField]
    private SpriteRenderer _sprite;
    private void Update()
    {
        if (_key._hasKey == true)
        {
            _sprite.color = Color.green;
        }
        if (_enter == true)
        {
            Debug.Log($"EnterDoor");
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log($"PressE");
                if (_key._hasKey == true)
                    OpenDoor();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _enter = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _enter = false;
    }
    private void OpenDoor()
    {
        transform.position += _offset;
    }
}
