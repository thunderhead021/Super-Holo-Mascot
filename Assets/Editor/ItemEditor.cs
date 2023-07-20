using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item))]
public class ItemEditor : Editor
{
	override public void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		serializedObject.Update();
		Item item = target as Item;
		switch (item.type)
		{
			case BuffType.StatBuff:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("atkBuff"));
				EditorGUILayout.PropertyField(serializedObject.FindProperty("hpBuff"));
				break;
			case BuffType.AddEffect:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("effect")); 
				EditorGUILayout.PropertyField(serializedObject.FindProperty("effectType")); 
				EditorGUILayout.PropertyField(serializedObject.FindProperty("passiveType"));
				break;
			case BuffType.TriggerImmidiately:
				EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerType")); 
				EditorGUILayout.PropertyField(serializedObject.FindProperty("triggerSkill"));
				break;
		}
		serializedObject.ApplyModifiedProperties();
	}
}
