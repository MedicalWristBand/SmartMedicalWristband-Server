using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PoliceServer.Enums
{
    public enum CustomsCode
    {
        [EnumMember(Value = "گمرک مترکز")]
        motemarkez = 99000, //Sina Decidec the code

        [EnumMember(Value = "منطقه ویژه اقتصادی لاوان")]
        lavan = 50307, //Sina Decidec the code
        
        [EnumMember(Value = "تهران امور واردات")]
        irthr = 10300,

        [EnumMember(Value = "غرب تهران")]
        irthrw = 10200, //Sina Decidec the code

        [EnumMember(Value = "تهران امور نمایشگاهی")]
        irthrs = 10600, //Sina Decidec the code

        [EnumMember(Value = "بجنورد")]
        irbjn = 60700, //Sina Decidec the code

        [EnumMember(Value = "منطقه ویژه اقتصادی شهید رجایی")]
        irbnd = 50100, 

        [EnumMember(Value = "منطقه ویژه صنایع معدنی و فلزی خلیج فارس")]
        felezat = 50800,

        [EnumMember(Value = "منطقه ویژه اقتصادی کشتی سازی خلیج فارس")]
        keshtisazi = 50101, //Sina Decidec the code

        [EnumMember(Value = "شهید باهنر")]
        irsbr = 50200,

        [EnumMember(Value = "بندر لنگه")]
        irbdh = 50300,

        [EnumMember(Value = "منطقه آزاد تجاری قشم")]
        irqsm = 50102,

        [EnumMember(Value = "منطقه آزاد تجاری کیش")]
        irkih = 50400,

        [EnumMember(Value = "همدان")]
        irhmd = 15500, //Sina Decidec the code

        [EnumMember(Value = "منطقه ویژه اقتصادی یزد")]
        iryzdv = 50901, //Sina Decidec the code

        [EnumMember(Value = "یزد")]
        iryzd = 50603, //Sina Decidec the code
        
        [EnumMember(Value = "ماهيرود")]
        mairood=60604,

        [EnumMember(Value = "منطقه ویژه اقتصادی پیام")]
        irpym = 10102, //Sina Decidec the code
        
        [EnumMember(Value = "اردبیل")]
        irard = 25400, //Sina Decidec the code

        [EnumMember(Value = "بیله سوار")]
        bileSavar=25401,
        
        [EnumMember(Value = "اصفهان")]
        iresf = 15100, //Sina Decidec the code

        [EnumMember(Value = "ذوب آهن")]
        zoobAhan = 15101, //Sina Decidec the code

        [EnumMember(Value = "فولاد مبارکه")]
        fooladMob = 15102, //Sina Decidec the code

        [EnumMember(Value = "مهران")]
        irmhr = 35104, //Sina Decidec the code

        [EnumMember(Value = "منطقه ویژه اقتصادی بوشهر ۱")]
        irbuz = 45100,
        
        [EnumMember(Value = "منطقه ویژه اقتصادی بوشهر ۲")]
        irbuz2 = 45110, //Sina Decided the code

        [EnumMember(Value = "فرودگاه امام خمینی (ره)")]
        irika = 10103,
        
        [EnumMember(Value = "تبریز")]
        irtbz = 25100, //Sina Decidec the code

        [EnumMember(Value = "فرودگاه تبریز")]
        irtbzFroodgah = 25101, //Sina Decidec the code

        [EnumMember(Value = "منطقه ویژه اقتصادی سهلان")]
        sahlan = 25200,

        [EnumMember(Value = "جلفا")]
        irdju = 25300,

        [EnumMember(Value = "منطقه آزاد تجاری ارس")]
        aras = 25304,

        [EnumMember(Value = "نوردوز")]
        noordooz = 25302,
        
        [EnumMember(Value = "ارومیه")]
        irurm = 30200, //Sina Decidec the code
        
        [EnumMember(Value = "سرو")]
        sarv = 30300, //Sina Decidec the code

        [EnumMember(Value = "بازارچه سرو")]
        sarv2 = 30301, //Sina

        [EnumMember(Value = "تمرچین پیرانشهر")]
        piranshahr = 30207,

        [EnumMember(Value = "خوی")]
        khoy = 30203, //Sina Decidec the code

        [EnumMember(Value = "بازارچه رازي-خوي")]
        khoyRazi = 30204, //Sina

        [EnumMember(Value = "رازی")]
        razi = 30302, //Sina Decidec the code

        [EnumMember(Value = "بازارچه كيله سردشت")]
        kileSardasht = 30206, //Sina

        [EnumMember(Value = "بازرگان")]
        bazargan = 30100,

        [EnumMember(Value = "منطقه آزاد تجاری ماکو")]
        makoo = 30209, //Sina Decidec the code

        [EnumMember(Value = "منطقه اقتصادی ویژه شیراز")]
        irshrzv = 45301, //Sina Decidec the code

        [EnumMember(Value = "فرودگاه شیراز")]
        irshrzf = 45302, //Sina Decidec the code

        [EnumMember(Value = "قزوین")]
        irqzv = 15300, //Sina Decidec the code

        [EnumMember(Value = "قم")]
        irqom = 90100, //Sina Decidec the code

        [EnumMember(Value = "منطقه ویژه اقتصادی سلفچگان")]
        salafchegan = 90101, //Sina Decided the code

        [EnumMember(Value = "سنندج")]
        irsnd = 35300, //Sina Decidec the code

        [EnumMember(Value = "بانه")]
        bane = 35303,

        [EnumMember(Value = "بازارچه سيرانبند")]
        siranband = 35304,

        [EnumMember(Value = "باشماق")]
        bashmagh = 35306,

        [EnumMember(Value = "کرمان")]
        irkrm = 50600, //Sina Decidec the code

        [EnumMember(Value = "منطقه ویژه اقتصادی رفسنجان")]
        rafsanjan = 50601, //Sina Decided the code

        [EnumMember(Value = "منطقه ویژه اقتصادی سیرجان")]
        sirjan = 50602, //Sina Decided the code

        [EnumMember(Value = "منطقه ویژه اقتصادی بم")]
        bam = 50605, //Sina Decided the code

        [EnumMember(Value = "کرمانشاه")]
        irkrmsh = 35100, //Sina Decided the code

        [EnumMember(Value = "خسروی")]
        irkhs = 35200, 

        [EnumMember(Value = "پرویزخان")]
        parvizKhan = 35500, 

        [EnumMember(Value = "باجگیران")]
        bajgiran = 60300, //Sina Decided the code

        [EnumMember(Value = "لطف آباد")]
        lotfAbad = 60400, 

        [EnumMember(Value = "سرخس")]
        sarakhs=60200,

        [EnumMember(Value = "منطقه ویژه اقتصادی سرخس")]
        sarakhsVije=60201, //Sina Decided the code

        [EnumMember(Value = "دوغارون")]
        irdog=60500,

        [EnumMember(Value = "منطقه ویژه دوغارون")]
        irdogVije = 60503, //Sina Decided the code

        [EnumMember(Value = "اهواز")]
        irahv = 40100, //Sina Decided the code

        [EnumMember(Value = "منطقه آزاد تجاری اروند - خرمشهر")]
        irkho=40202,
        
        [EnumMember(Value = "شلمچه")]
        shalamche = 40201, //Sina Decided the code

        [EnumMember(Value = "منطقه آزاد تجاری اروند - آبادان")]
        irabd=40306,

        [EnumMember(Value = "منطقه ویژه اقتصادی بندر امام خمینی (ره)")]
        irbkm=40400,

        [EnumMember(Value = "منطقه ویژه اقتصادی پتروشیمی بندر امام خمینی (ره)")]
        irbkmPet=40401, //Sina Decided the code

        [EnumMember(Value = "زنجان")]
        irznj = 80100, //Sina Decided the code

        [EnumMember(Value = "سمنان")]
        irsmn = 15400, //Sina Decided the code

        [EnumMember(Value = "اینچه برون")]
        incheBoroon = 20500,

        [EnumMember(Value = "زاهدان")]
        zahedan=55100,

        [EnumMember(Value = "میلک")]
        milak=55105,

        [EnumMember(Value = "میرجاوه")]
        mirjaveh=55200,

        [EnumMember(Value = "منطقه آزاد تجاری چابهار")]
        irzbr = 55108,

        [EnumMember(Value = "منطقه آزاد تجاری انزلی")]
        irbaz = 20100,

        [EnumMember(Value = "منطقه آزاد تجاری حسن رود انزلی")]
        hasanRoud = 20601,

        [EnumMember(Value = "آستارا")]
        irasr=20200,

        [EnumMember(Value = "منطقه ویژه اقتصادی لرستان (ازنا)")]
        azna = 15502, //Sina Decided the code

        [EnumMember(Value = "خرم آباد")]
        khoramAbad = 15501, //Sina Decided the code

        [EnumMember(Value = "منطقه ویژه اقتصادی نوشهر")]
        noshahr = 20306, //Sina Decided the code

        [EnumMember(Value = "منطقه ویژه اقتصادی امیرآباد")]
        iramd=20304,

        [EnumMember(Value = "اراک")]
        irra = 15200,

        [EnumMember(Value = "مشهد")]
        irmhd=60100,
        
        [EnumMember(Value = "گناوه")]
        irgnh=45200,

        [EnumMember(Value = "عسلویه")]
        irasa = 45108,

        [EnumMember(Value = "ساوه")]
        irsvh=15201,

        [EnumMember(Value = "منطقه ويژه اقتصادي انرژي پارس")]
        energyPars = 45400,

        [EnumMember(Value = "ساری")]
        sari = 20303,

        [EnumMember(Value = "فریدون کنار")]
        fereydunKenar = 20302,

        [EnumMember(Value = "منطقه ويژه اقتصادي کاوه")]
        kave = 15202,

        [EnumMember(Value = "منطقه ويژه اقتصادي ايرانيان(زرنديه)")]
        zarandiye = 15203,

        [EnumMember(Value = "شهرکرد")]
        shahrKord = 10800,

        [EnumMember(Value = "خارک")]
        khark = 45103,

        [EnumMember(Value = "چابهار")]
        chabahar = 55300,

        [EnumMember(Value = "حوزه 3 امانات پستي")]
        post3 = 10400,

        [EnumMember(Value = "فرودگاه اصفهان")]
        esfAirport = 15103,

        [EnumMember(Value = "بندر نوشهر")]
        noushahr = 20300,

        [EnumMember(Value = "بازارچه نوردوز")]
        nordooz = 25303,

        [EnumMember(Value = "پلدشت")]
        poldasht = 30101,

        [EnumMember(Value = "بازارچه ساري سو")]
        sariSu = 30102,

        [EnumMember(Value = "بازارچه پيرانشهر-تمرچين")]
        tamrchin = 30202,

        [EnumMember(Value = "بازارچه صنم بلاغي")]
        sanamBolaghi = 30303,

        [EnumMember(Value = "پاوه")]
        pave = 35101,

        [EnumMember(Value = "بازارچه شوشمي")]
        shooshami = 35102,

        [EnumMember(Value = "شيخ صله")]
        sheykhSele = 35105,

        [EnumMember(Value = "بازارچه شيخ صله")]
        sheykhSele2 = 35106,

        [EnumMember(Value = "بندر ريگ")]
        bandarRig = 45202,

        [EnumMember(Value = "فرودگاه لار")]
        larAirPort = 45303,

        [EnumMember(Value = "ديلم")]
        deylam = 45201,

/*
بازارچه پرويزخان=35201
قصر شيرين=35400
چذابه -مرزبستان=40102
اروندكنار=40302
چوئبده=40303
بازارچه ابادان=40305
ماه شهر=40403
هنديجان=40404
كنگ=50301
جاسك=50500
سراوان=55102
بازارچه ميلک=55103
زابل=55104
بازارچه ميرجاوه=55106
بازارچه كوهك-سراوان=55107
بازارچه مرزي جالق=55110
بازارچه مرزي ريمدان=55112
پيشين=55301
بازارچه پيشين=55304
بازارچه باجگيران=60301
بازارچه يزدان=60302
بازارچه گلورده=60501
بازارچه دوغارون=60502
بازارچه دوكوهانه=60601
بازارچه ماهيرود=60603
بازارچه مهران=70201
بناب=25102
منطقه آزاد تجاري خداآفرين=25205
منطقه ويژه اقتصادي لامرد=45115
بيرجند=60600
کاشان=15104
سجافي=40402
مراغه=25500
ياسوج=15505
منطقه ويژه اسلام اباد غرب=35107
مهاباد=30201
ملاير=15503
سومار=35202
آمل=20305
        */
        
    }
}