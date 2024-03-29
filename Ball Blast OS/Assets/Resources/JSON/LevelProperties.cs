﻿using System;
using UnityEngine;

public class LevelProperties
{
    public static void Process(ref LevelProperties.Properties gameProperties, string path)
    {
        gameProperties = JsonUtility.FromJson<Properties>(Resources.Load<TextAsset>(path).text);
    }

    [Serializable]
    public class Properties
    {
        public float gravity;
        public int bullet_count_increase;
        public int bullet_damage_increase;
        public levels[] levels;
    }

    [Serializable]
    public class levels
    {
        public balls[] balls;
    }


    [Serializable]
    public class balls
    {
        public int hp;
        public int[] splits;
        public int delay;

        public balls(int hp, int[] splits, int delay)
        {
            this.hp = hp;
            this.splits = splits;
            this.delay = delay;
        }
    }

    [Serializable]
    public class splits
    {
        public int[] splitAmounts;
    }

}
