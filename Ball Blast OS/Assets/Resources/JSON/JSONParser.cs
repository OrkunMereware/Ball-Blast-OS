using System;
using UnityEngine;

public class JSONParser
{
    public static void Read(ref JSONParser.GameProperties gameProperties, string path)
    {
        gameProperties = JsonUtility.FromJson<GameProperties>(Resources.Load<TextAsset>(path).text);
    }

    [Serializable]
    public class GameProperties
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
