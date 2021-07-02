using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using static UnityEditor.BuildPipeline;

namespace Editor.Build
{
    public static class Builder
    {
        [MenuItem("Build/Android")]
        public static void BuildAndroid()
        {
            BuildReport report = BuildPlayer(new BuildPlayerOptions()
            {
                target = BuildTarget.Android,
                scenes = new[] {"Assets/Scenes/SampleScene.unity"},
                locationPathName = "../../artifacts/game.apk",
                options = BuildOptions.None,
            });

            if (report.summary.result != BuildResult.Succeeded)
                throw new Exception("Failed build Android. See log for details.");
        }
    }
}