using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LeaderBoard : EditorWindow
{
	private const string StyleSheetPath = "Assets/leader-board.uss";
	private static readonly string[] Scores;

	static LeaderBoard()
	{
		Scores = new[] {
			"Joffrey",
			"Sansa",
			"Arya",
			"Bran",
			"Jon",
			"Rickon",
			"Joffrey",
			"Sansa",
			"Arya",
			"Bran",
			"Jon",
			"Rickon",
			"Joffrey",
			"Sansa",
			"Arya",
			"Bran",
			"Jon",
			"Rickon",
			"Joffrey",
			"Sansa",
			"Arya",
			"Bran",
			"Jon",
			"Rickon",
			"Joffrey",
			"Sansa",
			"Arya",
			"Bran",
			"Jon",
			"Rickon",
			"Joffrey",
			"Sansa",
			"Arya",
			"Bran",
			"Jon",
			"Rickon"
		};
	}

	[MenuItem("Demo/Leaderboard")]
	public static void ShowLeaderBoard()
	{
		var window = GetWindow<LeaderBoard>();
		window.minSize = new Vector2(325, 500);
		window.maxSize = new Vector2(1000, 500);
		window.titleContent = new GUIContent("Leader Board");
	}
	private void OnEnable()
	{
		rootVisualElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath));

		var container = new ListView();
		container.AddToClassList("slab-container");

		for (var i = 0; i < Scores.Length; i++)
		{
			var score = Scores[i];
			var scoreElement = new Score(i, score);
			container.Add(scoreElement);
		}
		
		rootVisualElement.Add(container);
	}
}

public class Score : VisualElement
{
	public Score(int index, string score)
	{
		var slab = new Slab();

		var leftPartition = new VisualElement { style = { flexGrow = 0.4f } };
		var rightPartition = new VisualElement { style = { flexGrow = 1f } };
		leftPartition.AddToClassList("partition");
		rightPartition.AddToClassList("partition");
		rightPartition.AddToClassList("partition-underline");
		
		var ranking = new Label { text = (index + 1).ToString() };
		var avatar = new Image();
		leftPartition.Add(ranking);
		leftPartition.Add(avatar);
		
		var name = new Label { text = score, style = { flexGrow = 1 }};
		var kills = new Label {text = 7.ToString()};
		var time = new Label { text = DateTime.UtcNow.Hour + ":" + DateTime.UtcNow.Minute };

		rightPartition.Add(name);
		rightPartition.Add(kills);
		rightPartition.Add(time);
		
		slab.Add(leftPartition);
		slab.Add(rightPartition);
		Add(slab);
	}
}

public class Slab : VisualElement
{
	public Slab()
	{
		AddToClassList("slab");
	}
}
