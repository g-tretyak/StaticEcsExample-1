using System;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class PreBuildProcess : IPreprocessBuildWithReport
{
    private const string Prefix = "v.01.";
    private const string Format = "dd.MM.yyyy.HH:mm";
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        var dt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
        var buildDate = Prefix + dt.ToString(Format);
        PlayerSettings.bundleVersion = buildDate;
    }
}