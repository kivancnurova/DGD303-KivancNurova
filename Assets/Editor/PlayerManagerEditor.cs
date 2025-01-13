using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerManager))]
public class PlayerManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerManager playerManager = (PlayerManager)target;

        if (GUILayout.Button("Level Up"))
        {
            playerManager.LevelUp();
        }
    }
}
