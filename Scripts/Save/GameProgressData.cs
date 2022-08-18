using System;
using System.Collections.Generic;

[Serializable]
public class GameProgressData : Save
{
    public List<SceneSaveData> ScenesProgress = new List<SceneSaveData>();
    public PlayerSaveData PlayerProgress = new PlayerSaveData();
    public int LastScene = 0;


    public GameProgressData() : this(new PlayerSaveData(), new List<SceneSaveData>(), 0) { }
    public GameProgressData(PlayerSaveData playerData) : this(playerData, new List<SceneSaveData>(), 0){ }
    public GameProgressData(List<SceneSaveData> scenesData) : this(new PlayerSaveData(), scenesData, 0) { }
    public GameProgressData(PlayerSaveData playerData, List<SceneSaveData> scenesData) : this(playerData, scenesData, 0) { }
    public GameProgressData(PlayerSaveData playerData, List<SceneSaveData> scenesData, int scene)
    {
        PlayerProgress = playerData;
        ScenesProgress = scenesData;
        LastScene = scene;
    }
}
