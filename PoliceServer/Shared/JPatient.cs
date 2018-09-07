using System;
using System.Collections.Generic;
using System.Linq;
using PoliceServer.Models;

namespace PoliceServer.Shared
{
    public class JPatient
    {
        public string name;
        public string lastname;
        public string patientId;
//        public string tarikhPaziresh;
//        public string tarikhTarkhis;
//        public string labReport;
//        public string sideEffects;
//        public string earlyDiagnosis;
//        public string secondaryDiagnosis;
//        public string finalDiagnosis;
//        public string relatedDoctor;
        public string jsonData;


        public Patient ConvertToPatient()
        {
            return new Patient()
            {
                Name = name,
                Family = lastname,
                LastUpdateDateTime = DateTime.Now,
                PatientCode = patientId,
                Status = jsonData,
            };
        }

//        {
//            "name":"ali",
//            "lastname":"alavi",
//            "patientId":"39928472",
//            "tarikhPaziresh": "2018-07-05 22:21:30",
//            "tarikhTarkhis": "2018-10-05 22:21:30",
//            "labReport": "described-labreport",
//            "summary": "full detail about patient during Hospitalization",
//            "sideEffects": "side effects that occured in patient",
//            "earlyDiagnosis": "diagnosis 1",
//            "secondaryDiagnosis": "diagnosis 2",
//            "finalDiagnosis":
//            "diagnosis 3",
//            "relatedDoctor":
//            "Dr.Samii"
//        }
        
    }
}