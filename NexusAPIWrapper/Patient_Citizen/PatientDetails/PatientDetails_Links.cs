using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_Links
    {
        [JsonProperty("self")]
        public PatientDetails_Self Self;

        [JsonProperty("availableCountries")]
        public PatientDetails_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public PatientDetails_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("prototypeValuePeriod")]
        public PatientDetails_PrototypeValuePeriod PrototypeValuePeriod;

        [JsonProperty("deleteValuePeriod")]
        public PatientDetails_DeleteValuePeriod DeleteValuePeriod;

        [JsonProperty("prototypeResidentialMunicipalityValueSchedule")]
        public PatientDetails_PrototypeResidentialMunicipalityValueSchedule PrototypeResidentialMunicipalityValueSchedule;

        [JsonProperty("prototypePayingMunicipalityValueSchedule")]
        public PatientDetails_PrototypePayingMunicipalityValueSchedule PrototypePayingMunicipalityValueSchedule;

        [JsonProperty("prototypeActingMunicipalityValueSchedule")]
        public PatientDetails_PrototypeActingMunicipalityValueSchedule PrototypeActingMunicipalityValueSchedule;

        [JsonProperty("documentPrototype")]
        public PatientDetails_DocumentPrototype DocumentPrototype;

        [JsonProperty("documentPrototypeUndefinedPathway")]
        public PatientDetails_DocumentPrototypeUndefinedPathway DocumentPrototypeUndefinedPathway;

        [JsonProperty("availableFormDefinitions")]
        public PatientDetails_AvailableFormDefinitions AvailableFormDefinitions;

        [JsonProperty("citizenOverviewForms")]
        public PatientDetails_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("searchFormUids")]
        public PatientDetails_SearchFormUids SearchFormUids;

        [JsonProperty("medcomFs3DataExchangeSetPrototype")]
        public PatientDetails_MedcomFs3DataExchangeSetPrototype MedcomFs3DataExchangeSetPrototype;

        [JsonProperty("getFormDataItemsByTags")]
        public PatientDetails_GetFormDataItemsByTags GetFormDataItemsByTags;

        [JsonProperty("availableLetterTemplates")]
        public PatientDetails_AvailableLetterTemplates AvailableLetterTemplates;

        [JsonProperty("availableLetterTemplatesNotPreassigned")]
        public PatientDetails_AvailableLetterTemplatesNotPreassigned AvailableLetterTemplatesNotPreassigned;

        [JsonProperty("medicationCard")]
        public PatientDetails_MedicationCard MedicationCard;

        [JsonProperty("medicationDatainsight")]
        public PatientDetails_MedicationDatainsight MedicationDatainsight;

        [JsonProperty("pnMedicationDatainsight")]
        public PatientDetails_PnMedicationDatainsight PnMedicationDatainsight;

        [JsonProperty("medicationCardInViewModeHandout")]
        public PatientDetails_MedicationCardInViewModeHandout MedicationCardInViewModeHandout;

        [JsonProperty("medicationCardInViewModeNursing")]
        public PatientDetails_MedicationCardInViewModeNursing MedicationCardInViewModeNursing;

        [JsonProperty("historicMedicationOverview")]
        public PatientDetails_HistoricMedicationOverview HistoricMedicationOverview;

        [JsonProperty("medicationCardPatientInformation")]
        public PatientDetails_MedicationCardPatientInformation MedicationCardPatientInformation;

        [JsonProperty("pnMedications")]
        public PatientDetails_PnMedications PnMedications;

        [JsonProperty("pnMedicationGroups")]
        public PatientDetails_PnMedicationGroups PnMedicationGroups;

        [JsonProperty("pnMedicationDispensationAvailableStates")]
        public PatientDetails_PnMedicationDispensationAvailableStates PnMedicationDispensationAvailableStates;

        [JsonProperty("medicineDispensing")]
        public PatientDetails_MedicineDispensing MedicineDispensing;

        [JsonProperty("bulkActivitiesPrintPrototype")]
        public PatientDetails_BulkActivitiesPrintPrototype BulkActivitiesPrintPrototype;

        [JsonProperty("availableProgramPathways")]
        public PatientDetails_AvailableProgramPathways AvailableProgramPathways;

        [JsonProperty("patientActivities")]
        public PatientDetails_PatientActivities PatientActivities;

        [JsonProperty("activePrograms")]
        public PatientDetails_ActivePrograms ActivePrograms;

        [JsonProperty("availablePathwayAssociation")]
        public PatientDetails_AvailablePathwayAssociation AvailablePathwayAssociation;

        [JsonProperty("availableOrganizations")]
        public PatientDetails_AvailableOrganizations AvailableOrganizations;

        [JsonProperty("availablePatientStates")]
        public PatientDetails_AvailablePatientStates AvailablePatientStates;

        [JsonProperty("update")]
        public PatientDetails_Update Update;

        [JsonProperty("manageSms")]
        public PatientDetails_ManageSms ManageSms;

        [JsonProperty("patientCardPrint")]
        public PatientDetails_PatientCardPrint PatientCardPrint;

        [JsonProperty("patientOverview")]
        public PatientDetails_PatientOverview PatientOverview;

        [JsonProperty("relativeContactPrototype")]
        public PatientDetails_RelativeContactPrototype RelativeContactPrototype;

        [JsonProperty("patientPreferences")]
        public PatientDetails_PatientPreferences PatientPreferences;

        [JsonProperty("patientPreferenceById")]
        public PatientDetails_PatientPreferenceById PatientPreferenceById;

        [JsonProperty("patientEvents")]
        public PatientDetails_PatientEvents PatientEvents;

        [JsonProperty("availableContacts")]
        public PatientDetails_AvailableContacts AvailableContacts;

        [JsonProperty("inboxMessages")]
        public PatientDetails_InboxMessages InboxMessages;

        [JsonProperty("outboxMessages")]
        public PatientDetails_OutboxMessages OutboxMessages;

        [JsonProperty("draftMessages")]
        public PatientDetails_DraftMessages DraftMessages;

        [JsonProperty("deletedMessages")]
        public PatientDetails_DeletedMessages DeletedMessages;

        [JsonProperty("addressPrototype")]
        public PatientDetails_AddressPrototype AddressPrototype;

        [JsonProperty("medcomClinicalEmailCorrespondencePrototype")]
        public PatientDetails_MedcomClinicalEmailCorrespondencePrototype MedcomClinicalEmailCorrespondencePrototype;

        [JsonProperty("dischargeLetterPrototype")]
        public PatientDetails_DischargeLetterPrototype DischargeLetterPrototype;

        [JsonProperty("patientConditions")]
        public PatientDetails_PatientConditions PatientConditions;

        [JsonProperty("patientConditionById")]
        public PatientDetails_PatientConditionById PatientConditionById;

        [JsonProperty("conditionsBulkPrototype")]
        public PatientDetails_ConditionsBulkPrototype ConditionsBulkPrototype;

        [JsonProperty("newObservationsBulkPrototype")]
        public PatientDetails_NewObservationsBulkPrototype NewObservationsBulkPrototype;

        [JsonProperty("availableConditionClassifications")]
        public PatientDetails_AvailableConditionClassifications AvailableConditionClassifications;

        [JsonProperty("activityLinks")]
        public PatientDetails_ActivityLinks ActivityLinks;

        [JsonProperty("managePatientIncome")]
        public PatientDetails_ManagePatientIncome ManagePatientIncome;

        [JsonProperty("prototypePatientIncomeValueSchedule")]
        public PatientDetails_PrototypePatientIncomeValueSchedule PrototypePatientIncomeValueSchedule;

        [JsonProperty("averageVisitedTimeLink")]
        public PatientDetails_AverageVisitedTimeLink AverageVisitedTimeLink;

        [JsonProperty("availableConditionClassificationLaws")]
        public PatientDetails_AvailableConditionClassificationLaws AvailableConditionClassificationLaws;

        [JsonProperty("dynamicTemplate")]
        public PatientDetails_DynamicTemplate DynamicTemplate;

        [JsonProperty("measurementInstructionPrototype")]
        public PatientDetails_MeasurementInstructionPrototype MeasurementInstructionPrototype;

        [JsonProperty("measurementInstructions")]
        public PatientDetails_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("measurementData")]
        public PatientDetails_MeasurementData MeasurementData;

        [JsonProperty("audit")]
        public PatientDetails_Audit Audit;

        [JsonProperty("prototypeAddressIndicatorValueSchedule")]
        public PatientDetails_PrototypeAddressIndicatorValueSchedule PrototypeAddressIndicatorValueSchedule;

        [JsonProperty("availableAssignmentTypes")]
        public PatientDetails_AvailableAssignmentTypes AvailableAssignmentTypes;

        [JsonProperty("eop")]
        public PatientDetails_Eop Eop;

        [JsonProperty("eopProblemPrototypes")]
        public PatientDetails_EopProblemPrototypes EopProblemPrototypes;

        [JsonProperty("eopVisitationPrototype")]
        public PatientDetails_EopVisitationPrototype EopVisitationPrototype;

        [JsonProperty("eopFollowUpPrototype")]
        public PatientDetails_EopFollowUpPrototype EopFollowUpPrototype;

        [JsonProperty("currentHclBasket")]
        public PatientDetails_CurrentHclBasket CurrentHclBasket;

        [JsonProperty("currentHclBasketSummary")]
        public PatientDetails_CurrentHclBasketSummary CurrentHclBasketSummary;

        [JsonProperty("hclGrants")]
        public PatientDetails_HclGrants HclGrants;

        [JsonProperty("lendings")]
        public PatientDetails_Lendings Lendings;

        [JsonProperty("hclDepotOrders")]
        public PatientDetails_HclDepotOrders HclDepotOrders;

        [JsonProperty("createOrReplaceActivityLink")]
        public PatientDetails_CreateOrReplaceActivityLink CreateOrReplaceActivityLink;

        [JsonProperty("financeOverview")]
        public PatientDetails_FinanceOverview FinanceOverview;

        [JsonProperty("medcomMunicipalityLetterPrototype")]
        public PatientDetails_MedcomMunicipalityLetterPrototype MedcomMunicipalityLetterPrototype;

        [JsonProperty("cprSystemRelativesAvailableTypes")]
        public PatientDetails_CprSystemRelativesAvailableTypes CprSystemRelativesAvailableTypes;

        [JsonProperty("cprSystemRelatives")]
        public PatientDetails_CprSystemRelatives CprSystemRelatives;

        [JsonProperty("medcomEmergencyMunicipalityLetterPrototype")]
        public PatientDetails_MedcomEmergencyMunicipalityLetterPrototype MedcomEmergencyMunicipalityLetterPrototype;

        [JsonProperty("prototypePatientStateValueSchedule")]
        public PatientDetails_PrototypePatientStateValueSchedule PrototypePatientStateValueSchedule;

        [JsonProperty("prototypeFreeChoiceValueSchedule")]
        public PatientDetails_PrototypeFreeChoiceValueSchedule PrototypeFreeChoiceValueSchedule;

        [JsonProperty("patientOrganizations")]
        public PatientDetails_PatientOrganizations PatientOrganizations;

        [JsonProperty("availableBaskets")]
        public PatientDetails_AvailableBaskets AvailableBaskets;

        [JsonProperty("financeInsightFilters")]
        public PatientDetails_FinanceInsightFilters FinanceInsightFilters;

        [JsonProperty("reportOfAdmissionPrototype")]
        public PatientDetails_ReportOfAdmissionPrototype ReportOfAdmissionPrototype;

        [JsonProperty("updateWithImage")]
        public PatientDetails_UpdateWithImage UpdateWithImage;

        [JsonProperty("sapa")]
        public PatientDetails_Sapa Sapa;

        [JsonProperty("availableLanguages")]
        public PatientDetails_AvailableLanguages AvailableLanguages;

        [JsonProperty("preferredLanguage")]
        public PatientDetails_PreferredLanguage PreferredLanguage;

        [JsonProperty("temporaryAddressUpdate")]
        public PatientDetails_TemporaryAddressUpdate TemporaryAddressUpdate;

        [JsonProperty("phoneNumberUpdateEnabled")]
        public PatientDetails_PhoneNumberUpdateEnabled PhoneNumberUpdateEnabled;

        [JsonProperty("patientReimbursementInformation")]
        public PatientDetails_PatientReimbursementInformation PatientReimbursementInformation;

        [JsonProperty("patientReimbursementInformationUpdate")]
        public PatientDetails_PatientReimbursementInformationUpdate PatientReimbursementInformationUpdate;

        [JsonProperty("patientNetworkContactPrototype")]
        public PatientDetails_PatientNetworkContactPrototype PatientNetworkContactPrototype;

        [JsonProperty("organDonation")]
        public PatientDetails_OrganDonation OrganDonation;

        [JsonProperty("lifeTreatmentWill")]
        public PatientDetails_LifeTreatmentWill LifeTreatmentWill;

        [JsonProperty("healthInsuranceGroup")]
        public PatientDetails_HealthInsuranceGroup HealthInsuranceGroup;
    }

}