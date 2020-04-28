using Apis.Converter;
using MessagePack;
using System;
using UnityEngine;

[Serializable]
public class AvatarParameter
{
    [Key(0)]
    public float headTarget_PositionX;
    [Key(1)]
    public float headTarget_PositionY;
    [Key(2)]
    public float headTarget_PositionZ;
    [Key(3)]
    public float headTarget_RotateX;
    [Key(4)]
    public float headTarget_RotateY;
    [Key(5)]
    public float headTarget_RotateZ;
    [Key(6)]
    public float headTarget_RotateW;
    [Key(7)]
    public float lHandTarget_PositionX;
    [Key(8)]
    public float lHandTarget_PositionY;
    [Key(9)]
    public float lHandTarget_PositionZ;
    [Key(10)]
    public float lHandTarget_RotateX;
    [Key(11)]
    public float lHandTarget_RotateY;
    [Key(12)]
    public float lHandTarget_RotateZ;
    [Key(13)]
    public float lHandTarget_RotateW;
    [Key(14)]
    public float rHandTarget_PositionX;
    [Key(15)]
    public float rHandTarget_PositionY;
    [Key(16)]
    public float rHandTarget_PositionZ;
    [Key(17)]
    public float rHandTarget_RotateX;
    [Key(18)]
    public float rHandTarget_RotateY;
    [Key(19)]
    public float rHandTarget_RotateZ;
    [Key(20)]
    public float rHandTarget_RotateW;
    [Key(21)]
    public float mainModel_PositionX;
    [Key(22)]
    public float mainModel_PositionY;
    [Key(23)]
    public float mainModel_PositionZ;

    public override string ToString() => JsonUtility.ToJson(this);
    public void FromString(string json)
    {
        var source = JsonUtility.FromJson<AvatarParameter>(json);
        headTarget_PositionX = source.headTarget_PositionX;
        headTarget_PositionY = source.headTarget_PositionY;
        headTarget_PositionZ = source.headTarget_PositionZ;
        headTarget_RotateX = source.headTarget_RotateX;
        headTarget_RotateY = source.headTarget_RotateY;
        headTarget_RotateZ = source.headTarget_RotateZ;
        headTarget_RotateW = source.headTarget_RotateW;
        lHandTarget_PositionX = source.lHandTarget_PositionX;
        lHandTarget_PositionY = source.lHandTarget_PositionY;
        lHandTarget_PositionZ = source.lHandTarget_PositionZ;
        lHandTarget_RotateX = source.lHandTarget_RotateX;
        lHandTarget_RotateY = source.lHandTarget_RotateY;
        lHandTarget_RotateZ = source.lHandTarget_RotateZ;
        lHandTarget_RotateW = source.lHandTarget_RotateW;
        rHandTarget_PositionX = source.rHandTarget_PositionX;
        rHandTarget_PositionY = source.rHandTarget_PositionY;
        rHandTarget_PositionZ = source.rHandTarget_PositionZ;
        rHandTarget_RotateX = source.rHandTarget_RotateX;
        rHandTarget_RotateY = source.rHandTarget_RotateY;
        rHandTarget_RotateZ = source.rHandTarget_RotateZ;
        rHandTarget_RotateW = source.rHandTarget_RotateW;
        mainModel_PositionX = source.mainModel_PositionX;
        mainModel_PositionY = source.mainModel_PositionY;
        mainModel_PositionZ = source.mainModel_PositionZ;
    }
}