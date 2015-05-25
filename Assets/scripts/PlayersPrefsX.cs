// ArrayPrefs2 v 1.4

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerPrefsX
{
static private int endianDiff1;
static private int endianDiff2;
static private int idx;
static private byte [] byteBlock;

enum ArrayType {Float, Int32, Bool, String, Vector2, Vector3, Quaternion, Color}

  public static bool SetStringArray (String key, String[] stringArray)
  {
  	var bytes = new byte[stringArray.Length + 1];
  	bytes[0] = System.Convert.ToByte (ArrayType.String);	// Identifier
  	Initialize();

  	// Store the length of each string that's in stringArray, so we can extract the correct strings in GetStringArray
  	for (var i = 0; i < stringArray.Length; i++)
  	{
  		if (stringArray[i] == null)
  		{
  			Debug.LogError ("Can't save null entries in the string array when setting " + key);
  			return false;
  		}
  		if (stringArray[i].Length > 255)
  		{
  			Debug.LogError ("Strings cannot be longer than 255 characters when setting " + key);
  			return false;
  		}
  		bytes[idx++] = (byte)stringArray[i].Length;
  	}

  	try
  	{
  		PlayerPrefs.SetString (key, System.Convert.ToBase64String (bytes) + "|" + String.Join("", stringArray));
  	}
  	catch
  	{
  		return false;
  	}
  	return true;
  }

  public static String[] GetStringArray (String key)
  {
  	if (PlayerPrefs.HasKey(key)) {
  		var completeString = PlayerPrefs.GetString(key);
  		var separatorIndex = completeString.IndexOf("|"[0]);
  		if (separatorIndex < 4) {
  			Debug.LogError ("Corrupt preference file for " + key);
  			return new String[0];
  		}
  		var bytes = System.Convert.FromBase64String (completeString.Substring(0, separatorIndex));
  		if ((ArrayType)bytes[0] != ArrayType.String) {
  			Debug.LogError (key + " is not a string array");
  			return new String[0];
  		}
  		Initialize();

  		var numberOfEntries = bytes.Length-1;
  		var stringArray = new String[numberOfEntries];
  		var stringIndex = separatorIndex + 1;
  		for (var i = 0; i < numberOfEntries; i++)
  		{
  			int stringLength = bytes[idx++];
  			if (stringIndex + stringLength > completeString.Length)
  			{
  				Debug.LogError ("Corrupt preference file for " + key);
  				return new String[0];
  			}
  			stringArray[i] = completeString.Substring(stringIndex, stringLength);
  			stringIndex += stringLength;
  		}

  		return stringArray;
  	}
  	return new String[0];
  }

  public static String[] GetStringArray (String key, String defaultValue, int defaultSize)
  {
  	if (PlayerPrefs.HasKey(key))
  	{
  		return GetStringArray(key);
  	}
  	var stringArray = new String[defaultSize];
  	for(int i=0;i<defaultSize;i++)
  	{
  		stringArray[i] = defaultValue;
  	}
  	return stringArray;
  }

  public static bool SetIntArray (String key, int[] intArray)
  {
  	return SetValue (key, intArray, ArrayType.Int32, 1, ConvertFromInt);
  }

  private static bool SetValue<T> (String key, T array, ArrayType arrayType, int vectorNumber, Action<T, byte[],int> convert) where T : IList
  {
  	var bytes = new byte[(4*array.Count)*vectorNumber + 1];
  	bytes[0] = System.Convert.ToByte (arrayType);	// Identifier
  	Initialize();

  	for (var i = 0; i < array.Count; i++) {
  		convert (array, bytes, i);
  	}
  	return SaveBytes (key, bytes);
  }

  private static void ConvertFromInt (int[] array, byte[] bytes, int i)
  {
  	ConvertInt32ToBytes (array[i], bytes);
  }

  private static void ConvertFromFloat (float[] array, byte[] bytes, int i)
  {
  	ConvertFloatToBytes (array[i], bytes);
  }

  private static void ConvertFromVector2 (Vector2[] array, byte[] bytes, int i)
  {
  	ConvertFloatToBytes (array[i].x, bytes);
  	ConvertFloatToBytes (array[i].y, bytes);
  }

  private static void ConvertFromVector3 (Vector3[] array, byte[] bytes, int i)
  {
  	ConvertFloatToBytes (array[i].x, bytes);
  	ConvertFloatToBytes (array[i].y, bytes);
  	ConvertFloatToBytes (array[i].z, bytes);
  }

  private static void ConvertFromQuaternion (Quaternion[] array, byte[] bytes, int i)
  {
  	ConvertFloatToBytes (array[i].x, bytes);
  	ConvertFloatToBytes (array[i].y, bytes);
  	ConvertFloatToBytes (array[i].z, bytes);
  	ConvertFloatToBytes (array[i].w, bytes);
  }

  private static void ConvertFromColor (Color[] array, byte[] bytes, int i)
  {
  	ConvertFloatToBytes (array[i].r, bytes);
  	ConvertFloatToBytes (array[i].g, bytes);
  	ConvertFloatToBytes (array[i].b, bytes);
  	ConvertFloatToBytes (array[i].a, bytes);
  }

  public static int[] GetIntArray (String key)
  {
  	var intList = new List<int>();
  	GetValue (key, intList, ArrayType.Int32, 1, ConvertToInt);
  	return intList.ToArray();
  }

  public static int[] GetIntArray (String key, int defaultValue, int defaultSize)
  {
  	if (PlayerPrefs.HasKey(key))
  	{
  		return GetIntArray(key);
  	}
  	var intArray = new int[defaultSize];
  	for (int i=0; i<defaultSize; i++)
  	{
  		intArray[i] = defaultValue;
  	}
  	return intArray;
  }

  public static float[] GetFloatArray (String key)
  {
  	var floatList = new List<float>();
  	GetValue (key, floatList, ArrayType.Float, 1, ConvertToFloat);
  	return floatList.ToArray();
  }

  public static float[] GetFloatArray (String key, float defaultValue, int defaultSize)
  {
  	if (PlayerPrefs.HasKey(key))
  	{
  		return GetFloatArray(key);
  	}
  	var floatArray = new float[defaultSize];
  	for (int i=0; i<defaultSize; i++)
  	{
  		floatArray[i] = defaultValue;
  	}
  	return floatArray;
  }

  private static void GetValue<T> (String key, T list, ArrayType arrayType, int vectorNumber, Action<T, byte[]> convert) where T : IList
  {
  	if (PlayerPrefs.HasKey(key))
  	{
  		var bytes = System.Convert.FromBase64String (PlayerPrefs.GetString(key));
  		if ((bytes.Length-1) % (vectorNumber*4) != 0)
  		{
  			Debug.LogError ("Corrupt preference file for " + key);
  			return;
  		}
  		if ((ArrayType)bytes[0] != arrayType)
  		{
  			Debug.LogError (key + " is not a " + arrayType.ToString() + " array");
  			return;
  		}
  		Initialize();

  		var end = (bytes.Length-1) / (vectorNumber*4);
  		for (var i = 0; i < end; i++)
  		{
  			convert (list, bytes);
  		}
  	}
  }

  private static void ConvertToInt (List<int> list, byte[] bytes)
  {
  	list.Add (ConvertBytesToInt32(bytes));
  }

  private static void ConvertToFloat (List<float> list, byte[] bytes)
  {
  	list.Add (ConvertBytesToFloat(bytes));
  }

  private static void ConvertToVector2 (List<Vector2> list, byte[] bytes)
  {
  	list.Add (new Vector2(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
  }

  private static void ConvertToVector3 (List<Vector3> list, byte[] bytes)
  {
  	list.Add (new Vector3(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
  }

  private static void ConvertToQuaternion (List<Quaternion> list,byte[] bytes)
  {
  	list.Add (new Quaternion(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
  }

  private static void ConvertToColor (List<Color> list, byte[] bytes)
  {
  	list.Add (new Color(ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes), ConvertBytesToFloat(bytes)));
  }

  public static void ShowArrayType (String key)
  {
  	var bytes = System.Convert.FromBase64String (PlayerPrefs.GetString(key));
  	if (bytes.Length > 0)
  	{
  		ArrayType arrayType = (ArrayType)bytes[0];
  		Debug.Log (key + " is a " + arrayType.ToString() + " array");
  	}
  }

  private static void Initialize ()
  {
  	if (System.BitConverter.IsLittleEndian)
  	{
  		endianDiff1 = 0;
  		endianDiff2 = 0;
  	}
  	else
  	{
  		endianDiff1 = 3;
  		endianDiff2 = 1;
  	}
  	if (byteBlock == null)
  	{
  		byteBlock = new byte[4];
  	}
  	idx = 1;
  }

  private static bool SaveBytes (String key, byte[] bytes)
  {
  	try
  	{
  		PlayerPrefs.SetString (key, System.Convert.ToBase64String (bytes));
  	}
  	catch
  	{
  		return false;
  	}
  	return true;
  }

  private static void ConvertFloatToBytes (float f, byte[] bytes)
  {
  	byteBlock = System.BitConverter.GetBytes (f);
  	ConvertTo4Bytes (bytes);
  }

  private static float ConvertBytesToFloat (byte[] bytes)
  {
  	ConvertFrom4Bytes (bytes);
  	return System.BitConverter.ToSingle (byteBlock, 0);
  }

  private static void ConvertInt32ToBytes (int i, byte[] bytes)
  {
  	byteBlock = System.BitConverter.GetBytes (i);
  	ConvertTo4Bytes (bytes);
  }

  private static int ConvertBytesToInt32 (byte[] bytes)
  {
  	ConvertFrom4Bytes (bytes);
  	return System.BitConverter.ToInt32 (byteBlock, 0);
  }

  private static void ConvertTo4Bytes (byte[] bytes)
  {
  	bytes[idx  ] = byteBlock[    endianDiff1];
  	bytes[idx+1] = byteBlock[1 + endianDiff2];
  	bytes[idx+2] = byteBlock[2 - endianDiff2];
  	bytes[idx+3] = byteBlock[3 - endianDiff1];
  	idx += 4;
  }

  private static void ConvertFrom4Bytes (byte[] bytes)
  {
  	byteBlock[    endianDiff1] = bytes[idx  ];
  	byteBlock[1 + endianDiff2] = bytes[idx+1];
  	byteBlock[2 - endianDiff2] = bytes[idx+2];
  	byteBlock[3 - endianDiff1] = bytes[idx+3];
  	idx += 4;
  }
}
