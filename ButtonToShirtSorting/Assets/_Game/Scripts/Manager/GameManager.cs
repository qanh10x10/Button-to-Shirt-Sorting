using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public List<LevelModelSO> levelModels = new List<LevelModelSO>();

    private void Start()
    {
        levelModels = Resources.LoadAll<LevelModelSO>("Levels").ToList();
    }

}
