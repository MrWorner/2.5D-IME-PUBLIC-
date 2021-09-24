using UnityEngine;

[AddComponentMenu("Camera-Control/Keyboard Orbit")]
public class MG_CameraControl : MonoBehaviour
{
    private static MG_CameraControl instance;//в редакторе только один объект должен быть создан
    [SerializeField]
    private GameObject camObj;//[R] объект камеры
    //[SerializeField]
    private Camera cam;//[R] компонен камеры

    [SerializeField]
    private float speedCameraMovement = 20.0f;
    [SerializeField]
    private float defaultZoom = 10f;

    private float x = 0f;
    private float y = 0f;
    private float z = -10f;

    [SerializeField]
    private float zoomMin = 5f; //Минимальный зум
    [SerializeField]
    private float zoomMax = 25f; //Максимальный зум
    [SerializeField]
    private Vector3 dragOrigin;
    //ZENDA ACADEMY переменные для управления средней кнопки мыши
    private Vector3 mouseOriginPoint;
    private Vector3 offset;
    private bool dragging;
    [SerializeField]
    private BoxCollider2D boxCollider;
    //================================Методы
    void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.Log("<color=red>MG_CameraControl Awake(): MG_CameraControl может быть только один компонент на Сцене, другие не нужны.</color>");
        if (camObj == null)
            Debug.Log("<color=red>MG_CameraControl Awake(): объект для camObj не прикреплен!</color>");
        //if (cam == null)
        //    Debug.Log("<color=red>MG_CameraControl Awake(): Camera не прикреплен!</color>");
        cam = Camera.main;
        x = transform.position.x; //Устанавливаем стандартное значение после старта игры (Иначе баг будет с позициями камеры)
        y = transform.position.y; //Устанавливаем стандартное значение после старта игры (Иначе баг будет с позициями камеры)
        cam.orthographicSize = 10f;
    }


    void LateUpdate()
    {
        MouseControl();
        KeyboardControl();
    }

    /// <summary>
    /// Управление клавиатурой
    /// </summary>
    private void KeyboardControl()
    {
        //BEGIN Управление движением камеры с помощью кнопок
        if (Input.anyKey)//Хоть какая либо клавиша нажата
        {
            x = camObj.transform.position.x;
            y = camObj.transform.position.y;
            z = camObj.transform.position.z;
            if (Input.GetKey(KeyCode.D))
            {
                float _bonusSpeed = 0;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _bonusSpeed = speedCameraMovement * 2;
                }

                x = x + ((speedCameraMovement + _bonusSpeed) * Time.deltaTime);
                camObj.transform.position = new Vector3(x, y, z);

            }
            if (Input.GetKey(KeyCode.A))
            {
                float _bonusSpeed = 0;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _bonusSpeed = speedCameraMovement * 2;
                }
                x = x - ((speedCameraMovement + _bonusSpeed) * Time.deltaTime);
                camObj.transform.position = new Vector3(x, y, z);
            }
            if (Input.GetKey(KeyCode.S))
            {
                float _bonusSpeed = 0;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _bonusSpeed = speedCameraMovement * 2;
                }
                y = y - ((speedCameraMovement + _bonusSpeed) * Time.deltaTime);
                camObj.transform.position = new Vector3(x, y, z);
            }
            if (Input.GetKey(KeyCode.W))
            {
                float _bonusSpeed = 0;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _bonusSpeed = speedCameraMovement * 2;
                }
                y = y + ((speedCameraMovement + _bonusSpeed) * Time.deltaTime);
                camObj.transform.position = new Vector3(x, y, z);
            }
            //END Управление движением камеры с помощью кнопок

            //BEGIN Управление зумом с помощью клавиши
            float _curZoomLevel = cam.orthographicSize;

            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                cam.orthographicSize = zoomMin;
            }

            if (Input.GetKey(KeyCode.KeypadMinus))
            {
                cam.orthographicSize = zoomMax;
            }

            if (Input.GetKey(KeyCode.Keypad5))
            {
                cam.orthographicSize = defaultZoom;
            }
            //END Управление зумом с помощью клавиши


            //BEGIN-------------------ОГРАНИЧЕНИЕ ПО ПОЛЮ
            float vertExtent = cam.orthographicSize;
            float horizExtent = vertExtent * Screen.width / Screen.height;

            Vector3 linkedCameraPos = cam.transform.position;
            Bounds areaBounds = boxCollider.bounds;

            camObj.transform.position = new Vector3(
                Mathf.Clamp(linkedCameraPos.x, areaBounds.min.x + horizExtent, areaBounds.max.x - horizExtent),
                Mathf.Clamp(linkedCameraPos.y, areaBounds.min.y + vertExtent, areaBounds.max.y - vertExtent),
                linkedCameraPos.z);
            //END-------------------ОГРАНИЧЕНИЕ ПО ПОЛЮ
        }
    }

    /// <summary>
    /// Управление мышкой
    /// </summary>
    private void MouseControl()
    {
        //Скролл вилл, добавить ограничение по Зуму
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * (10f * cam.orthographicSize * 0.1f), zoomMin, zoomMax);

        //END Управление зумом с помощью средней кнопкой мыши

        //BEGIN Нажатие по клетки левой кнопки мышки
        //if (Input.GetMouseButton(0))//КЛИК МЫШИ БЕЗ ОТПУСКАНИЯ
        //{
        //}
        //END Нажатие по клетки левой кнопки мышки

        //if (Input.GetMouseButton(1))//КЛИК МЫШИ БЕЗ ОТПУСКАНИЯ (ПРАВЫЙ КЛИК)
        //{
        //}

        //ZENDA ACADEMY для управления средней кнопки мыши
        if (Input.GetMouseButton(2))
        {
            offset = (cam.ScreenToWorldPoint(Input.mousePosition) - camObj.transform.position);
            if (!dragging)
            {
                dragging = true;
                mouseOriginPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                //mouseOriginPoint = camObj.transform.position;
            }
        }
        else
        {
            dragging = false;
        }
        if (dragging)
        {      
            camObj.transform.position = mouseOriginPoint - offset;
        }          
        //END---ZENDA ACADEMY для управления средней кнопки мыши




   


    }

}