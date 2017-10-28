
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Show : MonoBehaviour
{
	public static Show _Instance;
	public RawImage CardPrefab;
	public Transform[] T;
	public Text cmdarg;
	public byte[] lordCard;
	public byte[] farmer1Card;
	public byte[] farmer2Card;
	public byte[] extraCard;
	public byte[] lastPlayCard;
	public byte[] thisPlayCard;
	//不支持输入 0x开头
	void Start()
	{
		_Instance = this;
		Data.Parse();
		LoadImage();
		WriteLordImage(lordCard);
		WriteExtraImage (extraCard);
		WriteFarmer1Image(farmer1Card);
		WriteFarmer2Image(farmer2Card);
	}
	string GetCardName(byte cardValue)
	{
		var colorCollection = new string[] { "", "Club", "Diamond", "Heart", "Spade" };
		var valueCollection = new string[]{
			"SJoker",
			"LJoker",
			"Three",
			"Four",
			"Five",
			"Six",
			"Seven",
			"Eight",
			"Nine",
			"Ten",
			"Jack",
			"Queen",
			"King",
			"One",
			"Two"};
		int color = (cardValue & 0xf0) >> 4;

		int value = cardValue & 0x0f;
		return colorCollection[color] + valueCollection[value - 1];
	}
	Dictionary<string, Texture2D> CardImageMap = new Dictionary<string, Texture2D>();
	Texture2D DeskImg;
	void LoadImage()
	{
		UnityEngine.Object[] allCards = Resources.LoadAll("CardsImage");
		foreach (var a in allCards)
		{
			CardImageMap.Add(a.name, (Texture2D)a);
		}
		if (DeskImg != null)
		{
			Debug.Log("加载成功");
		}
		Debug.Log(allCards[2].name);
	}

	RectTransform GetRawImage(byte card)
	{
		string name = GetCardName(card);
		Texture2D t = CardImageMap[name];
		RawImage image = Instantiate<RawImage>(CardPrefab);
		image.texture = t;
		return image.GetComponent<RectTransform>(); ;
	}

	void WriteLordImage(byte[] cards)
	{
		float posX = 0f;
		foreach (var x in cards)
		{
			var tran = GetRawImage(x);
			tran.SetParent(T[0], false);
			tran.localPosition = new Vector3(posX, tran.localPosition.y);
			posX += 40;
		}
	}	void WriteExtraImage(byte[] cards)
	{
		float posX = 0f;
		foreach (var x in cards)
		{
			var tran = GetRawImage(x);
			tran.SetParent(T[3], false);
			tran.localPosition = new Vector3(posX, tran.localPosition.y);
			posX += 40;
		}
	}
	void WriteFarmer1Image(byte[] cards)
	{
       float posY= 0f;
		foreach (var x in cards)
		{
			var tran = GetRawImage(x);
			tran.SetParent(T[1], false);
			tran.Rotate(0, 0, 90);
			tran.localPosition = new Vector3(tran.localPosition.x, posY);
			posY -= 40;
		}
	}
	void WriteFarmer2Image(byte[] cards)
	{
		float posY = 0f;
		foreach (var x in cards)
		{
			var tran = GetRawImage(x);
			tran.SetParent(T[2], false);
			tran.Rotate(0, 0, 90);
			tran.localPosition = new Vector3(tran.localPosition.x, posY);
			posY -= 40;
		}
	}
}
