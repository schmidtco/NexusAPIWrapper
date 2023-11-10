using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_Links
    {
        [JsonProperty("self")]
        public PatientDetailsSearch_Self Self;

        [JsonProperty("availableCountries")]
        public PatientDetailsSearch_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public PatientDetailsSearch_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("prototypeValuePeriod")]
        public PatientDetailsSearch_PrototypeValuePeriod PrototypeValuePeriod;

        [JsonProperty("deleteValuePeriod")]
        public PatientDetailsSearch_DeleteValuePeriod DeleteValuePeriod;

        [JsonProperty("prototypeResidentialMunicipalityValueSchedule")]
        public PatientDetailsSearch_PrototypeResidentialMunicipalityValueSchedule PrototypeResidentialMunicipalityValueSchedule;

        [JsonProperty("prototypePayingMunicipalityValueSchedule")]
        public PatientDetailsSearch_PrototypePayingMunicipalityValueSchedule PrototypePayingMunicipalityValueSchedule;

        [JsonProperty("prototypeActingMunicipalityValueSchedule")]
        public PatientDetailsSearch_PrototypeActingMunicipalityValueSchedule PrototypeActingMunicipalityValueSchedule;

        [JsonProperty("documentPrototype")]
        public PatientDetailsSearch_DocumentPrototype DocumentPrototype;

        [JsonProperty("documentPrototypeUndefinedPathway")]
        public PatientDetailsSearch_DocumentPrototypeUndefinedPathway DocumentPrototypeUndefinedPathway;

        [JsonProperty("availableFormDefinitions")]
        public PatientDetailsSearch_AvailableFormDefinitions AvailableFormDefinitions;

        [JsonProperty("citizenOverviewForms")]
        public PatientDetailsSearch_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("searchFormUids")]
        public PatientDetailsSearch_SearchFormUids SearchFormUids;

        [JsonProperty("medcomFs3DataExchangeSetPrototype")]
        public PatientDetailsSearch_MedcomFs3DataExchangeSetPrototype MedcomFs3DataExchangeSetPrototype;

        [JsonProperty("getFormDataItemsByTags")]
        public PatientDetailsSearch_GetFormDataItemsByTags GetFormDataItemsByTags;

        [JsonProperty("availableLetterTemplates")]
        public PatientDetailsSearch_AvailableLetterTemplates AvailableLetterTemplates;

        [JsonProperty("availableLetterTemplatesNotPreassigned")]
        public PatientDetailsSearch_AvailableLetterTemplatesNotPreassigned AvailableLetterTemplatesNotPreassigned;

        [JsonProperty("medicationCard")]
        public PatientDetailsSearch_MedicationCard MedicationCard;

        [JsonProperty("medicationDatainsight")]
        public PatientDetailsSearch_MedicationDatainsight MedicationDatainsight;

        [JsonProperty("pnMedicationDatainsight")]
        public PatientDetailsSearch_PnMedicationDatainsight PnMedicationDatainsight;

        [JsonProperty("medicationCardInViewModeHandout")]
        public PatientDetailsSearch_MedicationCardInViewModeHandout MedicationCardInViewModeHandout;

        [JsonProperty("medicationCardInViewModeNursing")]
        public PatientDetailsSearch_MedicationCardInViewModeNursing MedicationCardInViewModeNursing;

        [JsonProperty("historicMedicationOverview")]
        public PatientDetailsSearch_HistoricMedicationOverview HistoricMedicationOverview;

        [JsonProperty("medicationCardPatientInformation")]
        public PatientDetailsSearch_MedicationCardPatientInformation MedicationCardPatientInformation;

        [JsonProperty("pnMedications")]
        public PatientDetailsSearch_PnMedications PnMedications;

        [JsonProperty("pnMedicationGroups")]
        public PatientDetailsSearch_PnMedicationGroups PnMedicationGroups;

        [JsonProperty("pnMedicationDispensationAvailableStates")]
        public PatientDetailsSearch_PnMedicationDispensationAvailableStates PnMedicationDispensationAvailableStates;

        [JsonProperty("medicineDispensing")]
        public PatientDetailsSearch_MedicineDispensing MedicineDispensing;

        [JsonProperty("bulkActivitiesPrintPrototype")]
        public PatientDetailsSearch_BulkActivitiesPrintPrototype BulkActivitiesPrintPrototype;

        [JsonProperty("availableProgramPathways")]
        public PatientDetailsSearch_AvailableProgramPathways AvailableProgramPathways;

        [JsonProperty("patientActivities")]
        public PatientDetailsSearch_PatientActivities PatientActivities;

        [JsonProperty("activePrograms")]
        public PatientDetailsSearch_ActivePrograms ActivePrograms;

        [JsonProperty("availablePathwayAssociation")]
        public PatientDetailsSearch_AvailablePathwayAssociation AvailablePathwayAssociation;

        [JsonProperty("availableOrganizations")]
        public PatientDetailsSearch_AvailableOrganizations AvailableOrganizations;

        [JsonProperty("availablePatientStates")]
        public PatientDetailsSearch_AvailablePatientStates AvailablePatientStates;

        [JsonProperty("update")]
        public PatientDetailsSearch_Update Update;

        [JsonProperty("manageSms")]
        public PatientDetailsSearch_ManageSms ManageSms;

        [JsonProperty("patientCardPrint")]
        public PatientDetailsSearch_PatientCardPrint PatientCardPrint;

        [JsonProperty("patientOverview")]
        public PatientDetailsSearch_PatientOverview PatientOverview;

        [JsonProperty("relativeContactPrototype")]
        public PatientDetailsSearch_RelativeContactPrototype RelativeContactPrototype;

        [JsonProperty("patientPreferences")]
        public PatientDetailsSearch_PatientPreferences PatientPreferences;

        [JsonProperty("patientPreferenceById")]
        public PatientDetailsSearch_PatientPreferenceById PatientPreferenceById;

        [JsonProperty("patientEvents")]
        public PatientDetailsSearch_PatientEvents PatientEvents;

        [JsonProperty("availableContacts")]
        public PatientDetailsSearch_AvailableContacts AvailableContacts;

        [JsonProperty("inboxMessages")]
        public PatientDetailsSearch_InboxMessages InboxMessages;

        [JsonProperty("outboxMessages")]
        public PatientDetailsSearch_OutboxMessages OutboxMessages;

        [JsonProperty("draftMessages")]
        public PatientDetailsSearch_DraftMessages DraftMessages;

        [JsonProperty("deletedMessages")]
        public PatientDetailsSearch_DeletedMessages DeletedMessages;

        [JsonProperty("addressPrototype")]
        public PatientDetailsSearch_AddressPrototype AddressPrototype;

        [JsonProperty("medcomClinicalEmailCorrespondencePrototype")]
        public PatientDetailsSearch_MedcomClinicalEmailCorrespondencePrototype MedcomClinicalEmailCorrespondencePrototype;

        [JsonProperty("dischargeLetterPrototype")]
        public PatientDetailsSearch_DischargeLetterPrototype DischargeLetterPrototype;

        [JsonProperty("patientConditions")]
        public PatientDetailsSearch_PatientConditions PatientConditions;

        [JsonProperty("patientConditionById")]
        public PatientDetailsSearch_PatientConditionById PatientConditionById;

        [JsonProperty("conditionsBulkPrototype")]
        public PatientDetailsSearch_ConditionsBulkPrototype ConditionsBulkPrototype;

        [JsonProperty("newObservationsBulkPrototype")]
        public PatientDetailsSearch_NewObservationsBulkPrototype NewObservationsBulkPrototype;

        [JsonProperty("availableConditionClassifications")]
        public PatientDetailsSearch_AvailableConditionClassifications AvailableConditionClassifications;

        [JsonProperty("activityLinks")]
        public PatientDetailsSearch_ActivityLinks ActivityLinks;

        [JsonProperty("managePatientIncome")]
        public PatientDetailsSearch_ManagePatientIncome ManagePatientIncome;

        [JsonProperty("prototypePatientIncomeValueSchedule")]
        public PatientDetailsSearch_PrototypePatientIncomeValueSchedule PrototypePatientIncomeValueSchedule;

        [JsonProperty("averageVisitedTimeLink")]
        public PatientDetailsSearch_AverageVisitedTimeLink AverageVisitedTimeLink;

        [JsonProperty("availableConditionClassificationLaws")]
        public PatientDetailsSearch_AvailableConditionClassificationLaws AvailableConditionClassificationLaws;

        [JsonProperty("dynamicTemplate")]
        public PatientDetailsSearch_DynamicTemplate DynamicTemplate;

        [JsonProperty("measurementInstructionPrototype")]
        public PatientDetailsSearch_MeasurementInstructionPrototype MeasurementInstructionPrototype;

        [JsonProperty("measurementInstructions")]
        public PatientDetailsSearch_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("measurementData")]
        public PatientDetailsSearch_MeasurementData MeasurementData;

        [JsonProperty("audit")]
        public PatientDetailsSearch_Audit Audit;

        [JsonProperty("prototypeAddressIndicatorValueSchedule")]
        public PatientDetailsSearch_PrototypeAddressIndicatorValueSchedule PrototypeAddressIndicatorValueSchedule;

        [JsonProperty("availableAssignmentTypes")]
        public PatientDetailsSearch_AvailableAssignmentTypes AvailableAssignmentTypes;

        [JsonProperty("eop")]
        public PatientDetailsSearch_Eop Eop;

        [JsonProperty("eopProblemPrototypes")]
        public PatientDetailsSearch_EopProblemPrototypes EopProblemPrototypes;

        [JsonProperty("eopVisitationPrototype")]
        public PatientDetailsSearch_EopVisitationPrototype EopVisitationPrototype;

        [JsonProperty("eopFollowUpPrototype")]
        public PatientDetailsSearch_EopFollowUpPrototype EopFollowUpPrototype;

        [JsonProperty("currentHclBasket")]
        public PatientDetailsSearch_CurrentHclBasket CurrentHclBasket;

        [JsonProperty("currentHclBasketSummary")]
        public PatientDetailsSearch_CurrentHclBasketSummary CurrentHclBasketSummary;

        [JsonProperty("hclGrants")]
        public PatientDetailsSearch_HclGrants HclGrants;

        [JsonProperty("lendings")]
        public PatientDetailsSearch_Lendings Lendings;

        [JsonProperty("hclDepotOrders")]
        public PatientDetailsSearch_HclDepotOrders HclDepotOrders;

        [JsonProperty("createOrReplaceActivityLink")]
        public PatientDetailsSearch_CreateOrReplaceActivityLink CreateOrReplaceActivityLink;

        [JsonProperty("financeOverview")]
        public PatientDetailsSearch_FinanceOverview FinanceOverview;

        [JsonProperty("medcomMunicipalityLetterPrototype")]
        public PatientDetailsSearch_MedcomMunicipalityLetterPrototype MedcomMunicipalityLetterPrototype;

        [JsonProperty("cprSystemRelativesAvailableTypes")]
        public PatientDetailsSearch_CprSystemRelativesAvailableTypes CprSystemRelativesAvailableTypes;

        [JsonProperty("cprSystemRelatives")]
        public PatientDetailsSearch_CprSystemRelatives CprSystemRelatives;

        [JsonProperty("medcomEmergencyMunicipalityLetterPrototype")]
        public PatientDetailsSearch_MedcomEmergencyMunicipalityLetterPrototype MedcomEmergencyMunicipalityLetterPrototype;

        [JsonProperty("prototypePatientStateValueSchedule")]
        public PatientDetailsSearch_PrototypePatientStateValueSchedule PrototypePatientStateValueSchedule;

        [JsonProperty("prototypeFreeChoiceValueSchedule")]
        public PatientDetailsSearch_PrototypeFreeChoiceValueSchedule PrototypeFreeChoiceValueSchedule;

        [JsonProperty("patientOrganizations")]
        public PatientDetailsSearch_PatientOrganizations PatientOrganizations;

        [JsonProperty("availableBaskets")]
        public PatientDetailsSearch_AvailableBaskets AvailableBaskets;

        [JsonProperty("financeInsightFilters")]
        public PatientDetailsSearch_FinanceInsightFilters FinanceInsightFilters;

        [JsonProperty("reportOfAdmissionPrototype")]
        public PatientDetailsSearch_ReportOfAdmissionPrototype ReportOfAdmissionPrototype;

        [JsonProperty("updateWithImage")]
        public PatientDetailsSearch_UpdateWithImage UpdateWithImage;

        [JsonProperty("availableLanguages")]
        public PatientDetailsSearch_AvailableLanguages AvailableLanguages;

        [JsonProperty("preferredLanguage")]
        public PatientDetailsSearch_PreferredLanguage PreferredLanguage;

        [JsonProperty("temporaryAddressUpdate")]
        public PatientDetailsSearch_TemporaryAddressUpdate TemporaryAddressUpdate;

        [JsonProperty("phoneNumberUpdateEnabled")]
        public PatientDetailsSearch_PhoneNumberUpdateEnabled PhoneNumberUpdateEnabled;

        [JsonProperty("patientReimbursementInformation")]
        public PatientDetailsSearch_PatientReimbursementInformation PatientReimbursementInformation;

        [JsonProperty("patientReimbursementInformationUpdate")]
        public PatientDetailsSearch_PatientReimbursementInformationUpdate PatientReimbursementInformationUpdate;

        [JsonProperty("patientNetworkContactPrototype")]
        public PatientDetailsSearch_PatientNetworkContactPrototype PatientNetworkContactPrototype;

        [JsonProperty("organDonation")]
        public PatientDetailsSearch_OrganDonation OrganDonation;

        [JsonProperty("lifeTreatmentWill")]
        public PatientDetailsSearch_LifeTreatmentWill LifeTreatmentWill;

        [JsonProperty("healthInsuranceGroup")]
        public PatientDetailsSearch_HealthInsuranceGroup HealthInsuranceGroup;
    }

}