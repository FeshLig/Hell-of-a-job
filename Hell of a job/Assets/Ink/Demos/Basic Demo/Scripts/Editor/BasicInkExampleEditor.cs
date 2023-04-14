﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Ink.UnityIntegration;
using Ink.Runtime;

[CustomEditor(typeof(BasicInkExample))]
[InitializeOnLoad]
public class BasicInkExampleEditor : Editor {
<<<<<<< Updated upstream

=======
    static bool storyExpanded;
>>>>>>> Stashed changes
    static BasicInkExampleEditor () {
        BasicInkExample.OnCreateStory += OnCreateStory;
    }

    static void OnCreateStory (Story story) {
        // If you'd like NOT to automatically show the window and attach (your teammates may appreciate it!) then replace "true" with "false" here. 
        InkPlayerWindow window = InkPlayerWindow.GetWindow(true);
        if(window != null) InkPlayerWindow.Attach(story);
    }
	public override void OnInspectorGUI () {
		Repaint();
		base.OnInspectorGUI ();
		var realTarget = target as BasicInkExample;
		var story = realTarget.story;
<<<<<<< Updated upstream
		InkPlayerWindow.DrawStoryPropertyField(story, new GUIContent("Story"));
=======
		InkPlayerWindow.DrawStoryPropertyField(story, ref storyExpanded, new GUIContent("Story"));
>>>>>>> Stashed changes
	}
}