﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using League_of_Hate.JData;
//
//    var summonerIcon = SummonerIcon.FromJson(jsonString);

using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace League_of_Hate.JData
{
    public partial class SummonerIcon
    {
        [JsonProperty("accountId")]
        public long AccountId { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("internalName")]
        public string InternalName { get; set; }

        [JsonProperty("percentCompleteForNextLevel")]
        public long PercentCompleteForNextLevel { get; set; }

        [JsonProperty("profileIconId")]
        public long ProfileIconId { get; set; }

        [JsonProperty("puuid")]
        public string Puuid { get; set; }

        [JsonProperty("rerollPoints")]
        public RerollPoints RerollPoints { get; set; }

        [JsonProperty("summonerId")]
        public long SummonerId { get; set; }

        [JsonProperty("summonerLevel")]
        public long SummonerLevel { get; set; }

        [JsonProperty("xpSinceLastLevel")]
        public long XpSinceLastLevel { get; set; }

        [JsonProperty("xpUntilNextLevel")]
        public long XpUntilNextLevel { get; set; }
    }

    public partial class RerollPoints
    {
        [JsonProperty("currentPoints")]
        public long CurrentPoints { get; set; }

        [JsonProperty("maxRolls")]
        public long MaxRolls { get; set; }

        [JsonProperty("numberOfRolls")]
        public long NumberOfRolls { get; set; }

        [JsonProperty("pointsCostToRoll")]
        public long PointsCostToRoll { get; set; }

        [JsonProperty("pointsToReroll")]
        public long PointsToReroll { get; set; }
    }

    public partial class SummonerIcon
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };

        public static SummonerIcon FromJson(string json) => JsonConvert.DeserializeObject<SummonerIcon>(json, Settings);

        public static string ToJson(SummonerIcon self) => JsonConvert.SerializeObject(self, Settings);
    }
}
