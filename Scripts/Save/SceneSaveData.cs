using System;
using System.Collections.Generic;

[Serializable]
public class SceneSaveData : Save
{
    public int SceneIndex = 0;
    public Dictionary<string, string> ObjectStates = new Dictionary<string, string>();

    public SceneSaveData(int scene) : this(scene, new Dictionary<string, string>()) { }
    public SceneSaveData(int scene, Dictionary<string, string> data)
    {
        ObjectStates = data;
        SceneIndex = scene;
    }

}
