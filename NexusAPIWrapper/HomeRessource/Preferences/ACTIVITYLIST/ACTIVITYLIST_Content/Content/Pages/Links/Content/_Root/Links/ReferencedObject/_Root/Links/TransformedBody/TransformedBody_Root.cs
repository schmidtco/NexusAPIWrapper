using AngleSharp.Dom;
using CsQuery.Engine.PseudoClassSelectors;
using CsQuery.EquationParser.Implementation;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusAPIWrapper.HomeRessource.Preferences.ACTIVITYLIST.ACTIVITYLIST_Content.Content.Pages.Links.Content._Root.Links.ReferencedObject._Root.Links.TransformedBody
{
    public class TransformedBody_Root
    {
        /// <summary>
        /// Pårørende/Relationer
        /// </summary>
        public List<TransformedBody_Relavites> Relatives { get; set; }
        /// <summary>
        /// Pårørende/Relationer - kommentardelen
        /// </summary>
        public string CommentsForRelatives { get; set; }
        /// <summary>
        /// Aktuel indlæggelse
        /// </summary>
        public TransformedBody_CurrentAdmission CurrentAdmission { get; set; }
        /// <summary>
        /// Diagnoser
        /// </summary>
        public List<TransformedBody_Diagnoses> Diagnoses {  get; set; }
        /// <summary>
        /// Funktionsevner ved udskrivelse
        /// </summary>
        public List<TransformedBody_FunctionalAbilitiesAtDischarge> FunctionalAbilitiesAtDischarge { get; set; }
        /// <summary>
        /// Sygeplejefaglige problemområder
        /// </summary>
        public List<TransformedBody_NursingProfessionalProblemAreas> NursingProfessionalProblemAreas {  get; set; }
        /// <summary>
        /// Seneste medicingivning
        /// </summary>
        public TransformedBody_MostRecentMedicationAdministration MostRecentMedicationAdministration { get; set; }
        /// <summary>
        /// Medicin information relateret til udskrivning
        /// </summary>
        public TransformedBody_MedInfoRelatedToDischarge MedicationInformationRelatedToDischarge { get; set; }
        /// <summary>
        /// Aftaler omkring kost første døgn efter udskrivning
        /// </summary>
        public TransformedBody_AgrRegDietFirstDayAftDischarge AgreementsRegardingDietTheFirstDayAfterDischarge { get; set; }
        /// <summary>
        /// Fremtidige aftaler
        /// </summary>
        public string FutureAgreements { get; set; }


        public TransformedBody_Root()
        {
            Relatives = new List<TransformedBody_Relavites>();
            CurrentAdmission = new TransformedBody_CurrentAdmission();
            Diagnoses = new List<TransformedBody_Diagnoses>();
            FunctionalAbilitiesAtDischarge = new List<TransformedBody_FunctionalAbilitiesAtDischarge>();
            NursingProfessionalProblemAreas = new List<TransformedBody_NursingProfessionalProblemAreas>();
            MostRecentMedicationAdministration = new TransformedBody_MostRecentMedicationAdministration();
            MedicationInformationRelatedToDischarge = new TransformedBody_MedInfoRelatedToDischarge();
            AgreementsRegardingDietTheFirstDayAfterDischarge = new TransformedBody_AgrRegDietFirstDayAftDischarge();
            //FutureAgreements = new TransformedBody_FutureAgreements();
        }
    }
}
