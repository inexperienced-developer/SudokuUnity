using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SectionWin : MonoBehaviour
{
    public List<Tile> tilesInSection;
    public List<SectionWin> nextSection;

    public async Task WinRoutine()
    {
        for(int i = 0; i < 5; i++)
        {
            if (i == 3)
                foreach (var section in nextSection)
                    section.WinRoutine();
            ChangeColor(Color.green, i);
            await Task.Delay(100);
        }
        for(int i = 0; i < 5; i++)
        {
            ChangeColor(Color.white, i);
            await Task.Delay(100);
        }
    }

    private void ChangeColor(Color color, int i)
    {
        switch (i)
        {
            case 0:
                tilesInSection[0].SetColor(color);
                break;
            case 1:
                tilesInSection[1].SetColor(color);
                tilesInSection[3].SetColor(color);
                break;
            case 2:
                tilesInSection[2].SetColor(color);
                tilesInSection[4].SetColor(color);
                tilesInSection[6].SetColor(color);
                break;
            case 3:
                tilesInSection[5].SetColor(color);
                tilesInSection[7].SetColor(color);
                break;
            case 4:
                tilesInSection[8].SetColor(color);
                break;
        }
    }
}
