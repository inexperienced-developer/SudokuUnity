using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InexperiencedDeveloper.Core;
using Firebase.Firestore;
using System.Threading.Tasks;
using System.Linq;

public class DBManager : Singleton<DBManager>
{
    private Firebase.FirebaseApp m_App;
    private FirebaseFirestore m_DB;
    protected override void Awake()
    {
        base.Awake();
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                m_App = Firebase.FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
                m_DB = FirebaseFirestore.DefaultInstance;
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    // --------------- PUZZLE FORMAT --------------- //
    // -------- [[1,2,3],[4,5,6],[7,8,9], ---------- //
    // -------- [1,2,3],[4,5,6],[7,8,9], ----------- //
    // -------- [1,2,3],[4,5,6],[7,8,9]] ----------- //
    // --------------------------------------------- //
    // ---  List length 9 of Byte array length 3 --- //
    // --------------------------------------------- //

    public async Task<List<int[]>> GetPuzzle()
    {
        List<byte[]> puzzle = new List<byte[]>();
        DocumentReference puzzleRef = m_DB.Collection("general").Document("puzzles");
        DocumentSnapshot puzzleSnap = await puzzleRef.GetSnapshotAsync();
        if(puzzleSnap != null)
        {
            Dictionary<string, object> puzzleDict = puzzleSnap.ToDictionary();
            if(puzzleDict != null)
            {
                var keys = puzzleDict.Keys.ToArray();
                if(puzzleDict.TryGetValue(keys[Random.Range(0, keys.Length)], out var randomPuzzle))
                {
                    
                }
                else
                {
                    Debug.LogError("Unable to find a random puzzle");
                }
            }
        }
        return null;
    }

    public async Task SavePuzzles(List<string> puzzles)
    {
        Debug.Log("Saving Puzzle");
        try
        {
            Dictionary<string, object> puzzDict = new Dictionary<string, object>();
            foreach (var puzz in puzzles)
            {
                puzzDict.Add(RandomPuzzleKey(), puzz);
            }
            DocumentReference puzzleRef = m_DB.Collection("general").Document("puzzles");
            await puzzleRef.SetAsync(puzzDict, SetOptions.MergeAll);
        } catch (System.Exception e)
        {
            Debug.LogError($"Can't save puzzles (Exception: {e})");
        }

    }

    private string RandomPuzzleKey(int length = 20)
    {
        string jumble = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        string key = "";
        for(int i = 0; i < length; i++)
        {
            key += jumble[Random.Range(0, jumble.Length)];
        }

        return key;
    }
}
