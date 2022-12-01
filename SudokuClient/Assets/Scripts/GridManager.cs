using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //    [SerializeField] private float width, height;

    //    [SerializeField] private GameTile gameTilePrefab;

    //    public Dictionary<Section, List<GameTile>> tileSections = new Dictionary<Section, List<GameTile>>();
    //    public Dictionary<int, List<GameTile>> tileCols = new Dictionary<int, List<GameTile>>();
    //    public Dictionary<int, List<GameTile>> tileRows = new Dictionary<int, List<GameTile>>();
    //    //public Dictionary<Section, List<int>> numInSection = new Dictionary<Section, List<int>>();
    //    //public Dictionary<int, List<int>> numInCols = new Dictionary<int, List<int>>();
    //    //public Dictionary<int, List<int>> numInRows = new Dictionary<int, List<int>>();
    //    public Dictionary<Section, List<int>> availableInSection = new Dictionary<Section, List<int>>();
    //    public Dictionary<int, List<int>> availableInCols = new Dictionary<int, List<int>>();
    //    public Dictionary<int, List<int>> availableInRows = new Dictionary<int, List<int>>();
    //    private int[] availableNums = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //    private List<int> sortedRows = new List<int>();
    //    private List<int> sortedCols = new List<int>();

    //    private IEnumerator Start()
    //    {
    //        int x = 0;
    //        int correct = 0;
    //        int total = 0;
    //        Section[] sections = new Section[9] { Section.I, Section.II, Section.III, Section.IV, Section.V, Section.VI, Section.VII, Section.VIII, Section.IX };
    //        while (x < 1000)
    //        {

    //            Init();
    //            GenerateGrid();
    //            int i = 0;
    //            FillSections(sections);
    //            while (i < 5)
    //            {
    //                yield return StartCoroutine(RowColumnSort());
    //                for (int j = 0; j < 9; j++)
    //                    foreach (var tile in tileCols[j])
    //                        tile.sorted = false;
    //                if (ValidPuzzle(sections)) break;

    //                i++;
    //            }
    //            print(ValidPuzzle(sections));

    //            yield return StartCoroutine(RowColumnSort());
    //            if (ValidPuzzle(sections))
    //            {
    //                correct += 1;
    //                var newGO = new GameObject();
    //                newGO.name = $"Puzzle{correct}";
    //                int turnOff = Random.Range(30, 50);
    //                for (int j = 0; j < 9; j++)
    //                    foreach (var tile in tileCols[j])
    //                    {
    //                        tile.GetComponentInChildren<TMP_Text>().text = tile.value.ToString();
    //                        tile.transform.SetParent(newGO.transform);
    //                    }
    //                while(turnOff > 0)
    //                {
    //                    for (int j = 0; j < 9; j++)
    //                    {
    //                        foreach (var tile in tileCols[j])
    //                        {
    //                            int random = Random.Range(0, 20);
    //                            if (random > 15 && tile.GetComponentInChildren<TMP_Text>(true))
    //                            {
    //                                tile.GetComponentInChildren<TMP_Text>(true).gameObject.SetActive(false);
    //                                turnOff--;
    //                            }
    //                        }
    //                        if (turnOff == 0) break;
    //                    }

    //                }
    //                string localPath = "Assets/Prefabs/Puzzles/" + newGO.name + ".prefab";
    //                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
    //                bool prefabSuccess;
    //                PrefabUtility.SaveAsPrefabAsset(newGO, localPath, out prefabSuccess);
    //                if (prefabSuccess == true)
    //                    Debug.Log("Prefab was saved successfully");
    //                else
    //                    Debug.Log("Prefab failed to save" + prefabSuccess);
    //            }
    //            total += 1;
    //            x++;
    //            for (int j = 0; j < 9; j++)
    //                foreach (var tile in tileCols[j])
    //                    Destroy(tile.gameObject);
    //            print($"Correctly generated: {correct}/{total}");
    //            tileCols.Clear();
    //            tileRows.Clear();
    //            tileSections.Clear();
    //            sortedCols.Clear();
    //            sortedRows.Clear();
    //        }

    //        //StartCoroutine(GenerateValidPuzzle(sections));
    //    }

    //    private IEnumerator GenerateValidPuzzle(Section[] sections)
    //    {
    //        while (true)
    //        {
    //            FillSections(sections);
    //            if (ValidPuzzle(sections)) break;
    //            for (int i = 0; i < 9; i++)
    //            {
    //                foreach(var tile in tileRows[i])
    //                {
    //                    tile.sorted = false;
    //                }
    //            }
    //            RowColumnSort();
    //            print(ValidPuzzle(sections));
    //            yield return new WaitForSeconds(0.1f);
    //        }
    //        print(ValidPuzzle(sections));
    //    }

    //    private void Init()
    //    {
    //        tileSections.Clear();
    //        tileRows.Clear();
    //        tileCols.Clear();

    //        //Sections Init
    //        tileSections.Add(Section.I, new List<GameTile>());
    //        tileSections.Add(Section.II, new List<GameTile>());
    //        tileSections.Add(Section.III, new List<GameTile>());
    //        tileSections.Add(Section.IV, new List<GameTile>());
    //        tileSections.Add(Section.V, new List<GameTile>());
    //        tileSections.Add(Section.VI, new List<GameTile>());
    //        tileSections.Add(Section.VII, new List<GameTile>());
    //        tileSections.Add(Section.VIII, new List<GameTile>());
    //        tileSections.Add(Section.IX, new List<GameTile>());

    //        //Cols/Rows init
    //        for (int i = 0; i < 9; i++)
    //        {
    //            tileCols.Add(i, new List<GameTile>());
    //            tileRows.Add(i, new List<GameTile>());
    //        }
    //    }

    //    private void GenerateGrid()
    //    {
    //        for (int x = 0; x < width; x++)
    //        {
    //            for (int y = 0; y < height; y++)
    //            {
    //                //var newTile = Instantiate(gameTilePrefab, new Vector3(x, y), Quaternion.identity);
    //                newTile.name = $"Tile {x} {y}";
    //                var gameTile = newTile.GetComponent<GameTile>();
    //                gameTile.section = GetSection(x, y);
    //                gameTile.column = x;
    //                gameTile.row = y;
    //                tileSections[gameTile.section].Add(gameTile);
    //                tileCols[x].Add(gameTile);
    //                tileRows[y].Add(gameTile);
    //                gameTile.value = 0;
    //                //gameTile.GetComponentInChildren<TMP_Text>().SetText(gameTile.value.ToString());
    //            }
    //        }

    //        Camera.main.transform.position = new Vector3((float)width / 2 - 0.5f, (float)width / 4 - 0.5f, -10);
    //    }

    //    private Section GetSection(int x, int y)
    //    {
    //        if(x < 3)
    //        {
    //            if (y < 3)
    //            {
    //                return Section.I;
    //            }
    //            else if (y < 6)
    //            {
    //                return Section.IV;
    //            }
    //            else
    //            {
    //                return Section.VII;
    //            }
    //        }
    //        else if(x < 6)
    //        {
    //            if (y < 3)
    //            {
    //                return Section.II;

    //            }
    //            else if (y < 6)
    //            {

    //                return Section.V;
    //            }
    //            else
    //            {
    //                return Section.VIII;
    //            }
    //        }
    //        else
    //        {
    //            if (y < 3)
    //            {
    //                return Section.III;

    //            }
    //            else if (y < 6)
    //            {
    //                return Section.VI;

    //            }
    //            else
    //            {
    //                return Section.IX;
    //            }
    //        }
    //        return Section.None;
    //    }

    //    private void FillSections(Section[] sections)
    //    {
    //        foreach(var section in sections)
    //        {
    //            List<int> availableNums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    //            foreach (var tile in tileSections[section])
    //            {
    //                var value = availableNums[Random.Range(0, availableNums.Count)];
    //                tile.value = value;
    //                availableNums.Remove(value);
    //                //tile.GetComponentInChildren<TMP_Text>().SetText(tile.value.ToString());
    //            }
    //        }
    //    }

    //    private IEnumerator RowColumnSort()
    //    {
    //        for (int i = 0; i < 9; i++)
    //        {
    //            yield return StartCoroutine(SortRow(i));
    //            yield return StartCoroutine(SortCol(i));
    //        }
    //    }

    //    private void RowColSort()
    //    {
    //        for (int i = 0; i < 9; i++)
    //        {
    //            SortRow(i);
    //            SortCol(i);
    //        }
    //    }


    //    private IEnumerator SortRow(int row)
    //    {
    //        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    //        yield return StartCoroutine(BoxSwap(tileRows[row], true, row * 10, color, row));
    //        sortedRows.Add(row);
    //    }

    //    private IEnumerator SortCol(int col)
    //    {
    //        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    //        yield return StartCoroutine(BoxSwap(tileCols[col], false, col * 10, color, col));
    //        sortedCols.Add(col);
    //    }

    //    private IEnumerator BoxSwap(List<GameTile> tiles, bool row, float color, Color col, int iteration)
    //    {
    //        List<int> alreadySeen = new List<int>();
    //        color += 100;
    //        foreach(var tile in tiles)
    //        {
    //            if (tile.sorted)
    //            {
    //                if (!alreadySeen.Contains(tile.value))
    //                {
    //                    alreadySeen.Add(tile.value);
    //                    continue;
    //                }
    //            }
    //            if (!alreadySeen.Contains(tile.value))
    //            {
    //                alreadySeen.Add(tile.value);
    //                tile.sprite.color = new Color(0f, color / 255, 0f);
    //                //tile.sprite.color = col;
    //            }
    //            //Duplicate number
    //            else
    //            {
    //                //Try 1: Check section for number that works (different row and different column)
    //                //If inverse has already been checked -> only swap within (if searching row and column has been checked -> swap within column only)
    //                List<int> sorted = row ? sortedCols : sortedRows;
    //                int current = row ? tile.column : tile.row;
    //                Section sect = tile.section;
    //                List<GameTile> validTiles = new List<GameTile>();
    //                bool foundMatch = false;
    //                if (sorted.Contains(current))
    //                {
    //                    foreach (var n in tileSections[sect])
    //                    {
    //                        if (n == tile) continue;
    //                        bool adjacent = row ? n.column == tile.column : n.row == tile.row;
    //                        if (!adjacent) continue;
    //                        validTiles.Add(n);
    //                    }
    //                }
    //                else
    //                {
    //                    //Otherwise check entire section
    //                    foreach (var n in tileSections[sect])
    //                    {
    //                        if (n == tile) continue;
    //                        if (n.row == tile.row || n.column == tile.column) continue;
    //                        if (n.sorted) continue;
    //                        validTiles.Add(n);
    //                    }

    //                }
    //                if(validTiles.Count > 0)
    //                {
    //                    foreach (var n in validTiles)
    //                    {
    //                        if (alreadySeen.Contains(n.value))
    //                            continue;
    //                        n.sprite.color = new Color(color / 255, color / 255, 0);
    //                        foundMatch = true;
    //                        var temp = tile.value;
    //                        tile.value = n.value;
    //                        n.value = temp;
    //                        //tile.GetComponentInChildren<TMP_Text>().SetText(tile.value.ToString());
    //                        //n.GetComponentInChildren<TMP_Text>().SetText(n.value.ToString());
    //                        alreadySeen.Add(tile.value);
    //                        break;
    //                    }
    //                    tile.sprite.color = foundMatch ? new Color(0, color / 255, color / 255) : new(color / 255, 0, 0);
    //                    //tile.sprite.color = col;
    //                }
    //                if(!foundMatch)
    //                {
    //                    //Try 2: Check previous duplicate
    //                    GameTile prevTile = new GameTile();
    //                    for(int i = 0; i < tiles.Count; i++)
    //                    {
    //                        if (tiles[i] == tile) continue;
    //                        if (tiles[i].value == tile.value)
    //                        {
    //                            prevTile = tiles[i];
    //                            break;
    //                        }
    //                    }
    //                    sect = prevTile.section;
    //                    foreach (var n in tileSections[sect])
    //                    {
    //                        if (n == prevTile) continue;
    //                        bool adjacent = row ? n.column == prevTile.column : n.row == prevTile.row;
    //                        if (!adjacent) continue;
    //                        validTiles.Add(n);
    //                    }

    //                    foreach (var n in validTiles)
    //                    {
    //                        if (alreadySeen.Contains(n.value))
    //                            continue;
    //                        n.sprite.color = new Color(color / 255, color / 255, 0);
    //                        foundMatch = true;
    //                        var temp = prevTile.value;
    //                        prevTile.value = n.value;
    //                        n.value = temp;
    //                        //prevTile.GetComponentInChildren<TMP_Text>(true)?.SetText(prevTile.value.ToString());
    //                        //n.GetComponentInChildren<TMP_Text>()?.SetText(n.value.ToString());
    //                        alreadySeen.Add(prevTile.value);
    //                        break;
    //                    }
    //                    prevTile.sprite.color = foundMatch ? new Color(0, color / 255, 0) : new(color / 255, 0, 0);
    //                    //tile.sprite.color = col;
    //                    if (!foundMatch)
    //                    {
    //                        bool valid = false;
    //                        List<GameTile> otherTiles = row ? tileCols[prevTile.column] : tileRows[prevTile.row];
    //                        int segmentToCheck = 0;
    //                        int j = 0;
    //                        while (j < 15)
    //                        {
    //                            otherTiles = row ? tileCols[prevTile.column] : tileRows[prevTile.row];
    //                            segmentToCheck = row ? prevTile.row : prevTile.column;
    //                            GameTile swapTile = segmentToCheck < otherTiles.Count-1 ? otherTiles[segmentToCheck + 1] : tiles[segmentToCheck - 1];
    //                            var temp = prevTile.value;
    //                            prevTile.value = swapTile.value;
    //                            swapTile.value = temp;
    //                            //prevTile.GetComponentInChildren<TMP_Text>(true)?.SetText(prevTile.value.ToString());
    //                            //swapTile.GetComponentInChildren<TMP_Text>(true)?.SetText(swapTile.value.ToString());
    //                            prevTile.sprite.color = Color.cyan;
    //                            swapTile.sprite.color = Color.cyan;
    //                            for (int i = 0; i < tiles.Count; i++)
    //                            {
    //                                if (tiles[i] == prevTile) continue;
    //                                if (tiles[i].value == prevTile.value)
    //                                {
    //                                    prevTile = tiles[i];
    //                                    break;
    //                                }

    //                            }
    //                            if(prevTile != null) prevTile.sprite.color = foundMatch ? new Color(0, color / 255, 0) : new(color / 255, 0, 0);
    //                            valid = row ? ValidRow(iteration) : ValidCol(iteration);
    //                            if (valid) break;
    //                            j++;
    //                        }

    //                    }
    //                }

    //            }
    //            tile.sorted = true;
    //            yield return new WaitForSeconds(0.1f);
    //        }
    //    }

    //    private void Swap(GameTile tileA, GameTile tileB)
    //    {
    //        var temp = tileA.value;
    //        tileA.value = tileB.value;
    //        tileB.value = temp;
    //        tileA.GetComponentInChildren<TMP_Text>().SetText(tileA.value.ToString());
    //        tileB.GetComponentInChildren<TMP_Text>().SetText(tileB.value.ToString());
    //    }

    //    private bool ValidPuzzle(Section[] sections)
    //    {
    //        //Check Sections
    //        foreach (var sect in sections)
    //        {
    //            List<int> alreadyContains = new List<int>();
    //            foreach (var num in tileSections[sect])
    //            {
    //                if (alreadyContains.Contains(num.value))
    //                    return false;
    //                else
    //                    alreadyContains.Add(num.value);
    //            }
    //        }
    //        //Check rows
    //        for (int i = 0; i < 9; i++)
    //        {
    //            if (!ValidRow(i)) return false;
    //        }
    //        //Check Columns
    //        for (int i = 0; i < 9; i++)
    //        {
    //            if (!ValidCol(i)) return false;
    //        }
    //        return true;
    //    }

    //    private bool ValidRow(int row)
    //    {
    //        List<int> alreadyContains = new List<int>();
    //        foreach (var num in tileRows[row])
    //        {
    //            if (alreadyContains.Contains(num.value))
    //                return false;
    //            else
    //                alreadyContains.Add(num.value);
    //        }
    //        return true;
    //    }

    //    private bool ValidCol(int col)
    //    {
    //        List<int> alreadyContains = new List<int>();
    //        foreach (var num in tileCols[col])
    //        {
    //            if (alreadyContains.Contains(num.value))
    //                return false;
    //            else
    //                alreadyContains.Add(num.value);
    //        }
    //        return true;
    //    }
}

