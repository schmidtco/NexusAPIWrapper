using Newtonsoft.Json; 
using System;
using System.Linq;
namespace NexusAPIWrapper{ 

    public class ObservationsPrototype_Root
    {
        [JsonProperty("state")]
        public ObservationsPrototype_State State;

        [JsonProperty("stateUnambiguousValue")]
        public bool? StateUnambiguousValue;

        [JsonProperty("currentLevel")]
        public ObservationsPrototype_CurrentLevel CurrentLevel;

        [JsonProperty("currentLevelUnambiguousValue")]
        public bool? CurrentLevelUnambiguousValue;

        [JsonProperty("currentLevelDescription")]
        public ObservationsPrototype_CurrentLevelDescription CurrentLevelDescription;

        [JsonProperty("currentLevelDescriptionUnambiguousValue")]
        public bool? CurrentLevelDescriptionUnambiguousValue;

        [JsonProperty("expectedLevel")]
        public ObservationsPrototype_ExpectedLevel ExpectedLevel;

        [JsonProperty("expectedLevelUnambiguousValue")]
        public bool? ExpectedLevelUnambiguousValue;

        [JsonProperty("expectedLevelDescription")]
        public ObservationsPrototype_ExpectedLevelDescription ExpectedLevelDescription;

        [JsonProperty("expectedLevelDescriptionUnambiguousValue")]
        public bool? ExpectedLevelDescriptionUnambiguousValue;

        [JsonProperty("goals")]
        public ObservationsPrototype_Goals Goals;

        [JsonProperty("goalsUnambiguousValue")]
        public bool? GoalsUnambiguousValue;

        [JsonProperty("execution")]
        public ObservationsPrototype_Execution Execution;

        [JsonProperty("executionUnambiguousValue")]
        public bool? ExecutionUnambiguousValue;

        [JsonProperty("limitations")]
        public ObservationsPrototype_Limitations Limitations;

        [JsonProperty("limitationsUnambiguousValue")]
        public bool? LimitationsUnambiguousValue;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("patient")]
        public ObservationsPrototype_Patient Patient;

        [JsonProperty("_links")]
        public ObservationsPrototype_Links Links;

        //public ObservationsPrototype_Root()
        //{
        //    State = new ObservationsPrototype_State();
        //    StateUnambiguousValue = null;

        //    CurrentLevel = new ObservationsPrototype_CurrentLevel();
        //    CurrentLevelUnambiguousValue = null;

        //    CurrentLevelDescription = new ObservationsPrototype_CurrentLevelDescription();
        //    CurrentLevelDescriptionUnambiguousValue = null;

        //    ExpectedLevel = new ObservationsPrototype_ExpectedLevel();
        //    ExpectedLevelUnambiguousValue = null;

        //    ExpectedLevelDescription = new ObservationsPrototype_ExpectedLevelDescription();
        //    ExpectedLevelDescriptionUnambiguousValue = null;

        //    Goals = new ObservationsPrototype_Goals();
        //    GoalsUnambiguousValue = null;

        //    Execution = new ObservationsPrototype_Execution();
        //    ExecutionUnambiguousValue = null;

        //    Limitations = new ObservationsPrototype_Limitations();
        //    LimitationsUnambiguousValue = null;

        //    UpdateDate = null;

        //    Patient = new ObservationsPrototype_Patient();

        //    Links = new ObservationsPrototype_Links();
        //}
        public void AddDataFromPatientCondition(PatientConditions_Root patientCondition)
        {
            if (patientCondition.CurrentLevel != null)
            {
                this.CurrentLevel.Value.Id = this.CurrentLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.CurrentLevel.Name).Id;
                this.CurrentLevel.Value.Active = this.CurrentLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.CurrentLevel.Name).Active;
                this.CurrentLevel.Value.AdditionalInformation = this.CurrentLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.CurrentLevel.Name).AdditionalInformation;
                this.CurrentLevel.Value.Version = this.CurrentLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.CurrentLevel.Name).Version;
                this.CurrentLevel.Value.Code = this.CurrentLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.CurrentLevel.Name).Code;
                this.CurrentLevel.Value.Links = this.CurrentLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.CurrentLevel.Name).Links;
                this.CurrentLevel.Value.Marker = this.CurrentLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.CurrentLevel.Name).Marker;
                this.CurrentLevel.Value.Name = this.CurrentLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.CurrentLevel.Name).Name;
                this.CurrentLevel.Value.NumericRepresentation = this.CurrentLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.CurrentLevel.Name).NumericRepresentation;

                this.CurrentLevelDescription.Value = patientCondition.CurrentLevelDescription;

                if (this.ExpectedLevel.Value != null)
                {
                    this.ExpectedLevel.Value.Id = this.ExpectedLevel.PossibleValues.FirstOrDefault(x => x.Name == patientCondition.ExpectedLevel.Name).Id;
                }
                else
                {
                    this.ExpectedLevel.Value = new ObservationsPrototype_Value();
                    this.ExpectedLevel.Value.Id = this.ExpectedLevel.PossibleValues[0].Id;
                }

                this.ExpectedLevelDescription.Value = patientCondition.ExpectedLevelDescription;

                this.Goals.Value = patientCondition.Goals;

                this.State.Value.Id = this.State.PossibleValues.FirstOrDefault(x => x.Name == "Aktiv").Id;
            }
            else
            {
                this.State.Id = this.State.PossibleValues[0].Id;


                this.CurrentLevel.Value = new ObservationsPrototype_Value();

                this.CurrentLevel.Value.Id = this.CurrentLevel.PossibleValues[0].Id;
                this.CurrentLevel.Value.Active = this.CurrentLevel.PossibleValues[0].Active;
                this.CurrentLevel.Value.AdditionalInformation = this.CurrentLevel.PossibleValues[0].AdditionalInformation;
                this.CurrentLevel.Value.Version = this.CurrentLevel.PossibleValues[0].Version;
                this.CurrentLevel.Value.Code = this.CurrentLevel.PossibleValues[0].Code;
                this.CurrentLevel.Value.Links = this.CurrentLevel.PossibleValues[0].Links;
                this.CurrentLevel.Value.Marker = this.CurrentLevel.PossibleValues[0].Marker;
                this.CurrentLevel.Value.Name = this.CurrentLevel.PossibleValues[0].Name;
                this.CurrentLevel.Value.NumericRepresentation = this.CurrentLevel.PossibleValues[0].NumericRepresentation;

                this.CurrentLevelDescription.Value = patientCondition.CurrentLevelDescription;

                this.ExpectedLevel.Value = new ObservationsPrototype_Value();
                this.ExpectedLevel.Value.Id = this.ExpectedLevel.PossibleValues[0].Id;

                this.ExpectedLevelDescription.Value = patientCondition.ExpectedLevelDescription;

                this.Goals.Value = patientCondition.Goals;

                this.State.Value.Id = this.State.PossibleValues.FirstOrDefault(x => x.Name == "Aktiv").Id;
            }
            
        }
    }


}