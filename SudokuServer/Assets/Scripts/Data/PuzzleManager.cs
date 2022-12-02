using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    private void Awake()
    {
        if (PuzzleManager.Instance != null) Destroy(this.gameObject);
        else Instance = this;
    }

    [Header("Puzzle generation variables")]
    public int width = 9, height = 9;
    public int iterations = 1000;
    public List<CompletedBoard> completedBoards;
    public Dictionary<Section, List<GameTile>> tileSections = new Dictionary<Section, List<GameTile>>();
    public Dictionary<int, List<GameTile>> tileCols = new Dictionary<int, List<GameTile>>();
    public Dictionary<int, List<GameTile>> tileRows = new Dictionary<int, List<GameTile>>();
    private List<int> sortedRows = new List<int>();
    private List<int> sortedCols = new List<int>();
    private string puzzlePath = "B:/ZB-Projects/UnitySudoku/puzzles.txt";
    public List<string> puzzles;

    private readonly Section[] sections = new Section[9] { Section.I, Section.II, Section.III, Section.IV, Section.V, Section.VI, Section.VII, Section.VIII, Section.IX };
    private List<int> validPuzzle = new List<int>();

    [Header("Placing tiles and buttons")]
    public Transform[] tileParents;
    public GameObject tileButton;
    public float percentOfTilesVisible = 0.3f;
    private readonly Color textBlue = new Color(0.43f, 0.66f, 0.76f);

    [ContextMenu("Generate puzzles")]
    private async Task GeneratePuzzles()
    {
        Clear();
        puzzles = new List<string>();
        int correct = 0;
        for (int i = 0; i < iterations; i++)
        {
            validPuzzle.Clear();
            Init();
            GenerateGrid();
            FillSections(sections);
            for (int j = 0; j < 10; j++)
            {
                await RowColumnSort();
                if (ValidPuzzle(sections)) break;
            }
            if (ValidPuzzle(sections))
            {
                CompletedBoard puzzle = new CompletedBoard();
                string puzzleString = "";
                for (int k = 0; k < 9; k++)
                {
                    foreach (var tile in tileCols[k])
                    {
                        puzzle.completedBoard.Add(tile);
                        string tileData = $"{tile.value}{(int)tile.section}";
                        puzzleString += tileData;
                    }
                }
                completedBoards.Add(puzzle);
                puzzles.Add(puzzleString);
                correct++;
            }
        }
        print($"{correct}/{iterations}");
        //List<string> lines = new List<string>();
        //if (File.Exists(puzzlePath))
        //{
        //    Append(puzzles);
        //}
        //else
        //{
        //    Write(puzzles);
        //}

        try
        {
            await DBManager.Instance.SavePuzzles(puzzles);

        } catch (System.Exception e)
        {
            Debug.LogError($"Cannot add to DB (Exception {e})");
        }
    }

    private void Write(List<string> contents)
    {
        StreamWriter file = new StreamWriter(puzzlePath);
        foreach (var line in contents)
            file.WriteLine(line);
        file.Close();
    }

    private void Append(List<string> contents)
    {
        StreamWriter file = new StreamWriter(puzzlePath, true);
        foreach (var line in contents)
            file.WriteLine(line);
        file.Close();
    }


    [ContextMenu("Clear puzzles")]
    private void Clear()
    {
        completedBoards.Clear();
        puzzles.Clear();
    }

    //public async Task StartGame()
    //{
    //    //Could be a database or just text-website hosting sudoku puzzles
    //    var puzzles = File.ReadAllLines(puzzlePath).ToList();
    //    string puzzle = puzzles[Random.Range(0, puzzles.Count)];
    //    for (int i = 0; i < puzzle.Length; i += 2)
    //    {
    //        int value = int.Parse(puzzle[i].ToString());
    //        int sectionNum = int.Parse(puzzle[i + 1].ToString());
    //        var button = Instantiate(tileButton, tileParents[sectionNum]);
    //        var text = button.GetComponentInChildren<TMP_Text>(true);
    //        var section = tileParents[sectionNum].GetComponent<SectionWin>();
    //        text.SetText(value.ToString());
    //        bool visible = percentOfTilesVisible > Random.Range(0f, 1f);
    //        var t = button.GetComponent<Tile>();
    //        t.value = value;
    //        t.DefaultVisible(visible);
    //        text.color = visible ? Color.black : textBlue;
    //        GameManager.Instance.tiles.Add(t);
    //        section.tilesInSection.Add(t);
    //    }
    //    print(Application.persistentDataPath);
    //}

    private void Init()
    {
        sortedRows.Clear();
        sortedCols.Clear();
        tileSections.Clear();
        tileRows.Clear();
        tileCols.Clear();

        //Sections Init
        tileSections.Add(Section.I, new List<GameTile>());
        tileSections.Add(Section.II, new List<GameTile>());
        tileSections.Add(Section.III, new List<GameTile>());
        tileSections.Add(Section.IV, new List<GameTile>());
        tileSections.Add(Section.V, new List<GameTile>());
        tileSections.Add(Section.VI, new List<GameTile>());
        tileSections.Add(Section.VII, new List<GameTile>());
        tileSections.Add(Section.VIII, new List<GameTile>());
        tileSections.Add(Section.IX, new List<GameTile>());

        //Cols/Rows init
        for (int i = 0; i < 9; i++)
        {
            tileCols.Add(i, new List<GameTile>());
            tileRows.Add(i, new List<GameTile>());
        }
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var gameTile = new GameTile();
                gameTile.section = GetSection(x, y);
                gameTile.column = x;
                gameTile.row = y;
                tileSections[gameTile.section].Add(gameTile);
                tileCols[x].Add(gameTile);
                tileRows[y].Add(gameTile);
                gameTile.value = 0;
            }
        }
    }

    private Section GetSection(int x, int y)
    {
        if (x < 3)
        {
            if (y < 3)
            {
                return Section.I;
            }
            else if (y < 6)
            {
                return Section.IV;
            }
            else
            {
                return Section.VII;
            }
        }
        else if (x < 6)
        {
            if (y < 3)
            {
                return Section.II;

            }
            else if (y < 6)
            {

                return Section.V;
            }
            else
            {
                return Section.VIII;
            }
        }
        else
        {
            if (y < 3)
            {
                return Section.III;

            }
            else if (y < 6)
            {
                return Section.VI;

            }
            else
            {
                return Section.IX;
            }
        }
    }

    private void FillSections(Section[] sections)
    {
        foreach (var section in sections)
        {
            List<int> availableNums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (var tile in tileSections[section])
            {
                var value = availableNums[Random.Range(0, availableNums.Count)];
                tile.value = value;
                availableNums.Remove(value);
            }
        }
    }

    private async Task RowColumnSort()
    {
        for (int i = 0; i < 9; i++)
        {
            await SortRow(i);
            await SortCol(i);
        }
    }

    private async Task SortRow(int row)
    {
        BoxSwap(tileRows[row], true, row * 10, row);
        sortedRows.Add(row);
    }

    private async Task SortCol(int col)
    {
        BoxSwap(tileCols[col], false, col * 10, col);
        sortedCols.Add(col);
    }

    private void BoxSwap(List<GameTile> tiles, bool row, float color, int iteration)
    {
        List<int> alreadySeen = new List<int>();
        foreach (var tile in tiles)
        {
            if (tile.sorted)
            {

                if (!alreadySeen.Contains(tile.value))
                {
                    alreadySeen.Add(tile.value);
                    continue;
                }
            }
            if (!alreadySeen.Contains(tile.value))
            {

                alreadySeen.Add(tile.value);
            }
            //Duplicate number
            else
            {
                //Try 1: Check section for number that works (different row and different column)
                //If inverse has already been checked -> only swap within (if searching row and column has been checked -> swap within column only)
                List<int> sorted = row ? sortedCols : sortedRows;
                int current = row ? tile.column : tile.row;
                Section sect = tile.section;
                List<GameTile> validTiles = new List<GameTile>();
                bool foundMatch = false;
                if (sorted.Contains(current))
                {
                    foreach (var n in tileSections[sect])
                    {
                        if (n == tile) continue;
                        bool adjacent = row ? n.column == tile.column : n.row == tile.row;
                        if (!adjacent) continue;
                        validTiles.Add(n);
                    }
                }
                else
                {
                    //Otherwise check entire section
                    foreach (var n in tileSections[sect])
                    {
                        if (n == tile) continue;
                        if (n.row == tile.row || n.column == tile.column) continue;
                        if (n.sorted) continue;
                        validTiles.Add(n);
                    }

                }
                if (validTiles.Count > 0)
                {
                    foreach (var n in validTiles)
                    {
                        if (alreadySeen.Contains(n.value))
                            continue;
                        foundMatch = true;
                        var temp = tile.value;
                        tile.value = n.value;
                        n.value = temp;
                        alreadySeen.Add(tile.value);
                        break;
                    }
                }
                if (!foundMatch)
                {
                    GameTile prevTile = new GameTile();
                    for (int i = 0; i < tiles.Count; i++)
                    {
                        if (tiles[i] == tile) continue;
                        if (tiles[i].value == tile.value)
                        {
                            prevTile = tiles[i];
                            break;
                        }
                    }
                    sect = prevTile.section;
                    foreach (var n in tileSections[sect])
                    {
                        if (n == prevTile) continue;
                        bool adjacent = row ? n.column == prevTile.column : n.row == prevTile.row;
                        if (!adjacent) continue;
                        validTiles.Add(n);
                    }

                    foreach (var n in validTiles)
                    {
                        if (alreadySeen.Contains(n.value))
                            continue;
                        foundMatch = true;
                        var temp = prevTile.value;
                        prevTile.value = n.value;
                        n.value = temp;
                        alreadySeen.Add(prevTile.value);
                        break;
                    }
                    if (!foundMatch)
                    {
                        bool valid = false;
                        List<GameTile> otherTiles = new List<GameTile>();
                        int segmentToCheck = 0;
                        for (int j = 0; j < 15; j++)
                        {
                            otherTiles = row ? tileCols[prevTile.column] : tileRows[prevTile.row];
                            segmentToCheck = row ? prevTile.row : prevTile.column;
                            GameTile swapTile = segmentToCheck < otherTiles.Count - 1 ? otherTiles[segmentToCheck + 1] : tiles[segmentToCheck - 1];
                            var temp = prevTile.value;
                            prevTile.value = swapTile.value;
                            swapTile.value = temp;
                            for (int i = 0; i < tiles.Count; i++)
                            {
                                if (tiles[i] == prevTile) continue;
                                if (tiles[i].value == prevTile.value)
                                {
                                    prevTile = tiles[i];
                                    break;
                                }

                            }
                            valid = row ? ValidRow(iteration) : ValidCol(iteration);
                            if (valid) break;
                        }

                    }
                }

            }
            tile.sorted = true;
        }
    }

    private bool ValidPuzzle(Section[] sections)
    {
        //Check Sections
        foreach (var sect in sections)
        {
            if (!ValidSection(sect)) return false;
        }
        //Check rows
        for (int i = 0; i < 9; i++)
        {
            if (!ValidRow(i)) return false;
        }
        //Check Columns
        for (int i = 0; i < 9; i++)
        {
            if (!ValidCol(i)) return false;
        }
        return true;
    }

    private bool ValidSection(Section section)
    {
        List<int> alreadyContains = new List<int>();
        foreach (var num in tileSections[section])
        {
            if (num.value == 0) return false;
            if (alreadyContains.Contains(num.value))
                return false;
            else
                alreadyContains.Add(num.value);
        }
        return true;
    }

    private bool ValidRow(int row)
    {
        List<int> alreadyContains = new List<int>();
        foreach (var num in tileRows[row])
        {
            if (num.value == 0) return false;
            if (alreadyContains.Contains(num.value))
                return false;
            else
                alreadyContains.Add(num.value);
        }
        return true;
    }

    private bool ValidCol(int col)
    {
        List<int> alreadyContains = new List<int>();
        foreach (var num in tileCols[col])
        {
            if (num.value == 0) return false;
            if (alreadyContains.Contains(num.value))
                return false;
            else
                alreadyContains.Add(num.value);
        }
        return true;
    }
}
