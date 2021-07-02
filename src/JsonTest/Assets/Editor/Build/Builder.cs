using UnityEditor;

namespace Editor.Build
{
    public static class Builder
    {
        [MenuItem("Build/Build Android")]
        public static void MyBuild()
        {
            BuildPipeline.BuildPlayer(new BuildPlayerOptions()
            {
                target = BuildTarget.Android,
                scenes = new[] {"Assets/Scenes/SampleScene.unity"},
                locationPathName = "../../artifacts/game.apk",
                options = BuildOptions.None,
            });
        }
    }
}