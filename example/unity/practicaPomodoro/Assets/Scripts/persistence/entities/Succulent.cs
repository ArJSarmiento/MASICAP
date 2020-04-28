﻿
    using System;
    using UnityEngine;

    public class Succulent
    {
        public string persistentId;
        public string name;
        public string resourcePath;
        public float waterLevel;
        public float waterDecayIndex; //
        public long lastTimeChanged;

        public Succulent(string persistentId, string name, string resourcePath, float waterLevel, float waterDecayIndex)
        {
            this.persistentId = persistentId;
            this.name = name;
            this.resourcePath = resourcePath;
            this.waterLevel = waterLevel;
            this.waterDecayIndex = waterDecayIndex;
            lastTimeChanged = DateTime.Now.Ticks;

        }

        public void updateWaterLevel(long timeDelta)
        {
            DateTime tp=new DateTime(timeDelta);
            Debug.Log(tp.Minute);
            //waterLevel -= waterDecayIndex *tp.Second;
        }

        public override string ToString()
        {
            return "Name: " + name + " - " + "Water level: " + waterLevel;
        }
    }
