using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;

public class AppHandler : MonoBehaviour
{
    public static AppHandler instance;
    public PythonData pythonData;
    public BallData ballData;
    Process pythonProcess;
    [SerializeField]
    GameObject[] allObjs;
    private void Awake()
    {
        Application.runInBackground = true;
        instance = this;
        string BallDatastr = File.ReadAllText(Application.dataPath.Substring(0, Application.dataPath.LastIndexOf(Path.AltDirectorySeparatorChar) + 1) + "balldata.json");
        ballData = JsonUtility.FromJson<BallData>(BallDatastr);
        allObjs[0].transform.position = new Vector3(allObjs[0].transform.position.x, ballData.TopPoint, allObjs[0].transform.position.z);
        allObjs[1].transform.position = new Vector3(allObjs[1].transform.position.x, ballData.CalculatePoint, allObjs[1].transform.position.z);
        allObjs[2].transform.position = new Vector3(allObjs[2].transform.position.x, ballData.LostPoint, allObjs[2].transform.position.z);
        allObjs[3].transform.position = new Vector3(allObjs[3].transform.position.x, ballData.LowestPoint, allObjs[3].transform.position.z);
    }
    public void SelectedColor(int _selected)
    {
        print(Application.dataPath);
        string pythonDatastr=File.ReadAllText(Application.dataPath.Substring(0, Application.dataPath.LastIndexOf(Path.AltDirectorySeparatorChar)+1) + "data.json");
        pythonData = JsonUtility.FromJson<PythonData>(pythonDatastr);
        pythonData.select = _selected;
        File.WriteAllText((Application.dataPath.Substring(0, Application.dataPath.LastIndexOf(Path.AltDirectorySeparatorChar)+1) + "data.json"), JsonUtility.ToJson(pythonData));
        StartProcess();
    }

    void StartProcess()
    {
        try
        {
            pythonProcess = new Process();
            pythonProcess.StartInfo.CreateNoWindow = true;
            pythonProcess.StartInfo.FileName = (Application.dataPath.Substring(0, Application.dataPath.LastIndexOf(Path.AltDirectorySeparatorChar) + 1) + "main.exe");
            pythonProcess.Start();
            //print(ExitCode);
        }
        catch (System.Exception e)
        {
            print(e);
        }
    }
    private void OnDestroy()
    {
        if (pythonProcess != null)
        {
            pythonProcess.Kill();
        }
    }
}

[System.Serializable]
public class BallData
{
    public float LowestPoint;
    public float LostPoint;
    public float CalculatePoint;
    public float TopPoint;
    public float time;

}
[System.Serializable]
public class PythonData
{
    /*
     * {"select":1,
"values":[{
"hmin":20,
"smin":72,
"vmin":126,
"hmax":80,
"smax":255,
"vmax":255
},{
"hmin":20,
"smin":72,
"vmin":126,
"hmax":80,
"smax":255,
"vmax":255
}]
}
     */
    public int select;
    public int port;
    public ColorValue[] values;
}
[System.Serializable]
public class ColorValue
{
    public int hmin;
    public int smin;
    public int vmin;
    public int hmax;
    public int smax;
    public int vmax;
}
