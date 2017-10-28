using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
public struct SimRecord
{
	public Record[] Steps;
}
[System.Serializable]
public struct Record {
	public int SelfIdentity;
	public int LastPlayerIdentity;
	public string CenterText;
	public List<int> LordCards;
	public List<int> Farmer1Card;
	public List<int> Farmer2Card;
}

public static class Data
{
	public static void Parse()
	{
		string cmd = System.Environment.CommandLine;
		string[] args = System.Environment.GetCommandLineArgs();
		string t="Player0 = {0x13,0x33,0x34,0x35,0x45,0x37,0x47,0x2a,0x3a,0x2b,0x3b,0x4b,0x3c,0x1d,0x4d,0x1e,0x2f}\nPlayer1 = {0x43,0x14,0x24,0x15,0x25,0x36,0x46,0x17,0x27,0x18,0x28,0x38,0x48,0x1a,0x4a,0x2d,0x4f}\nPlayer2 = {0x44,0x16,0x26,0x19,0x29,0x39,0x49,0x1b,0x1c,0x2c,0x3d,0x3e,0x4e,0x1f,0x3f,0x1,0x2}\nExtra ={23,2e,4c}";

		if (args.Length <= (1 + 1)) {
			if (args.Length>1)
			t = args [1];
			string[] split = t.Split ('#');
			for (int i = 0; i < split.Length; i++) {
				split[i]=split[i].Substring (split[i].IndexOf ("{"));
				Debug.Log(split[i]);
			}
			Show._Instance.lordCard =	StringToByteArray (split [0]);
			Show._Instance.farmer1Card =	StringToByteArray (split [1]);
			Show._Instance.farmer2Card =	StringToByteArray (split [2]);
			Show._Instance.extraCard =	StringToByteArray (split [3]);
		}


		if (args.Length == (1 + 3 + 1))
		{
			LordCard.AddRange(StringToByteArray(args[1]));
			Farmer1Card.AddRange(StringToByteArray(args[2]));
			Farmer2Card.AddRange(StringToByteArray(args[3]));
			ExtraCard.AddRange(StringToByteArray(args[4]));
		}      
		SimRecord x = new SimRecord();
		x.Steps = new Record[3];
		Record c0 = new Record();
		c0.LastPlayerIdentity = 1;
		c0.LordCards = new List<int>();
		c0.LordCards.Add(0x13);
		x.Steps[0] = c0;
		Debug.Log(JsonUtility.ToJson(x));
	}
	static byte[] StringToByteArray(string arg)
	{
		if (arg.StartsWith("[") || arg.StartsWith("{"))
		{
			arg = arg.Substring(1, arg.Length - 2);
		}
		var x = arg.Split(',');
		byte[] r = new byte[x.Length];
		for (int i = 0; i < x.Length; i++)
		{
			r[i] =(byte) System.Convert.ToByte(x[i],16);
		}
		return r;
	}
	public static List<byte> LordCard = new List<byte>();
	public static List<byte> Farmer1Card = new List<byte>();
	public static List<byte> Farmer2Card = new List<byte>();
	public static List<byte> ExtraCard = new List<byte>();
	public static List<byte> LastPlayCard = new List<byte>();
	public static List<byte> CurrentPlayCard = new List<byte>();
}