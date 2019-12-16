using UnityEngine;

public class ColorSnakeGameController : MonoBehaviour
{
    //Границы экрана от позиции камеры
    public class CameraBounds
    {
        public float Left;
        public float Right;
        public float Up;
        public float Down;
    }

    [SerializeField] private Camera m_MainCamera;
    public Camera MainCamera => m_MainCamera;

    [SerializeField] private ColorSnakeSnake m_Snake;

    [SerializeField] private ColorSnakeTypes m_Types;
    public ColorSnakeTypes Types => m_Types;

    private CameraBounds bounds;

    public CameraBounds Bounds
    {
        get => bounds;
        private set => bounds = value;
    }

    private void Awake()
    {
        Vector2 minScreen = m_MainCamera.ScreenToWorldPoint(Vector3.zero);

        bounds = new CameraBounds
        {
            Left = minScreen.x,
            Right = Mathf.Abs(minScreen.x),
            Up = Mathf.Abs(minScreen.y),
            Down = minScreen.y
        };
    }

    private void Update()
    {
        Vector3 movement = Time.deltaTime * 3f * Vector3.up;
        m_MainCamera.transform.Translate(movement);
        m_Snake.transform.Translate(movement);
    }

    private void Reset()
    {
        m_MainCamera = Camera.main;
    }
}