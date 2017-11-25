using UnityEngine;

public class RoomGameLogic : MonoBehaviour
{
    private GameObject _playerObj;
    private Rigidbody2D _playerObjRigidbody2D;

    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private Camera _camera;
    
    private Vector2 _jumpForce = new Vector2(250f, 200f);
    
    private void Awake()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }
        
        var topRightPoint = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        _leftWall.transform.position = new Vector3(-topRightPoint.x, _leftWall.transform.position.y, _leftWall.transform.position.z);
        _rightWall.transform.position = new Vector3(topRightPoint.x, _rightWall.transform.position.y, _rightWall.transform.position.z);
        
        var view = (GameObject) Instantiate(Resources.Load("Player", typeof(GameObject)));

        _playerObj = (GameObject) Instantiate(Resources.Load("Player2D", typeof(GameObject)));
        view.transform.parent = _playerObj.transform;

        _playerObjRigidbody2D = _playerObj.GetComponent<Rigidbody2D>();
        
        _playerObj.transform.position = new Vector2(0, -2.217f);
        
        _playerObj.GetComponent<RoomPlayerIO>().RoomGameLogic = this;;
        
        PlayerState.LoadFromPrefs();
    }

    public void ClickOnPlayer()
    {
        if (Mathf.Abs(_playerObjRigidbody2D.velocity.magnitude) <= Mathf.Epsilon)
        {
            PlayerState.Tickle();

            float xForce = 100; //(Random.value - 0.5f) * _jumpForce[0];
            _playerObjRigidbody2D.AddForce(new Vector2(xForce, _jumpForce[1]));
        }        
    }
}
