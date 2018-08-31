using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PoliceServer.Enums
{
    public class OstanHelper
    {
    }

    public enum Ostan
    {
        [EnumMember(Value = "استان")]
        NotSelected,

        [EnumMember(Value = "آذربایجان شرقی")]
        AzarbayjanEeast,
        [EnumMember(Value = "آذربایجان غربی")]
        AzarbayjanWest,
        [EnumMember(Value = "اردبیل")]
        Ardebil,
        [EnumMember(Value = "اصفهان")]
        Esfahan,
        [EnumMember(Value = "البرز")]
        Alborz,
        [EnumMember(Value = "ایلام")]
        Ilam,
        [EnumMember(Value = "بوشهر")]
        Boushehr,
        [EnumMember(Value = "تهران")]
        Tehran,
        [EnumMember(Value = "چهارمحال و بختیاری")]
        ChaharmahaloBakhtiari,
        [EnumMember(Value = "خراسان جنوبی")]
        KhorasanSouth,
        [EnumMember(Value = "خراسان رضوی")]
        KhorasanRzv,
        [EnumMember(Value = "خراسان شمالی")]
        KhorasanNorth,
        [EnumMember(Value = "خوزستان")]
        Khouzestan,
        [EnumMember(Value = "زنجان")]
        Zanjan,
        [EnumMember(Value = "سمنان")]
        Semnan,
        [EnumMember(Value = "سیستان و بلوچستان")]
        SistanoBaluchestan,
        [EnumMember(Value = "فارس")]
        Fars,
        [EnumMember(Value = "قزوین")]
        Ghazvin,
        [EnumMember(Value = "قم")]
        Ghom,
        [EnumMember(Value = "کردستان")]
        Kordestan,
        [EnumMember(Value = "کرمان")]
        Kerman,
        [EnumMember(Value = "کرمانشاه")]
        Kermanshah,
        [EnumMember(Value = "کهگیلویه و بویراحمد")]
        KohkiluyeVaBoyerahmad,
        [EnumMember(Value = "گلستان")]
        Golestan,
        [EnumMember(Value = "گیلان")]
        Gilan,
        [EnumMember(Value = "لرستان")]
        Lorestan,
        [EnumMember(Value = "مازندران")]
        Mazandaran,
        [EnumMember(Value = "مرکزی")]
        Markazi,
        [EnumMember(Value = "هرمزگان")]
        Hormozgan,
        [EnumMember(Value = "همدان")]
        Hamedan,
        [EnumMember(Value = "یزد")]
        Yazd

    }
}