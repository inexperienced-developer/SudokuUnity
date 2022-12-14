using InexperiencedDeveloper.Core;
using InexperiencedDeveloper.Utils.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] private GameObject m_PlayerPrefab;
    public GameObject PlayerPrefab => m_PlayerPrefab;

    [Header("Raycasting")]
    private Tile selectedTile;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;
    [SerializeField] private GraphicRaycaster raycaster;

    [Space]
    [Header("Win Condition")]
    [HideInInspector] public List<Tile> tiles;
    [SerializeField] private SectionWin startingSection;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        if(UIManager.Instance.Raycaster == null)
        {
            IDLogger.LogError($"UIManager.Instance.Raycaster is null");
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //Set up the new Pointer Event
            pointerEventData = new PointerEventData(eventSystem);
            //Set the Pointer Event Position to that of the game object
            pointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            UIManager.Instance.Raycaster?.Raycast(pointerEventData, results);

            if (results.Count > 0)
            {
                var tile = results[0].gameObject.GetComponent<Tile>();
                if(tile != null && !tile.defaultVisible)
                {
                    selectedTile?.SelectTile(false);
                    selectedTile = tile;
                    selectedTile?.SelectTile(true);
                }

            }
        }
    }

    public void SetValue(int value)
    {
        if(selectedTile != null)
        {
            selectedTile.TryValue(value);
        }
        bool unsolved = false;
        for(int i = 0; i < tiles.Count; i++)
        {
            var tile = tiles[i];
            if (!tile.Solved)
                unsolved = true;
            PlayerPrefs.SetString("active", $"Saved game");

        }
        foreach (var tile in tiles)
        {

        }

        if (unsolved) return;
        Win();
    }

    private void Win()
    {
        foreach (var tile in tiles)
            tile.SetColor(Color.white);
        startingSection.WinRoutine();
        PlayerPrefs.DeleteAll();
    }
    
    public void ClearTiles()
    {
        foreach(var tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        tiles.Clear();
        selectedTile = null;
    }
}
