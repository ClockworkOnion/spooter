using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    static ScoreBoard instance;
    private TextMeshProUGUI scoreBoard, tagInput;
    private uint currentScore = 0;
    private readonly SortedSet<Score> scores = new SortedSet<Score>();
    private bool notAdded = true, restartable = true;

    private uint shownScoreCount = 5, tagLength = 3;
    
    public bool Restartable
    {
        get => restartable;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        tagInput = GameObject.Find("ScoreBoard/TagInput/Text Area/Text").GetComponent<TextMeshProUGUI>();
        scoreBoard = GameObject.Find("ScoreBoard/ScoreBoard").GetComponent<TextMeshProUGUI>();
        LoadScore();
        RefreshScoreBoard();
        gameObject.SetActive(false);
    }

    public void OnGameFinished(uint score)
    {
        currentScore = score;
        notAdded = true;
        gameObject.SetActive(true);
    }

    public void AddScore()
    {
        if (notAdded && tagInput.GetParsedText().Length == tagLength + 1)
        {
            notAdded = false;
            if (scores.Count < shownScoreCount)
            {
                scores.Add(new Score(tagInput.GetParsedText(), currentScore));
            } else if(scores.Min.score < currentScore)
            {
                scores.Remove(scores.Min);
                scores.Add(new Score(tagInput.GetParsedText(), currentScore));
            }
            RefreshScoreBoard();
            SaveScore();
        }
    }

    void RefreshScoreBoard()
    {
        string text = "Top Scores\n";
        foreach (var item in scores.Reverse())
        {
            text += item + "\n";
        }
        scoreBoard.SetText(text);
    }

    void SaveScore()
    {
        BinaryWriter file = new BinaryWriter(File.Open(Application.persistentDataPath + "scores", FileMode.OpenOrCreate));
        file.Write(scores.Count);
        foreach (var item in scores)
        {
            file.Write(item.tag);
            file.Write(item.score);
        }
        file.Close();
    }

    void LoadScore()
    {
        BinaryReader file = new BinaryReader(File.Open(Application.persistentDataPath + "scores", FileMode.OpenOrCreate));
        int count = file.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            string tag = file.ReadString();
            uint score = file.ReadUInt32();
            scores.Add(new Score(tag, score));
        }
        file.Close();
    }

    public void OnGameRestart()
    {
        notAdded = true;
        gameObject.SetActive(false);
    }

    public void OnSelect(string ignored)
    {
        restartable = !restartable;
    }

    public static ScoreBoard GetScoreBoard()
    {
        return instance;
    }
}


[Serializable]
class Score : IComparable<Score>
{
    public readonly string tag;
    public readonly uint score;

    public Score(string tag, uint score)
    {
        this.tag = tag;
        this.score = score;
    }

    public int CompareTo(Score other)
    {
        return Comparer.Default.Compare(score, other.score) == 0? Comparer.Default.Compare(tag, other.tag) : Comparer.Default.Compare(score, other.score);
    }

    public override string ToString()
    {
        return $"{tag} : {score}";
    }
}