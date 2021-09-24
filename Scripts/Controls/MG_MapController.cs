using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MG_MapController : MonoBehaviour
{
    [SerializeField]
    private GameObject cursorObj;//объект курсора
    [SerializeField]
    private bool rightClicked = false;//правый клик мыши было зафиксировано нажатие

    [SerializeField]
    private MG_FloorConstructor floorConstructor;//[R] конструктор пола
    [SerializeField]
    private MG_WallConstructor wallConstructor;//[R] конструктор стен
    [SerializeField]
    private MG_EntryConstructor entryConstructor;//[R] конструктор entry
    [SerializeField]
    private MG_RoomConstructor roomConstructor;//[R] конструктор комнат
    [SerializeField]
    private MG_PropConstructor propConstructor;//[R] конструктор prop'ов
    [SerializeField]
    private MG_Editor editor;//[R] Настройки редактора

    void Awake()
    {
        if (floorConstructor == null)
            Debug.Log("<color=red>MG_MapController Awake(): MG_FloorConstructor не прикреплен!</color>");
        if (wallConstructor == null)
            Debug.Log("<color=red>MG_MapController Awake(): MG_WallConstructor не прикреплен!</color>");
        if (entryConstructor == null)
            Debug.Log("<color=red>MG_MapController Awake(): MG_EntryConstructor не прикреплен!</color>");
        if (propConstructor == null)
            Debug.Log("<color=red>MG_MapController Awake(): MG_PropConstructor не прикреплен!</color>");
        if (editor == null)
            Debug.Log("<color=red>MG_MapController Awake(): MG_Editor не прикреплен!</color>");
    }

    void Update()
    {
        CursorTarget();
        if (Input.GetMouseButton(0))//Левый клик мыши без отжатия
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;//ФИКС ДЛЯ ТОГО ЧТОБЫ НЕЛЬЗЯ БЫЛО КЛИКНУТЬ ЧЕРЕЗ UI, багнутый
            MG_TileMap _map = editor.GetCurrentMap();

            Vector3 _worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//получаем координаты клика мыши
            int _floorN = editor.GetFloorNumber();//получить этаж
            Vector3Int _pos = _map.GetCellPos(_worldPos, _floorN);//переводим координаты клика мыши в Vector3Int
            Direction _dir = editor.GetDirection();//получить направление

            switch (editor.GetEditorMode())
            {
                case EditorMode.Floor:
                    {
                        switch (editor.GetFloorTypeMode())
                        {
                            case FloorType.Carpet://активна кнопка "Floor"
                                {
                                    MG_FloorProperty _property = editor.GetFloorProperty(FloorType.Carpet);//получаем свойство выбранное в редакторе (пол)
                                    Color _color = editor.GetFloorColor();//получаем цвет комнаты
                                    bool _isFloorCreated = floorConstructor.SafeConstruct(_pos, _property, _color, _map, _floorN);//вызываем метод для постройки пола с проверками. Также получаем результат (TRUE - пол создан)
                                    bool _roomMode = editor.GetRoomMode();//получаем включен ли режим создания комнаты
                                    if (_roomMode)//если включен, то идем дальше
                                    {
                                        if (_isFloorCreated)//если пол был создан, то приступаем к строительству комнаты
                                        {
                                            MG_WallProperty _propertyWall = editor.GetBasicWallProperty();//получаем свойство стены
                                            roomConstructor.ConstructRoom(_pos, _propertyWall, _color, _map, _floorN);//начинаем создавать/расширять комнату
                                        }
                                    }
                                    break;
                                }
                            case FloorType.Grass://активна кнопка "Grass"
                                {
                                    MG_FloorProperty _property = editor.GetFloorProperty(FloorType.Grass);//получаем свойство выбранное в редакторе (трава)
                                    floorConstructor.SafeConstruct(_pos, _property, Color.white, _map, _floorN);//вызываем метод для постройки пола с проверками
                                    break;
                                }
                            case FloorType.None://активна кнопка "Eraser"
                                {
                                    foreach (var _d in MG_Direction.Basic)//Удаляем стены с каждого 4-х направления
                                    {
                                        wallConstructor.Remove(_pos, _d, _map, _floorN);//Удаляем стены
                                    }
                                    propConstructor.SafeRemove(_pos, _map, _floorN);//удаляем Prop
                                    floorConstructor.SafeRemove(_pos, _map, _floorN);//удаляем комнату по заданным координатам
                                    break;
                                }
                        }
                        break;
                    }
                case EditorMode.Walls:
                    {
                        if (!_dir.Equals(Direction.None))//если направление задано, то строим
                        {
                            MG_WallProperty _property = editor.GetWallProperty();//получаем свойство                  
                            wallConstructor.SafeConstruct(_pos, _property, _dir, _map, _floorN);//создаем                                           
                        }
                        else//если направление НЕ задано, то удаляем ВСЕ СТЕНЫ
                        {
                            wallConstructor.SafeRemoveAll(_pos, _map, _floorN);//удаляем все стены
                        }
                        break;
                    }
                case EditorMode.Doors:
                    {
                        if (!_dir.Equals(Direction.None))//если направление задано, то строим
                        {
                            MG_WallProperty _property = editor.GetBasicWallProperty();//получаем свойство стены                                              
                            wallConstructor.SafeConstruct(_pos, _property, _dir, _map, _floorN);//создаем

                            MG_EntryProperty _propertyE = editor.GetEntryProperty(EntryType.Door);//получаем свойство                  
                            entryConstructor.SafeConstruct(_pos, _propertyE, _dir, _map, _floorN);//создаем 
                        }
                        else//если направление НЕ задано, то удаляем ВСЕ СТЕНЫ
                        {
                            entryConstructor.SafeRemoveAll(_pos, _map, _floorN, EntryType.Door);//удаляем все entries
                        }
                        break;
                    }
                case EditorMode.Windows:
                    {
                        if (!_dir.Equals(Direction.None))//если направление задано, то строим
                        {
                            MG_WallProperty _property = editor.GetBasicWallProperty();//получаем свойство стены                                              
                            wallConstructor.SafeConstruct(_pos, _property, _dir, _map, _floorN);//создаем

                            MG_EntryProperty _propertyE = editor.GetEntryProperty(EntryType.Window);//получаем свойство                  
                            entryConstructor.SafeConstruct(_pos, _propertyE, _dir, _map, _floorN);//создаем 
                        }
                        else//если направление НЕ задано, то удаляем ВСЕ СТЕНЫ
                        {
                            entryConstructor.SafeRemoveAll(_pos, _map, _floorN, EntryType.Window);//удаляем все entries
                        }
                        break;
                    }
                case EditorMode.Props:
                    {
                        if (!_dir.Equals(Direction.None))//если направление задано, то строим
                        {
                            MG_PropProperty _property = editor.GetPropProperty();//получаем свойство стены                                              
                            propConstructor.SafeConstruct(_pos, _property, _dir, _map, _floorN);//создаем    
                        }
                        else//если направление НЕ задано, то удаляем
                        {
                            propConstructor.SafeRemove(_pos, _map, _floorN);//удаление  
                        }
                        break;
                    }
                    //default: return;
            }
            //SizeX = map.size.x;
            //SizeY = map.size.y;
            if (rightClicked) rightClicked = false;
        }
        else if (Input.GetMouseButton(1))//Правый клик мыши без отжатия
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;//ФИКС ДЛЯ ТОГО ЧТОБЫ НЕЛЬЗЯ БЫЛО КЛИКНУТЬ ЧЕРЕЗ UI, багнутый
            MG_TileMap _map = editor.GetCurrentMap();//получить активную карту
            Vector3 _worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//получаем координаты клика мыши
            int _floorN = editor.GetFloorNumber();//получить этаж
            Vector3Int _pos = _map.GetCellPos(_worldPos, _floorN);//переводим координаты клика мыши в Vector3Int
            Direction _dir = editor.GetDirection();//получить номер
            switch (editor.GetEditorMode())
            {
                case EditorMode.Floor:
                    {
                        MG_FloorProperty _property = editor.GetFloorProperty(FloorType.Grass);//получаем свойство выбранное в редакторе
                        floorConstructor.SafeConstruct(_pos, _property, Color.white, _map, _floorN);
                        break;
                    }
                //case EditorMode.FloorEraser:
                //    {
                //        //propManager.PlaceProp(_chosenCellByMouse, Direction.None);
                //        //if (CreateWalls) floorManager.RemoveFloor(_chosenCellByMouse);
                //        //floorManager.PlaceGrass(_chosenCellByMouse, ChosenFloorPropForGrass, true);
                //        break;
                //    }
                case EditorMode.Walls:
                    {
                        wallConstructor.SafeRemove(_pos, _dir, _map, _floorN);//удаляем стену по направлению
                        break;
                    }
                case EditorMode.Doors:
                    {
                        entryConstructor.SafeRemove(_pos, _dir, _map, _floorN, EntryType.Door);//удаляем entry по направлению
                        break;
                    }

                case EditorMode.Windows:
                    {
                        entryConstructor.SafeRemove(_pos, _dir, _map, _floorN, EntryType.Window);//удаляем entry по направлению
                        break;
                    }
                case EditorMode.Props:
                    {
                        propConstructor.SafeRemove(_pos, _map, _floorN);//удаление  
                        break;
                    }
                    //default: return;
            }
            if (!rightClicked) rightClicked = true;
        }
    }

    void CursorTarget()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        MG_TileMap _map = editor.GetCurrentMap();
        MapType _mapType = _map.GetMapType();
        int _floorN = editor.GetFloorNumber();
        Tilemap _tileMap = _map.GetFloorMap(_floorN);
        if (_mapType.Equals(MapType.Isometric2D))
        {
            //https://stackoverflow.com/questions/54357827/mouse-events-for-unity3d-isometric-tile-map
            Vector2 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int _gridPos = _tileMap.WorldToCell(_mousePos);
            Vector2 _mousePosUpd = _tileMap.CellToWorld(_gridPos);
            cursorObj.transform.position = _mousePosUpd;
        }
        else
        {
            Vector3 _worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int _chosenCell = _tileMap.WorldToCell(_worldPos);
            cursorObj.transform.position = new Vector3(_chosenCell.x + 0.5f, _chosenCell.y + 0.5f, 0);
        }
    }

}
